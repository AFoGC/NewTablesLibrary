using NewTablesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public class Book : Cell
        {
            public string Name { get; set; }
            public ManyToOne<Genre, Book> Genre { get; private set; }

            public Book()
            {
                Genre = new ManyToOne<Genre, Book>(this);
            }
        }

        public class Genre : Cell
        {
            public string Name { get; set; }
            public OneToManyCollection<Book, Genre> Books { get; private set; }

            public Genre()
            {
                Books = new OneToManyCollection<Book, Genre>(this);
            }
        }
    }
}
