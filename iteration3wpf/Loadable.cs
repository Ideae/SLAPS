using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace iteration3wpf
{
    public abstract class Loadable<T> : Loadable where T : Loadable<T>, new()
    {
        public string TableName { get { return this.GetType().Name + "s"; } }
        protected readonly static ConcurrentDictionary<int, T> loadedCache = new ConcurrentDictionary<int, T>();

        private static readonly Func<int, T> _addDelegate = key =>
            {
                T item = new T();
                item.SetDataLite(SQLiteDB.main.getRowById(item.TableName, key));
                return item;
            };

        static Loadable()
        {
            //_tableName = typeof(T).TypeName() + "s";
        }

        protected virtual void SetDataLite(DataRow data)
        {
            FieldInfo[] fInfos = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var p in fInfos)
            {
                Synchronize syn = (Synchronize) p.GetCustomAttribute(typeof(Synchronize));
                if (syn == null || syn.lite == false ) return;

                Object o = null;
                string columnName = p.Name.Substring(1);
                if (!data.Table.Columns.Contains(columnName))
                {
                    String t = "";
                    if (p.FieldType == typeof(int) || p.FieldType == typeof(long) || p.FieldType == typeof(byte)) t = "INTEGER";
                    else if (p.FieldType == typeof(float) || p.FieldType == typeof(double)) t = "NUMERIC";
                    else t = "TEXT";
                    SQLiteDB.main.ExecuteNonQuery("ALTER TABLE " + TableName + " ADD COLUMN " + columnName + " "+ t +";");
                    throw new SystemException("Database Was upgraded. The previous database was inconsistent with new code. This excpe[tion will probably not Happen the next time you run the program.");
                }

                o = parseDB(data, p.FieldType, columnName);
                p.SetValue(this, o);
            }
        }

        protected virtual void SetData(DataRow data)
        {
            FieldInfo[] fInfos = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var p in fInfos)
            {
                if (p.GetCustomAttribute(typeof(Synchronize)) == null) return;

                Object o = null;
                string columnName = p.Name.Substring(1);
                if (!data.Table.Columns.Contains(columnName))
                {
                    String t = "";
                    if (p.FieldType == typeof(int) || p.FieldType == typeof(long) || p.FieldType == typeof(byte)) t = "INTEGER";
                    else if (p.FieldType == typeof(float) || p.FieldType == typeof(double)) t = "NUMERIC";
                    else t = "TEXT";
                    SQLiteDB.main.ExecuteNonQuery("ALTER TABLE " + TableName + " ADD COLUMN " + columnName + " " + t + ";");
                    throw new SystemException("Database Was upgraded. The previous database was inconsistent with new code. This excpe[tion will probably not Happen the next time you run the program.");
                }

                o = parseDB(data, p.FieldType, columnName);
                p.SetValue(this, o);
            }
        }

        private static object parseDB(DataRow data, Type t, string column)
        {

            Object o = null;
            if (t == typeof(string)) o = data.Field<string>(column);
            else if (t == typeof(int)) o = (int)data.Field<long>(column);
            else if (t.IsEnum) o = Enum.Parse(t, data.Field<string>(column));
            else if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>))
                if (t.GetGenericArguments()[0].IsSubclassOf(typeof(Loadable)))
                {
                    Type[] ga = t.GetGenericArguments();
                    MethodInfo mi = ga[0].GetMethod("decodeList", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                    o = mi.Invoke(null, new object[1] { (data.Field<string>(column)) });
                }

                else throw new NotImplementedException("Only Lists of Loadable derived types are supported.");
            else throw new NotImplementedException("Only Primitives and Loadable derived types are supported.");
            return o;
        }
        private static string composeDB(Object o)
        {
            string s = "";
            if (o is string) s = (string)o;
            else if (o is int) s = ((long)o).ToString();
            else if (o.GetType().IsEnum) s = ((Enum)o).ToString();
            else if (o.GetType().IsGenericType && o.GetType().GetGenericTypeDefinition() == typeof(List<>))
                if (o.GetType().GetGenericArguments()[0].IsSubclassOf(typeof(Loadable)))
                {
                    dynamic d = (dynamic)o;
                    foreach (var a in d) { s = s + "," + a.Id; }
                }
                else throw new NotImplementedException("Only Lists of Loadable derived types are supported.");
            else throw new NotImplementedException("Only Lists of Loadable derived types are supported.");
            return s;
        }


        public abstract int Id { get; set; }

        public static T GetById(int id)
        {
            return loadedCache.GetOrAdd(id, _addDelegate);
        }

        public static List<T> decodeList(string s)
        {
            List<T> ret = new List<T>();
            if (s == null) return ret;
            string[] ss = s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return ss.Select(sss => Int32.Parse(sss)).ToList().ConvertAll<T>(i => GetById(i));

        }
        protected void reload() { }
        protected V syncUp<V>(string propName, V value)
        {
            V oldVal = (V)this.GetType().GetProperty(propName).GetValue(this);
            V oldValDB = (V)parseDB(SQLiteDB.main.getRowById(TableName, this.Id), typeof(V), propName);
            if (oldVal != null && !oldVal.Equals(oldValDB))
            {
                MessageBox.Show("Database was modified by other User at the same time, Please check the value and try again.");
                return oldValDB;
            }
            else
            {
                SQLiteDB.main.Update(TableName, new Dictionary<string, string>() { { propName, composeDB(value) } }, "Id=" + this.Id);
                return value;
            }
        }

        protected V syncDown<V>(string propName, V value)
        {
            if (value != null && !value.Equals(default(V))) return value;
            V DBVal = (V)parseDB(SQLiteDB.main.getRowById(TableName, this.Id), typeof(V), propName);
            return DBVal;
        }


    }
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class Synchronize : System.Attribute {
        public bool lite = false;

        public Synchronize(bool lite = false) { this.lite = lite; }
    }

    public interface Loadable { }
}
