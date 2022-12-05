using System;

namespace NewTablesLibrary
{
    public abstract class BaseTable
    {
        [SaveField("id")] private int _id = 0;
        [SaveField("name")] private String _name = String.Empty;

        private TablesCollection _tablesCollection;

        public int ID
        {
            get => _id;
            internal set => _id = value;
        }

        public String Name
        {
            get => _name;
            set => _name = value;
        }

        public TablesCollection TablesCollection
        {
            get => _tablesCollection;
            internal set => _tablesCollection = value;
        }

        public abstract Type DataType { get; }
    }
}
