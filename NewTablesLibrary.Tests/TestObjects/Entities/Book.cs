namespace NewTablesLibrary.Tests.TestObjects.Entities
{
    [Cell("Book")]
    public class Book : Cell
    {
        [SaveField("name")] private string _name = string.Empty;
        [SaveField("author")] private string _author = string.Empty;
        [SaveField("date")] private DateTime _date = new DateTime();

        [SaveField("categoryId")] 
        private readonly ManyToOne<Book, Category> _category;

        [SaveField("genreId")] 
        private readonly ManyToOne<Book, Genre> _genre;

        private readonly OneToOne<Book, BookDescription> _description;

        public Book()
        {
            _category = new ManyToOne<Book, Category>(this);
            _genre = new ManyToOne<Book, Genre>(this);
            _description = new OneToOne<Book, BookDescription>(this);
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

        public Category Category
        {
            get => _category.Value;
            set => _category.SetValue(value);
        }
        
        public Genre Genre
        {
            get => _genre.Value; 
            set => _genre.SetValue(value);
        }

        public BookDescription Description
        {
            get => _description.Value;
            set => _description.SetValue(value);
        }
    }
}
