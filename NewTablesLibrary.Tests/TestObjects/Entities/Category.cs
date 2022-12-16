namespace NewTablesLibrary.Tests.TestObjects.Entities
{
    [Cell("Category")]
    public class Category : Cell
    {
        [SaveField("name")] private string _name = String.Empty;
        [SaveField("description")] private string _description = String.Empty;

        private readonly OneToManyCollection<Category, Book> _books;

        public Category()
        {
            _books = new OneToManyCollection<Category, Book>(this);
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public OneToManyCollection<Category, Book> Books => _books;
    }
}
