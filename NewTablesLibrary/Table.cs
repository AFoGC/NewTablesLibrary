using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTablesLibrary
{
    public class Table<T> : BaseTable
    {
        private ObservableCollection<T> _cells = new ObservableCollection<T>();
    }
}
