using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;

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
        public event Action TablesLoaded;
        public event Action TablesSaved;
        public event Action DataChanged;

        public int Count => _tables.Count;
        public int IndexOf(BaseTable item) => _tables.IndexOf(item);
        public bool Contains(BaseTable item) => _tables.Contains(item);

        private void TablesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public void Add<T>(Table<T> table) where T : Cell, new()
        {
            Type tableType = table.GetType();
            if (_tables.Any(x => x.GetType() == tableType))
                throw new NotSupportedException("This collection already has a table with this type");

            _tables.Add(table);
            table.ID = ++counter;
            table.ParentCollection = this;
        }

        public T GetTableByTableType<T>() where T : BaseTable
        {
            Type tableType = typeof(T);
            return _tables
                .Where(x => x.GetType() == tableType)
                .First() as T;
        }

        public Table<T> GetTableByDataType<T>() where T : Cell, new()
        {
            Type dataType = typeof(T);
            return _tables
                .Where(x => x.DataType == dataType)
                .First() as Table<T>;
        }

        public BaseTable GetTable(Type dataType)
        {
            return _tables.Where(x => x.DataType == dataType).First();
        }

        public void LoadFromFile(string filePath)
        {
            IEnumerable<string> lines = File.ReadAllLines(filePath);
            LoadFromStrings(lines);
        }

        public void LoadFromStrings(IEnumerable<string> lines)
        {
            Command command = new Command();
            IEnumerator<string> enumerator = lines.GetEnumerator();

            if(HasDocStart(lines, command) == false)
               throw new IOException();

            while (command.GetNextCommand(enumerator))
            {
                if (command.IsCommand == false)
                    continue;

                if (command.FieldName == "Table")
                {
                    LoadTable(enumerator, command);
                    continue;
                }

                if (command.IsMark && command.FieldName == "DocEnd")
                    break;
            }

            LoadConnections();
            TablesLoaded?.Invoke();
        }

        private void LoadTable(IEnumerator<string> enumerator, Command command)
        {
            CellAttribute attribute;
            foreach (BaseTable table in _tables)
            {
                attribute = StaticHelper.GetCellAttrbute(table);
                if (attribute.DataSaveName == command.FieldValue)
                {
                    table.LoadTable(enumerator, command);
                    continue;
                }
            }
        }

        private void LoadConnections()
        {
            foreach (BaseTable table in _tables)
                table.LoadConnections();
        }

        private bool HasDocStart(IEnumerable<string> lines, Command command)
        {
            bool hasDocStart = false;
            command.GetCommand(lines.First());

            if (command.FieldName == "DocStart")
                hasDocStart = true;

            return hasDocStart;
        }

        public void SaveTable(string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AddCommand("DocStart", 0);

            foreach (BaseTable table in _tables)
            {
                table.SaveTable(stringBuilder);
            }

            stringBuilder.AddCommand("DocEnd", 0);
            using (StreamWriter writer = new StreamWriter(filePath, false ,Encoding.UTF8))
            {
                writer.Write(stringBuilder.ToString());
            }

            TablesSaved?.Invoke();
        }

        internal void OnDataChanged()
        {
            DataChanged?.Invoke();
        }
    }
}
