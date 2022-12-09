using NewTablesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Entities
{
    [Cell("Genre")]
    public class Genre : Cell
    {
        [SaveField("name")] private string _name = String.Empty;
        [SaveField("isClassic")] private bool _isClassic = false;

        private readonly OneToManyCollection<Genre, Book> _books;

        public Genre()
        {
            _books = new OneToManyCollection<Genre, Book>(this);
        }

        public string Name
        {
            get => _name; 
            set => _name = value;
        }

        public bool IsClassic
        {
            get => _isClassic;
            set => _isClassic = value;
        }

        public OneToManyCollection<Genre, Book> Books => _books;
    }
}
