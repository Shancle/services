using System;
using LibraryClient.LibraryService;

namespace LibraryClient
{
    class ClientCallback : ILibraryServiceCallback
    {
        public void OnCallback()
        {
            Console.WriteLine("Какие такие книги?");
        }
    }
}
