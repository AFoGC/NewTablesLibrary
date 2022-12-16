using NewTablesLibrary.Tests.TestObjects.Entities;
using NewTablesLibrary.Tests.TestObjects.Tables;

namespace NewTablesLibrary.Tests.TestObjects
{
    public static class InitTablesCollection
    {
        public static TablesCollection InitializeTablesCollection()
        {
            TablesCollection collection = new TablesCollection();

            collection.Add(new BooksTable());
            collection.Add(new GenresTable());
            collection.Add(new CategoriesTable());
            collection.Add(new BookDescriptionsTable());

            return collection;
        }

        public static TablesCollection InitialzeTablesCollectionWithData()
        {
            TablesCollection collection = InitializeTablesCollection();

            BooksTable books = collection.GetTableByTableType<BooksTable>();
            GenresTable genres = collection.GetTableByTableType<GenresTable>();
            CategoriesTable categories = collection.GetTableByTableType<CategoriesTable>();
            BookDescriptionsTable descriptions = collection.GetTableByTableType<BookDescriptionsTable>();

            books.Add(new Book { Name = "MagnumOpus1", Author = "SuperMind", Date = new DateTime(2001, 1, 1) });
            books.Add(new Book { Name = "Manga1", Author = "Mangaka", Date = new DateTime(2021, 9, 21) });
            books.Add(new Book { Name = "Manga2", Author = "Mangaka", Date = new DateTime(2019, 11, 2) });
            books.Add(new Book { Name = "Teaching Book", Date = new DateTime(2018, 5, 2) });

            genres.Add(new Genre { Name = "Educaton", IsPriority = true });
            genres.Add(new Genre { Name = "Manga", IsPriority = false });

            categories.Add(new Category { Name = "For teenagers", Description = "AHAHA"});

            descriptions.Add(new BookDescription { DescriptonText = "Briliiant Book!", Mark = 5 });
            descriptions.Add(new BookDescription { DescriptonText = "A little bit boring boook", Mark = 3 });
            descriptions.Add(new BookDescription { DescriptonText = "Worst book ever!!!", Mark = 1 });

            return collection;
        }

        public static TablesCollection InitialzeTablesCollectionWithConnectedData()
        {
            TablesCollection collection = InitialzeTablesCollectionWithData();

            BooksTable books = collection.GetTableByTableType<BooksTable>();
            GenresTable genres = collection.GetTableByTableType<GenresTable>();
            CategoriesTable categories = collection.GetTableByTableType<CategoriesTable>();
            BookDescriptionsTable descriptions = collection.GetTableByTableType<BookDescriptionsTable>();

            books[0].Description = descriptions[0];
            books[2].Description = descriptions[1];
            books[3].Description = descriptions[2];

            books[0].Genre = genres[0];
            books[1].Genre = genres[1];
            books[2].Genre = genres[1];
            books[3].Genre = genres[0];

            books[1].Category = categories[0];
            books[2].Category = categories[0];
            books[3].Category = categories[0];

            books[0].Description = descriptions[0];
            books[2].Description = descriptions[1];
            books[3].Description = descriptions[2];

            return collection;
        }
    }
}
