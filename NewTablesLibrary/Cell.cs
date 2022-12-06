using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewTablesLibrary
{
    public abstract class Cell : INotifyPropertyChanged
    {
        [SaveField("id")] private int _id;

        public BaseTable ParentTable { get; internal set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID 
        {
            get => _id;
            internal set => _id = value;
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
