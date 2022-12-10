﻿using ConsoleTest.Entities;
using ConsoleTest.Tables;
using NewTablesLibrary;

namespace ConsoleTest
{
    public static class TablesTest
    {
        static TablesCollection collection;

        public static void SaveTest()
        {
            SaveInit();
            collection.SaveTable("text.tl");
        }

        private static void SaveInit()
        {
            collection = new TablesCollection();
            BooksTable books = new BooksTable();
            GenresTable genres = new GenresTable();
            CategoriesTable categories = new CategoriesTable();

            collection.Add(books);
            collection.Add(genres);
            collection.Add(categories);

            Book book1 = new Book() { Author = "Ja", Name = "Genii 1" };
            Book book2 = new Book() { Author = "Ty", Name = "Atlant Raspravil plechi" };
            Book book3 = new Book() { Name = "TestBook", Date = DateTime.Today };

            Genre genre1 = new Genre() { Name = "Vopros", IsClassic = true };
            Genre genre2 = new Genre() { Name = "Test" };

            Category category1 = new Category() { Name = "TestCat", Description = "AHAHAH" };

            books.Add(book1);
            books.Add(book2);
            books.Add(book3);
            genres.Add(genre1);
            genres.Add(genre2);
            categories.Add(category1);

            genre1.Books.Add(book1);
            genre1.Books.Add(book2);
            genre2.Books.Add(book3);

            category1.Books.Add(book3);
        }

        public static void LoadTest()
        {
            LoadInit();
            collection.LoadFromFile("text.tl");
            collection.SaveTable("textOut.tl");

            foreach (Book book in collection.GetTableByDataType<Book>())
            {
                Console.Write(book.Name);
                Console.Write(": ");
                Console.Write(book.Genre?.Name);
                Console.Write(": ");
                Console.Write(book.Category?.Name);
                Console.WriteLine(";");
            }
        }

        private static void LoadInit()
        {
            collection = new TablesCollection();
            BooksTable books = new BooksTable();
            GenresTable genres = new GenresTable();
            CategoriesTable categories = new CategoriesTable();

            collection.Add(books);
            collection.Add(genres);
            collection.Add(categories);
        }
    }
}
