using System;
using System.Collections.Generic;
using LibraryService.Data;

namespace LibraryService.Service
{
    public class LibraryService : ILibraryService
    {
        private static readonly List<Book> Books = new List<Book>
        {
            new Book(1, "Война и мир", "Лев Николаевич Толстой", 1869, BookType.FictionBook),
            new Book(2, "From Big Data to Extreme Data", "Grady Booch", 2014, BookType.ResearchArticle),
            new Book(3, "Nonlinear Analysis: Real World Applications", "Joachim Escher", 2015, BookType.Journal)
        }; 

        public void Add(Book book)
        {
            Books.Add(book);
        }

        public Book GetById(int id)
        {
            return Books.Find(book => book.Id == id);
        }

        public List<Book> GetByAuthor(string author)
        {
            return Books.FindAll(book => book.Author == author);
        }

        public void TakeBook(int id)
        {
            var book = Books.Find(x => x.Id == id);
            if (!book.IsInLibrary)
            {
                throw new InvalidOperationException("Книга уже выдана");
            }
            book.IsInLibrary = false;
        }

        public void ReturnBook(int id)
        {
            var book = Books.Find(x => x.Id == id);
            if (book.IsInLibrary)
            {
                throw new InvalidOperationException("Книга уже в библиотеке");
            }
            book.IsInLibrary = true;
        }
    }
}