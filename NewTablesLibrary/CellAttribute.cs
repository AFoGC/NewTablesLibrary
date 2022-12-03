using System;

namespace NewTablesLibrary
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CellAttribute : Attribute
    {
        private readonly string _dataSaveName;

        public CellAttribute(string saveName)
        {
            _dataSaveName = saveName;
        }

        public string DataSaveName => _dataSaveName;
    }
}
