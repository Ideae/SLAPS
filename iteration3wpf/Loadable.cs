using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace iteration3wpf
{
    public abstract class Loadable<T> where T : Loadable<T>, new()
    {
        protected readonly static ConcurrentDictionary<int, T> loadedCache = new ConcurrentDictionary<int, T>();

        private static readonly string _tableName;

        static Loadable()
        {
            _tableName = typeof(T).TypeName() + "s";
        }

        protected abstract void SetData(DataRow data);

        public virtual int Id { get; set; }

        public static TItem GetById<TItem>(int id) where TItem : T
        {
            Func<int, TItem> _addDelegate = key =>
            {
                TItem item = (TItem)Activator.CreateInstance(typeof(TItem));
                item.SetData(SQLiteDB.main.getRowById(_tableName, key));
                return item;
            };
            return (TItem)loadedCache.GetOrAdd(id, _addDelegate);
        }
    }
}
