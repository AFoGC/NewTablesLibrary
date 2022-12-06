using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace NewTablesLibrary
{
    public class Table<T> : BaseTable, INotifyCollectionChanged, IEnumerable<T>
                            where T : Cell, new()
    {
        private int counter = 0;
        private ObservableCollection<T> _cells;

        public Table()
        {
            _cells = new ObservableCollection<T>();
            _cells.CollectionChanged += CellsChanged;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public TablesCollection ParentCollection { get; internal set; }

        public override Type DataType => typeof(T);
        public int LastID => counter;
        public int Count => _cells.Count;
        public T this[int index] => _cells[index];
        public int IndexOf(T item) => _cells.IndexOf(item);
        public bool Contains(T item) => _cells.Contains(item);

        private void CellsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public bool Add()
        {
            T item = new T();
            return Add(item);
        }

        public bool Add(T item)
        {
            bool isAdded = false;

            if (Contains(item) == false)
            {
                _cells.Add(item);
                AddItemConnection(item);
                isAdded = true;
            }

            return isAdded;
        }

        private void AddItemConnection(T item)
        {
            item.ID = ++counter;
            item.ParentTable = this;
        }

        public bool Remove(T item)
        {
            bool isRemoved = _cells.Remove(item);

            //TODO Add counter logic (example in old version)
            if (isRemoved)
                RemoveItemConnection(item);

            return isRemoved;
        }

        public void Clear()
        {
            foreach (T item in _cells)
                RemoveItemConnection(item);

            _cells.Clear();
        }

        private void RemoveItemConnection(T item)
        {
            item.ID = 0;
            item.ParentTable = null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cells.GetEnumerator();
        }
    }
}
