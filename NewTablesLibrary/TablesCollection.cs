using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace NewTablesLibrary
{
    public class TablesCollection : INotifyCollectionChanged
    {
        private int counter = 0;
        private ObservableCollection<BaseTable> _tables;

        public TablesCollection()
        {
            _tables = new ObservableCollection<BaseTable>();
            _tables.CollectionChanged += TablesChanged;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void TablesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }
    }
}
