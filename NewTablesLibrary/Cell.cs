using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NewTablesLibrary
{
    public class Cell : INotifyPropertyChanged
    {
        private int id;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID 
        {
            get => id;
            set
            {
                id = value;
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
