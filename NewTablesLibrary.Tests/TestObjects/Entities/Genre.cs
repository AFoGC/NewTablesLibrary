namespace NewTablesLibrary.Tests.TestObjects.Entities
{
    [Cell("Genre")]
    public class Genre : Cell
    {
        [SaveField("name")] private string _name = String.Empty;
        [SaveField("isPriorityGenre")] private bool _isPriorityGenre = false;

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

        public bool IsPriority
        {
            get => _isPriorityGenre;
            set => _isPriorityGenre = value;
        }

        public OneToManyCollection<Genre, Book> Books => _books;
    }
}
