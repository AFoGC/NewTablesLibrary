using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;

namespace NewTablesLibrary
{
    public class OneToManyCollection<ParentT, T> : INotifyCollectionChanged, ICollection<T>
                                                   where T : Cell where ParentT : Cell, new()
    {
        private readonly ParentT _parent;
        private readonly ObservableCollection<T> _cells;

        public OneToManyCollection(ParentT parent)
        {
            _parent = parent;
            _cells = new ObservableCollection<T>();
            _cells.CollectionChanged += CellsChanged;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Count => _cells.Count;
        public bool IsReadOnly => false;

        public T this[int index] => _cells[index];
        public int IndexOf(T item) => _cells.IndexOf(item);
        public bool Contains(T item) => _cells.Contains(item);

        private void CellsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public bool Add(T item)
        {
            InnerAdd(item);
            SetConnection(item);
            return true;
        }

        public bool Remove(T item)
        {
            bool removed = false;

            if (InnerRemove(item))
            {
                RemoveConnection(item);
                removed = true;
            }

            return removed;
        }

        public void Clear()
        {
            foreach (T item in _cells)
                RemoveConnection(item);

            _cells.Clear();
        }

        public void Move(int oldindex, int newIndex)
        {
            _cells.Move(oldindex, newIndex);
        }

        private void SetConnection(T item)
        {
            ManyToOne<T, ParentT> manyToOne = GetManyToOneField(item);
            manyToOne.InnerSetValue(_parent);
        }

        private void RemoveConnection(T item)
        {
            ManyToOne<T, ParentT> manyToOne = GetManyToOneField(item);
            manyToOne.InnerSetValue(null);
        }

        internal void InnerAdd(T item)
        {
            _cells.Add(item);
        }

        internal bool InnerRemove(T item)
        {
            return _cells.Remove(item);
        }

        private ManyToOne<T, ParentT> GetManyToOneField(T item)
        {
            Type type = item.GetType();
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic);

            IEnumerable<FieldInfo> interFields = fields.Where(x =>
                x.GetValue(item).GetType() == typeof(ManyToOne<T, ParentT>));

            FieldInfo interField = interFields.First();
            return interField.GetValue(item) as ManyToOne<T, ParentT>;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _cells.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cells.GetEnumerator();
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }
    }
}
