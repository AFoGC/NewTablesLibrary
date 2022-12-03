using System;
using System.Collections.ObjectModel;

namespace NewTablesLibrary
{
    public class Table<T> : BaseTable
    {
        private ObservableCollection<T> _cells;

        public Table()
        {
            _cells = new ObservableCollection<T>();
        }

        public override Type DataType => typeof(T);
    }
}
