using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace NewTablesLibrary
{
    public class TablesCollection : INotifyCollectionChanged
    {
        private int counter = 0;
        private ObservableCollection<BaseTable> _tables;

        public TablesCollection()
        {
            _tables = new ObservableCollection<BaseTable>();
            _tables.CollectionChanged += TablesChanged;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Count => _tables.Count;
        public int IndexOf(BaseTable item) => _tables.IndexOf(item);
        public bool Contains(BaseTable item) => _tables.Contains(item);

        private void TablesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public void Add<T>(Table<T> table) where T : Cell, new()
        {
            Type tableType = table.GetType();
            if (_tables.Any(x => x.GetType() == tableType))
                throw new NotSupportedException("This collection already has a table with this type");

            _tables.Add(table);
            table.ID = ++counter;
            table.ParentCollection = this;
        }

        public T GetTableByTableType<T>() where T : BaseTable
        {
            Type tableType = typeof(T);
            IEnumerable<BaseTable> tables = 
                _tables.Where(x => x.GetType() == tableType);

            return tables.First() as T;
        }

        public Table<T> GetTableByDataType<T>() where T : Cell, new()
        {
            Type dataType = typeof(T);
            IEnumerable<BaseTable> tables = 
                _tables.Where(x => x.DataType == dataType);

            return tables.First() as Table<T>;
        }
    }
}
