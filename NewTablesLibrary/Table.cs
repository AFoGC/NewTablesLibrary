using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace NewTablesLibrary
{
    public class Table<T> : BaseTable, INotifyCollectionChanged
    {
        private ObservableCollection<T> _cells;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public Table()
        {
            _cells = new ObservableCollection<T>();
            _cells.CollectionChanged += CellsChanged;
        }

        public override Type DataType => typeof(T);
        public T this[int index] => _cells[index];
        public int IndexOf(T item) => _cells.IndexOf(item);
        public bool Contains(T item) => _cells.Contains(item);

        private void CellsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public bool Add(T item)
        {
            _cells.Add(item);
            return true;
        }

        public bool Remove(T item)
        {
            return _cells.Remove(item);
        }

        public void Clear()
        {
            _cells.Clear();
        }

        public void Move(int oldIndex, int newIndex)
        {
            _cells.Move(oldIndex, newIndex);
        }
    }
}
