using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NewTablesLibrary
{
    public class OneToOne<ParentT, T> : BaseConnectionById
                                        where T : Cell, new() where ParentT : Cell, new()
    {
        private readonly FieldInfo _oneToManyField;
        private readonly ParentT _parent;

        [ConnectionId]
        private int _valueID;
        private T _value;

        public OneToOne(ParentT parent)
        {
            _parent = parent;
            _oneToManyField = InitOneToOneFieldInfo();
        }

        public T Value => _value;
        public int ValueID => _valueID;

        public event Action ConnectionChanged;

        public void SetValue(T value)
        {
            GetOneToOneInstance(_value).InnerSetValue(null);
            InnerSetValue(value);
            GetOneToOneInstance(_value).InnerSetValue(_parent);
        }

        private void InnerSetValue(T value)
        {
            _value = value;
            if (_value != null)
                _valueID = _value.ID;
            else
                _valueID = 0;

            ConnectionChanged?.Invoke();
        }

        internal override void LoadConnection()
        {
            T target = GetTargetElement();

            if (target != null)
                GetOneToOneInstance(target).InnerSetValue(_parent);
        }

        private T GetTargetElement()
        {
            TablesCollection collection = _parent.ParentTable.ParentCollection;
            Table<T> table = collection.GetTableByDataType<T>();
            return table.Where(x => x.ID == this.ValueID).FirstOrDefault();
        }

        private OneToOne<T, ParentT> GetOneToOneInstance(T value)
        {
            return _oneToManyField.GetValue(value) as OneToOne<T, ParentT>;
        }

        private FieldInfo InitOneToOneFieldInfo()
        {
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic);

            return fields
                .Where(x => x.FieldType == typeof(OneToOne<T, ParentT>))
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
