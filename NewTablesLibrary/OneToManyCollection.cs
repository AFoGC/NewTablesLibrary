using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using System.Collections.ObjectModel;

namespace NewTablesLibrary
{
    public class OneToManyCollection<T, ParentT> where T : Cell where ParentT : Cell
    {
        private readonly ParentT _parent;
        //private readonly List<T> _cells;
        private readonly ObservableCollection<T> _cells;

        public OneToManyCollection(ParentT parent)
        {
            _parent = parent;
        }

        public void Add(T item)
        {
            InnerAdd(item);
            ManyToOne<ParentT, T> manyToOne = GetManyToOneField(item);
            manyToOne.InnerSetValue(_parent);
        }

        public bool Remove(T item)
        {
            if (InnerRemove(item))
            {
                ManyToOne<ParentT, T> manyToOne = GetManyToOneField(item);
                manyToOne.InnerSetValue(null);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {

        }

        public void Move(int oldindex, int newIndex)
        {
            _cells.Move(oldindex, newIndex);
        }

        internal void InnerAdd(T item)
        {
            _cells.Add(item);
        }

        public bool InnerRemove(T item)
        {
            return _cells.Remove(item);
        }

        private ManyToOne<ParentT, T> GetManyToOneField(T item)
        {
            Type type = item.GetType();
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic);

            IEnumerable<FieldInfo> interFields = fields.Where(x =>
                x.GetValue(item).GetType() == typeof(ManyToOne<ParentT, T>));

            FieldInfo interField = interFields.First();
            return interField.GetValue(item) as ManyToOne<ParentT, T>;
        }
    }
}
