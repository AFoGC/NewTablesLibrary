using NewTablesLibrary.Tests.TestObjects;
using NewTablesLibrary.Tests.TestObjects.Tables;
using Xunit;

namespace NewTablesLibrary.Tests
{
    public class ConnectionTests
    {
        TablesCollection collection;
        BooksTable Books => collection.GetTableByTableType<BooksTable>();
        GenresTable Genres => collection.GetTableByTableType<GenresTable>();
        CategoriesTable Categories => collection.GetTableByTableType<CategoriesTable>();
        BookDescriptionsTable Descriptions => collection.GetTableByTableType<BookDescriptionsTable>();


        [Fact]
        public void OneToOne_SetValue_WithoutConnection()
        {
            collection = InitTablesCollection.InitialzeTablesCollectionWithData();
            var book = Books[0];
            var description = Descriptions[0];

            book.Description = description;

            Assert.Equal(description, book.Description);
            Assert.Equal(book, description.Book);
        }

        [Fact]
        public void OneToOne_SetValue_WithConnection()
        {
            collection = InitTablesCollection.InitialzeTablesCollectionWithData();
            var book1 = Books[0];
            var book2 = Books[1];
            var description = Descriptions[0];

            book1.Description = description;
            book2.Description = description;

            Assert.Equal(description, book2.Description);
            Assert.Equal(book2, description.Book);
            Assert.Null(book1.Description);
        }

        [Fact]
        public void OneToOne_SetValue_WithConnectionThroughBook()
        {
            collection = InitTablesCollection.InitialzeTablesCollectionWithData();
            var book1 = Books[0];
            var book2 = Books[1];
            var description = Descriptions[0];

            description.Book = book1;
            description.Book = book2;

            Assert.Equal(description, book2.Description);
            Assert.Equal(book2, description.Book);
            Assert.Null(book1.Description);
        }

        [Fact]
        public void OneToMany_Add()
        {
            collection = InitTablesCollection.InitialzeTablesCollectionWithData();
            var book1 = Books[0];
            var book2 = Books[1];
            var genre = Genres[0];

            genre.Books.Add(book1);
            genre.Books.Add(book2);

            Assert.Equal(2, genre.Books.Count);
            Assert.Equal(book1.Genre, genre);
            Assert.Equal(book2.Genre, genre);
        }

        [Fact]
        public void ManyToOne_SetValue()
        {
            collection = InitTablesCollection.InitialzeTablesCollectionWithData();
            var book1 = Books[0];
            var book2 = Books[1];
            var genre = Genres[0];

            book1.Genre = genre;
            book2.Genre = genre;

            Assert.Equal(2, genre.Books.Count);
            Assert.Equal(book1, genre.Books[0]);
            Assert.Equal(book2, genre.Books[1]);
        }
    }
}
