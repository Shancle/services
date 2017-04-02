using System.Collections.Generic;
using System.ServiceModel;
using LibraryService.Data;

namespace LibraryService.Service
{
    [ServiceContract]
    public interface ILibraryService
    {
        [OperationContract]
        void Add(Book book);

        [OperationContract]
        Book GetById(int id);

        [OperationContract]
        List<Book> GetByAuthor(string author);

        [OperationContract]
        void TakeBook(int id);

        [OperationContract]
        void ReturnBook(int id);
    }
}