using NewTablesLibrary;

namespace ConsoleTest
{
    public static class ConnectionsFields
    {
        public static void TestConnections()
        {
            Book book1 = new Book() { Name = "Clean Code" };
            Book book2 = new Book() { Name = "CLR via C#" };
            Genre genre = new Genre() { Name = "Programng books" };

            genre.Books.Add(book1);
            genre.Books.Add(book2);

            foreach (Book book in genre.Books)
            {
                Console.WriteLine(book.Name);
                Console.WriteLine(book.Genre.Value.Name);
                Console.WriteLine();
            }

            genre.Books.Remove(book1);
            genre.Books.Remove(book2);
        }

        public class Book : Cell
        {
            public string Name { get; set; }
            public ManyToOne<Book, Genre> Genre { get; private set; }

            public Book()
            {
                Genre = new ManyToOne<Book, Genre>(this);
            }
        }

        public class Genre : Cell
        {
            public string Name { get; set; }
            public OneToManyCollection<Genre, Book> Books { get; private set; }

            public Genre()
            {
                Books = new OneToManyCollection<Genre, Book>(this);
            }
        }
    }
}
