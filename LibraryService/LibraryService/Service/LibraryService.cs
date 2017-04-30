using System;
using System.Collections.Generic;
using System.Linq;
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

        private static readonly Dictionary<int, List<Tuple<Book, DateTime>>> UserBooks =
            new Dictionary<int, List<Tuple<Book, DateTime>>>
            {
                {
                    14, new List<Tuple<Book, DateTime>>
                    {
                        Tuple.Create(Books[1], DateTime.Now.AddDays(-40))
                    }
                }
            };

        private int ClientId { get; set; }
        private string ClientName { get; set; }
        private const int MaxBooksPerClient = 5;
        private ILibraryServiceCallback Callback => OperationContext.Current.GetCallbackChannel<ILibraryServiceCallback>();

        public void Enter(int id, string name)
        {
            ClientId = id;
            ClientName = name;
            if (!UserBooks.ContainsKey(id))
            {
                UserBooks.Add(id, new List<Tuple<Book, DateTime>>());
            }
        }

        public void Add(Book book)
        {
            Books.Add(book);
        }

        public Book GetById(int id)
        {
            var book = Books.Find(x => x.Id == id);

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

        public void TakeBook(int id)
        {
            var book = Books.Find(x => x.Id == id);
            if (!book.IsInLibrary)
            {
                throw new FaultException("Книга уже выдана");
            }
            if (UserBooks[ClientId].Count == MaxBooksPerClient)
            {
                Console.WriteLine($"{ClientName} слишком жадный");
                throw new FaultException("Не больше 5 книг в руки");
            }
            book.IsInLibrary = false;
            UserBooks[ClientId].Add(Tuple.Create(book, DateTime.Now));
            Console.WriteLine($"{ClientName} взял книгу {book.Name}");
        }

        public void ReturnBook(int id)
        {
            var book = Books.Find(x => x.Id == id);
            if (book.IsInLibrary)
            {
                throw new FaultException("Книга уже в библиотеке");
            }
            book.IsInLibrary = true;
            UserBooks[ClientId].RemoveAll(x => x.Item1.Id == book.Id);
            Console.WriteLine($"{ClientName} вернул книгу {book.Name}");
        }

        public void ApplyСhanges()
        {
            if (UserBooks[ClientId].Any(x => (DateTime.Now - x.Item2).TotalDays > DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)))
            {
                Console.WriteLine("Где книги и когда принесёшь?");
                Callback.OnCallback();
            }
            Console.WriteLine($"Клиент {ClientName} ушёл");
        }

        [OperationBehavior(ReleaseInstanceMode = ReleaseInstanceMode.AfterCall)]
        public void Exit()
        {
            Console.WriteLine($"Клиент {ClientName} сбежал");
        }

        public void Dispose()
        {
            Console.WriteLine($"{ClientName} завершил сессию");
        }
    }
}