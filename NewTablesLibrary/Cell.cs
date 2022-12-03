using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewTablesLibrary
{
    public class Cell : INotifyPropertyChanged
    {
        [SaveField("id")] private int _id;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID 
        {
            get => _id;
            internal set
            {
                _id = value;
            }
        }

        

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);

            /*
            if (this.ParentTable != null)
            {
                ((BaseTable)ParentTable).InCollectionChanged();
            }
            */
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
