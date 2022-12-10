using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NewTablesLibrary
{
    public class ManyToOne<ParentT, T> : BaseConnectionById 
                                         where T : Cell, new() where ParentT : Cell
    {
        private readonly ParentT _parent;
        private T _value;

        [ConnectionId]
        private int _valueID;

        public ManyToOne(ParentT parent)
        {
            _parent = parent;
        }

        public T Value => _value;
        public int ValueID => _valueID;

        internal void InnerSetValue(T value)
        {
            _value = value;
            if (_value != null)
                _valueID = value.ID;
            else
                _valueID = 0;
        }

        internal override void LoadConnection()
        {
            T target = GetTargetElement();
            if (target != null)
            {
                OneToManyCollection<T, ParentT> oneToMany = GetOneToManyField(target);
                oneToMany.Add(_parent);
            }
        }

        private T GetTargetElement()
        {
            TablesCollection collection = _parent.ParentTable.ParentCollection;
            Table<T> table = collection.GetTableByDataType<T>();
            return table.Where(x => x.ID == this.ValueID).FirstOrDefault();
        }

        private OneToManyCollection<T, ParentT> GetOneToManyField(T value)
        {
            Type type = value.GetType();
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic);

            IEnumerable<FieldInfo> interFields = fields.Where(x =>
                x.GetValue(value).GetType() == typeof(OneToManyCollection<T, ParentT>));

            FieldInfo interField = interFields.First();
            return interField.GetValue(value) as OneToManyCollection<T, ParentT>;
        }

        //internal static FieldInfo GetID

        /*
        public void SetValue(T value)
        {
            RemoveFromPrevious();



        }

        private void RemoveFromPrevious()
        {
            if (_value != null)
            {
                OneToManyCollection<ParentT, T> collection = GetOneToManyField(_value);
                collection.InnerRemove(_parent);
            }
        }
        */
    }
}
