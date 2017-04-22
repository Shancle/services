using System;
using System.Collections.Generic;
using System.ServiceModel;
using LibraryService.Data;

namespace LibraryService.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class LibraryService : ILibraryService, IDisposable
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
        private static readonly Dictionary<int, List<Book>> UserBooks = new Dictionary<int, List<Book>>(); 

        private int ClientId { get; set; }
        private string ClientName { get; set; }
        private const int MaxBooksPerClient = 5;

        public void Enter(int id, string name)
        {
            ClientId = id;
            ClientName = name;
            if (!UserBooks.ContainsKey(id))
            {
                UserBooks.Add(id, new List<Book>());
            }
        }

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
            if (UserBooks[ClientId].Count == MaxBooksPerClient)
            {
                Console.WriteLine($"{ClientName} слишком жадный");
                return; ; // Todo: fault in next lesson
            }
            book.IsInLibrary = false;
            UserBooks[ClientId].Add(book);
            Console.WriteLine($"{ClientName} взял книгу {book.Name}");
        }

        public void ReturnBook(int id)
        {
            var book = Books.Find(x => x.Id == id);
            if (book.IsInLibrary)
            {
                throw new InvalidOperationException("Книга уже в библиотеке");
            }
            book.IsInLibrary = true;
            UserBooks[ClientId].Remove(book);
            Console.WriteLine($"{ClientName} вернул книгу {book.Name}");
        }

        [OperationBehavior(ReleaseInstanceMode = ReleaseInstanceMode.AfterCall)]
        public void ApplyСhanges()
        {
            Console.WriteLine($"Клиент {ClientName} ушёл");
        }

        public void Dispose()
        {
            Console.WriteLine($"{ClientName} завершил сессию");
        }
    }
}