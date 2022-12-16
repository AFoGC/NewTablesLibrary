namespace NewTablesLibrary.Tests.TestObjects.Entities
{

    [Cell("BookDescription")]
    public class BookDescription : Cell
    {
        [SaveField("text")] private string _descriptionText;
        [SaveField("mark")] private byte _mark;

        [SaveField("BookId")] private readonly OneToOne<BookDescription, Book> _book;

        public BookDescription()
        {
            _book = new OneToOne<BookDescription, Book>(this);
        }

        public string DescriptonText
        {
            get => _descriptionText;
            set => _descriptionText = value;
        }

        public byte Mark
        {
            get => _mark;
            set => _mark = value;
        }

        public Book Book
        {
            get => _book.Value;
            set => _book.SetValue(value);
        }
    }
}
