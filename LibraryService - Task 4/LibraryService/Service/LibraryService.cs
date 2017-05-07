using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using LibraryService.Data;

namespace LibraryService.Service
{
    public class LibraryService : ILibraryService
    {
        private static readonly List<Book> Books = new List<Book>
        {
            new Book(1, "Война и мир", "Лев Николаевич Толстой", 1869, BookType.FictionBook),
            new Book(2, "From Big Data to Extreme Data", "Grady Booch", 2014, BookType.ResearchArticle),
            new Book(3, "Nonlinear Analysis: Real World Applications", "Joachim Escher", 2015, BookType.Journal),
            new Book(4, "сложное название 1", "Автор 1", 2015, BookType.Journal),
            new Book(5, "сложное название 2", "Автор 2", 2016, BookType.Chronicle),
            new Book(6, "сложное название 3", "Автор 3", 2017, BookType.ResearchArticle)
        };

        public void Add(Book book)
        {
            Books.Add(book);
        }

        public Book GetById(string id)
        {
            var book = Books.Find(x => x.Id == int.Parse(id));

            if (book == null)
            {
                throw new FaultException("Книга не найдена");
            }

            return book;
        }

        public List<Book> GetByAuthor(string author)
        {
            var books = Books.FindAll(book => book.Author == author);

            if (books == null)
            {
                throw new FaultException("Книги не найдены");
            }

            return books;
        }

        public List<Book> GetAllBooks()
        {
            return Books;
        } 
    }
}