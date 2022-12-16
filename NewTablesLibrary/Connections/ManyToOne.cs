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
            _oneToManyField = InitOneToManyFieldInfo();
        }

        public event Action ConnectionChanged;

        public T Value => _value;
        public int ValueID => _valueID;
        
        public void SetValue(T value)
        {
            RemoveFromPrevious();
            InnerSetValue(value);
            GetOneToManyInstance(value).InnerAdd(_parent);
        }

        internal void InnerSetValue(T value)
        {
            _value = value;
            if (_value != null)
                _valueID = value.ID;
            else
                _valueID = 0;

            ConnectionChanged?.Invoke();
        }

        private void RemoveFromPrevious()
        {
            if (_value != null)
                GetOneToManyInstance(_value).InnerRemove(_parent);
        }

        internal override void LoadConnection()
        {
            T target = GetTargetElement();

            if (target != null)
                GetOneToManyInstance(target).Add(_parent);
        }

        private T GetTargetElement()
        {
            TablesCollection collection = _parent.ParentTable.ParentCollection;
            Table<T> table = collection.GetTableByDataType<T>();
            return table.Where(x => x.ID == this.ValueID).FirstOrDefault();
        }

        private OneToManyCollection<T, ParentT> GetOneToManyInstance(T value)
        {
            return _oneToManyField.GetValue(value) as OneToManyCollection<T, ParentT>;
        }

        private FieldInfo InitOneToManyFieldInfo()
        {
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic);

            return fields
                .Where(x => x.FieldType == typeof(OneToManyCollection<T, ParentT>))
                .First();
        }

        public override void FromString(string value)
        {
            _valueID = Convert.ToInt32(value);
        }

        public override string ToString()
        {
            return ValueID.ToString();
        }
    }
}
