using System;

namespace NewTablesLibrary
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SaveFieldAttribute : Attribute
    {
        private readonly string _fieldSaveName;

        public SaveFieldAttribute(string fieldSaveName)
        {
            _fieldSaveName = fieldSaveName;
        }

        public string FieldSaveName => _fieldSaveName;
    }
}
