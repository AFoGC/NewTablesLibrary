using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

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
            while (StaticHelper.NextCommand(enumerator, command))
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
            
            IEnumerable<(FieldInfo, SaveFieldAttribute)> fields = 
                StaticHelper.GetFieldsWithAttribute<SaveFieldAttribute>(this);

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
                    idField.SetValue(this, Convert.ChangeType(value, idField.FieldType));
                }
            }
            else
            {
                field.SetValue(this, Convert.ChangeType(value, field.FieldType));
            }
        }

        private FieldInfo GetIdField(object manyToOneInstance)
        {
            return StaticHelper.GetFieldsWithAttribute<ConnectionIdAttribute>(manyToOneInstance).First().Item1;
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
