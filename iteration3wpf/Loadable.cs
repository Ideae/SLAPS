using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace iteration3wpf
{
    public abstract class Loadable<T> : Loadable where T : Loadable<T>
    {
        public static string TableName { get { return typeof(T).Name + "s"; } }
        protected readonly static ConcurrentDictionary<int, T> loadedCache = new ConcurrentDictionary<int, T>();

        private static readonly Func<int, T> _addDelegate = key =>
            {
                T item = (T)Activator.CreateInstance(typeof(T), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new object[] { key }, null);
                item.SetDataLite(SQLiteDB.main.getRowById(TableName, key));
                return item;
            };

        protected Loadable(int id)
        {
            //Set the internal Id in the constructor
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
                checkUpgrade(data, p.FieldType, columnName);
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
                checkUpgrade(data, p.FieldType, columnName);
                o = parseDB(data, p.FieldType, columnName);
                p.SetValue(this, o);
            }
        }

        private void checkUpgrade(DataRow data, Type type, string columnName)
        {
            if (!data.Table.Columns.Contains(columnName))
            {
                String t = "";
                if (type == typeof(int) || type == typeof(long) || type == typeof(byte)) t = "INTEGER";
                else if (type == typeof(float) || type == typeof(double)) t = "NUMERIC";
                else t = "TEXT";
                SQLiteDB.main.ExecuteNonQuery("ALTER TABLE " + TableName + " ADD COLUMN " + columnName + " " + t + ";");
                throw new SystemException("Database Was upgraded. The previous database was inconsistent with new code. This excpe[tion will probably not Happen the next time you run the program.");
            }
        }

        private static object parseDB(DataRow data, Type t, string column)
        {
            Object o = null;
            if (t == typeof(string)) o = data.Field<string>(column);
            else if (t == typeof(int)) o = (int)(data.Field<long?>(column) ?? -1);
            else if (t.IsEnum) o = Enum.Parse(t, data.Field<string>(column) ?? Activator.CreateInstance(t).ToString());
            else if (t == typeof(float)) o = (float?)data.Field<float?>(column) ?? -1f;
            else if (t == typeof(DateTime)) o = DateTime.Parse(data.Field<string>(column) ?? DateTime.Now.ToString());
            else if (t == typeof(bool)) o = (bool.Parse(data.Field<string>(column) ?? bool.FalseString));
            else if (t.IsSubclassOf(typeof(Loadable)))
            {
                MethodInfo mi = t.GetMethod("GetById", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                o = mi.Invoke(null, new object[1] { ((int?)data.Field<long?>(column) ?? -1) });
            }
            else if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ObservableCollection<>))
                if (t.GetGenericArguments()[0] == typeof(Loadable))
                {
                    string list = data.Field<string>(column);
                    if (String.IsNullOrEmpty(list)) return new ObservableCollection<Loadable>();
                    string[] items = list.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    ObservableCollection<Loadable> ret = new ObservableCollection<Loadable>();
                    foreach (string item in items)
                    {
                        string[] args = item.Split('#');
                        Type ga = Type.GetType(args[0]);
                        MethodInfo mi = ga.GetMethod("GetById", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                        Object oo = mi.Invoke(null, new object[1] { Int32.Parse(args[1]) });
                        ret.Add((Loadable)oo);
                    }
                    o = ret;

                }
                else if (t.GetGenericArguments()[0].IsSubclassOf(typeof(Loadable)))
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
            else if (o is int) s = ((long)((int)o)).ToString();
            else if (o is float) s = o.ToString();
            else if (o.GetType().IsEnum) s = ((Enum)o).ToString();
            else if (o is DateTime) s = o.ToString();
            else if (o is bool) s = o.ToString();
            else if (o.GetType().IsSubclassOf(typeof(Loadable))) s = ((dynamic)o).Id.ToString();
            else if (o.GetType().IsGenericType && o.GetType().GetGenericTypeDefinition() == typeof(ObservableCollection<>))
                if (o.GetType().GetGenericArguments()[0] == typeof(Loadable))
                {
                    dynamic d = (dynamic)o;
                    foreach (dynamic a in d) { s = s + "," + a.GetType().Namespace + "." + a.GetType().Name + "#" + a.Id; }
                }
                else if (o.GetType().GetGenericArguments()[0].IsSubclassOf(typeof(Loadable)))
                {
                    dynamic d = (dynamic)o;
                    foreach (var a in d) { s = s + "," + a.Id; }
                }
                else throw new NotImplementedException("Only Lists of Loadable derived types are supported.");
            else throw new NotImplementedException("Only Primitives and Loadable derived types are supported.");
            return s;
        }


        public abstract int Id { get; set; }

        public static T GetById(int id)
        {
            if (id == -1) return default(T);
            return loadedCache.GetOrAdd(id, _addDelegate);
        }

        public static ObservableCollection<T> decodeList(string s)
        {
            ObservableCollection<T> ret = new ObservableCollection<T>();
            if (s == null) return ret;
            string[] ss = s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var q in ss.Select(sss => Int32.Parse(sss)).ToList().ConvertAll<T>(i => GetById(i)).ToArray()) ret.Add(q);
            return ret;

        }
        protected void reload() { }
        protected V syncUp<V>(string propName, V value)
        {
            V oldVal = (V)this.GetType().GetProperty(propName).GetValue(this);
            DataRow d = SQLiteDB.main.getRowById(TableName, this.Id);
                Object o = parseDB(d, typeof(V), propName);
            V oldValDB = (V)o;

                SQLiteDB.main.Update(TableName, new Dictionary<string, string>() { { propName, composeDB(value) } }, "Id=" + this.Id);
                return value;
        }

        protected V syncDown<V>(string propName, V value)
        {
            if (value != null && !value.Equals(default(V)) &&
                !(value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(ObservableCollection<>) && ((dynamic)value).Count <=0 ))
                    return value;
            DataRow data = SQLiteDB.main.getRowById(TableName, this.Id);
            checkUpgrade(data, typeof(V), propName);
            V DBVal = (V)parseDB(data, typeof(V), propName);
            if (value != null && value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(ObservableCollection<>))
            {
                foreach (dynamic d in ((dynamic)DBVal))
                {
                    ((dynamic)value).Add(d);
                }
                return value;
            }
            return DBVal;
        }
        public static T getNew()
        {
            string res = SQLiteDB.main.ExecuteScalar("SELECT MAX(Id) FROM " + TableName + ";");
            int id = String.IsNullOrEmpty(res)? 1: Int32.Parse(res) + 1;
            SQLiteDB.main.Insert(TableName, new Dictionary<string, string>() { { "Id", id.ToString() } });
            object instantiatedType = Activator.CreateInstance(typeof(T), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new object[] { id }, null);
            return (T)instantiatedType;
        }

    }
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class Synchronize : System.Attribute {
        public bool lite = false;

        public Synchronize(bool lite = false) { this.lite = lite; }
    }

    public class Loadable { }

}
