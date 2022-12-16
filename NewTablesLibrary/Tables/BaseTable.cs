using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace NewTablesLibrary
{
    public abstract class BaseTable : INotifyPropertyChanged
    {
        [SaveField("id")] private int _id = 0;
        [SaveField("name")] private string _name = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID
        {
            get => _id;
            internal set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public TablesCollection ParentCollection { get; internal set; }

        public abstract Type DataType { get; }
        protected abstract IEnumerable<Cell> Cells { get; }

        internal abstract void LoadTable(IEnumerator<string> enumerator, Command command);
        internal abstract void SaveTable(StringBuilder builder);
        

        internal void LoadConnections()
        {
            if (HasDataTypeIdConnectionField())
                foreach (Cell cell in Cells)
                    cell.LoadConnections();
        }

        internal bool HasDataTypeIdConnectionField()
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            return DataType.GetAllFields(flags).Any(x => 
                x.FieldType.BaseType == typeof(BaseConnectionById));
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            OnDataChanged();
        }

        protected void OnDataChanged()
        {
            ParentCollection.OnDataChanged();
        }

        internal abstract void InvokeOnLoaded();
    }
}
