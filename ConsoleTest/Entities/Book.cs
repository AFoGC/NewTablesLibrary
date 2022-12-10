using NewTablesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Entities
{
    [Cell("Book")]
    public class Book : Cell
    {
        [SaveField("name")] private string _name = string.Empty;
        [SaveField("author")] private string _author = string.Empty;
        [SaveField("date")] private DateTime _date = new DateTime();

        [SaveField("categoryId")] private readonly ManyToOne<Book, Category> _category;
        [SaveField("genreId")] private readonly ManyToOne<Book, Genre> _genre;

        public Book()
        {
            _category = new ManyToOne<Book, Category>(this);
            _genre = new ManyToOne<Book, Genre>(this);
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Author
        {
            get => _author;
            set => _author = value;
        }

        public DateTime Date
        {
            get => _date; 
            set => _date = value;
        }

        public Category Category => _category.Value;
        public Genre Genre => _genre.Value;
    }
}
