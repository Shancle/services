using System.Collections.Generic;
using System.ServiceModel;
using LibraryService.Data;

namespace LibraryService.Service
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ILibraryServiceCallback))]
    public interface ILibraryService
    {
        [OperationContract(IsInitiating = true)]
        void Enter(int id, string name);

        [OperationContract(IsInitiating = false)]
        void Add(Book book);

        [OperationContract(IsInitiating = false)]
        Book GetById(int id);

        [OperationContract(IsInitiating = false)]
        List<Book> GetByAuthor(string author);

        [OperationContract(IsInitiating = false)]
        void TakeBook(int id);

        [OperationContract(IsInitiating = false)]
        void ReturnBook(int id);

        [OperationContract(IsInitiating = false, IsTerminating = true)]
        void ApplyСhanges();

        [OperationContract(IsInitiating = false, IsTerminating = true, IsOneWay = true)]
        void Exit();
    }
}