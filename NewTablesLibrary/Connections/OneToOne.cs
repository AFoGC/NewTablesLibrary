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
        private readonly FieldInfo _oneToOneTargetField;
        private readonly FieldInfo _oneToOneParentField;
        private readonly ParentT _parent;

        [ConnectionId]
        private int _valueID;
        private T _value;

        public OneToOne(ParentT parent)
        {
            _parent = parent;
            _oneToOneTargetField = InitTargetOneToOneFieldInfo();
            _oneToOneParentField = InitParentOneToOneFieldInfo();
        }

        public T Value => _value;
        public int ValueID => _valueID;

        public event Action ConnectionChanged;

        public void SetValue(T value)
        {
            OneToOne<T, ParentT> targetNew = null;
            OneToOne<T, ParentT> targetOld = null;

            if (value != null)
            {
                targetNew = GetTargetOneToOneInstance(value);
                if (targetNew.Value != null)
                    GetParentOneToOneInstance(targetNew.Value).InnerSetValue(null);
            }

            if (_value != null)
            {
                targetOld = GetTargetOneToOneInstance(_value);
                if (targetOld.Value != null)
                    GetParentOneToOneInstance(targetOld.Value).InnerSetValue(null);
            }

            InnerSetValue(value);
            targetOld?.InnerSetValue(null);
            targetNew?.InnerSetValue(_parent);
        }

        private void InnerSetValue(T value)
        {
            _value = value;

            if (value != null)
                _valueID = value.ID;

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
                return _oneToOneTargetField.GetValue(value) as OneToOne<T, ParentT>;
            else 
                return null;
        }

        private OneToOne<ParentT, T> GetParentOneToOneInstance(ParentT value)
        {
            if (value != null)
                return _oneToOneParentField.GetValue(value) as OneToOne<ParentT, T>;
            else
                return null;
        }

        private FieldInfo InitTargetOneToOneFieldInfo()
        {
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic);

            return fields
                .Where(x => x.FieldType == typeof(OneToOne<T, ParentT>))
                .First();
        }

        private FieldInfo InitParentOneToOneFieldInfo()
        {
            Type type = typeof(ParentT);
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic);

            return fields
                .Where(x => x.FieldType == typeof(OneToOne<ParentT, T>))
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
