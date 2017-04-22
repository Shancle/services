using System;
using LibraryClient.LibraryService;

namespace LibraryClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new LibraryServiceClient();

            client.Enter(1, "Client1");

            var bookFromService = client.GetById(1);
            Console.WriteLine(bookFromService.Name);

            client.Add(new Book { Author = bookFromService.Author, BookType = BookType.Journal, PublicationYear = 2017, Id = 100500, Name = "Новая книга"});

            foreach (var book in client.GetByAuthor(bookFromService.Author))
            {
                Console.WriteLine($"Автор: {book.Author} Название: {book.Name} Год выпуска: {book.PublicationYear}");
                if (book.BookType == BookType.FictionBook)
                {
                    Console.WriteLine("Это художественная книга");
                }
            }

            client.TakeBook(1);
            client.TakeBook(2);
            client.TakeBook(3);
            client.TakeBook(4);
            client.TakeBook(5);
            client.TakeBook(6);

            client.ApplyСhanges();
            client.Close();

            client = new LibraryServiceClient();

            client.Enter(1, "Client1");

            client.TakeBook(6);

            try
            {
                client.TakeBook(1);
            }
            catch (Exception)
            {
                Console.WriteLine("книга была взята");
            }
            Console.ReadLine();
        }
    }
}
