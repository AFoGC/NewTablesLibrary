using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
        protected abstract IEnumerable<Cell> Cells { get; }

        internal abstract void LoadTable(IEnumerator<string> enumerator, Command command);
        internal abstract void SaveTable(StringBuilder builder);
        

        internal void LoadConnections()
        {
            if(HasDataTypeIdConnectionField())
                foreach (Cell cell in Cells)
                    cell.LoadConnections();
        }

        internal bool HasDataTypeIdConnectionField()
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            return DataType.GetAllFields(flags).Any(x => 
                x.FieldType.BaseType == typeof(BaseConnectionById));
        }
    }
}
