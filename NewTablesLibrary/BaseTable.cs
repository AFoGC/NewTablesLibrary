using System;

namespace NewTablesLibrary
{
    public abstract class BaseTable
    {
        [SaveField("id")] private int _id = 0;
        [SaveField("name")] private string _name = string.Empty;

        public int ID
        {
            get => _id;
            internal set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public TablesCollection ParentCollection { get; internal set; }

        public abstract Type DataType { get; }
    }
}
