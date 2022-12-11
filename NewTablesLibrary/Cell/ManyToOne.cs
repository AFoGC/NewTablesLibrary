using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NewTablesLibrary
{
    public class ManyToOne<ParentT, T> : BaseConnectionById 
                                         where T : Cell, new() where ParentT : Cell
    {
        private readonly FieldInfo _oneToManyField;
        private readonly ParentT _parent;

        [ConnectionId]
        private int _valueID;
        private T _value;

        public ManyToOne(ParentT parent)
        {
            _parent = parent;
            _oneToManyField = GetOneToManyFieldInfo();
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
                OneToManyCollection<T, ParentT> oneToMany = GetOneToManyInstance(target);
                oneToMany.Add(_parent);
            }
        }

        private T GetTargetElement()
        {
            TablesCollection collection = _parent.ParentTable.ParentCollection;
            Table<T> table = collection.GetTableByDataType<T>();
            return table.Where(x => x.ID == this.ValueID).FirstOrDefault();
        }
        
        public void SetValue(T value)
        {
            RemoveFromPrevious();
            InnerSetValue(value);
            GetOneToManyInstance(value).InnerAdd(_parent);
        }

        private void RemoveFromPrevious()
        {
            if (_value != null)
            {
                OneToManyCollection<T, ParentT> collection = GetOneToManyInstance(_value);
                collection.InnerRemove(_parent);
            }
        }

        private OneToManyCollection<T, ParentT> GetOneToManyInstance(T value)
        {
            return _oneToManyField.GetValue(value) as OneToManyCollection<T, ParentT>;
        }

        private FieldInfo GetOneToManyFieldInfo()
        {
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic);

            IEnumerable<FieldInfo> interFields = fields.Where(x =>
                x.FieldType == typeof(OneToManyCollection<T, ParentT>));

            return interFields.First();
        }

        public override string ToString()
        {
            return ValueID.ToString();
        }
    }
}
