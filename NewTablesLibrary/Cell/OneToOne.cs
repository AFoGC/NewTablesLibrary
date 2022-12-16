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
            OneToOne<T, ParentT> targetOneToOne = GetTargetOneToOneInstance(_value);
            if (targetOneToOne != null)
            {
                OneToOne<ParentT, T> parentOneToOne = GetParentOneToOneInstance(targetOneToOne.Value);
                parentOneToOne?.InnerSetValue(null);
            }
            
            targetOneToOne?.InnerSetValue(null);

            InnerSetValue(value);
            GetTargetOneToOneInstance(value)?.InnerSetValue(_parent);
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
                GetTargetOneToOneInstance(target).SetValue(_parent);
        }

        private T GetTargetElement()
        {
            TablesCollection collection = _parent.ParentTable.ParentCollection;
            Table<T> table = collection.GetTableByDataType<T>();
            return table.Where(x => x.ID == this.ValueID).FirstOrDefault();
        }

        private OneToOne<T, ParentT> GetTargetOneToOneInstance(T value)
        {
            if (value != null)
                return _oneToManyField.GetValue(value) as OneToOne<T, ParentT>;
            else 
                return null;
        }

        private OneToOne<ParentT, T> GetParentOneToOneInstance(ParentT value)
        {
            if (value != null)
                return _oneToManyField.GetValue(value) as OneToOne<ParentT, T>;
            else
                return null;
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
