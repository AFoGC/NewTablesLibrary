using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace NewTablesLibrary
{
    public abstract class Cell : INotifyPropertyChanged
    {
        [SaveField("id")] private int _id;

        public BaseTable ParentTable { get; internal set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID 
        {
            get => _id;
            internal set => _id = value;
        }

        internal void LoadCell(IEnumerator<string> enumerator, Command command)
        {
            Type thisType = this.GetType();
            while (command.GetNextCommand(enumerator))
            {
                if (command.IsCommand && command.IsMark == false)
                {
                    LoadField(command);
                    continue;
                }

                if (command.IsMark && command.FieldName == thisType.Name)
                    break;
            }
        }

        private void LoadField(Command command)
        {
            FieldInfo field;
            SaveFieldAttribute attribute;
            
            IEnumerable<(FieldInfo, SaveFieldAttribute)> fields = GetSaveFields();

            foreach ((FieldInfo, SaveFieldAttribute) item in fields)
            {
                field = item.Item1;
                attribute = item.Item2;

                if (attribute.FieldSaveName == command.FieldName)
                    SetFieldValue(field, command.FieldValue);
            }
        }

        private void SetFieldValue(FieldInfo field, string value)
        {
            Type manyToOneType = typeof(ManyToOne<,>);
            if (field.FieldType.IsGenericType)
            {
                if (field.FieldType.GetGenericTypeDefinition() == manyToOneType)
                {
                    FieldInfo idField = GetIdField(field.GetValue(this));
                    object idValue = field.GetValue(this);
                    idField.SetValue(idValue, Convert.ChangeType(value, idField.FieldType));
                }
            }
            else
            {
                field.SetValue(this, Convert.ChangeType(value, field.FieldType));
            }
        }

        internal void SaveCell(StringBuilder builder)
        {
            FieldInfo field;
            SaveFieldAttribute attribute;
            IEnumerable<(FieldInfo, SaveFieldAttribute)> fields = GetSaveFields();

            foreach ((FieldInfo, SaveFieldAttribute) value in fields)
            {
                field = value.Item1;
                attribute = value.Item2;

                SaveField(builder, field, attribute);
            }
        }

        private void SaveField(StringBuilder builder, FieldInfo field, SaveFieldAttribute attribute)
        {
            if (field.FieldType.HasGenericTypeDefenition(typeof(ManyToOne<,>)))
            {
                FieldInfo idField = GetIdField(field.GetValue(this));
                object idValue = idField.GetValue(field.GetValue(this));
                builder.AddCommand(attribute.FieldSaveName, idValue.ToString(), 2);
            }
            else
            {
                builder.AddCommand(attribute.FieldSaveName, field.GetValue(this).ToString(), 2);
            }
        }

        private FieldInfo GetIdField(object manyToOneInstance)
        {
            return StaticHelper.GetFieldsWithAttribute<ConnectionIdAttribute>(manyToOneInstance).First().Item1;
        }

        private IEnumerable<(FieldInfo, SaveFieldAttribute)> GetSaveFields()
        {
            return StaticHelper.GetFieldsWithAttribute<SaveFieldAttribute>(this);
        }

        internal void LoadConnections()
        {

        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
