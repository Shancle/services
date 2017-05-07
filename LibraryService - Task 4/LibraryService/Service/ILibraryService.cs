using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using LibraryService.Data;

namespace LibraryService.Service
{
    [ServiceContract]
    public interface ILibraryService
    {
        [WebInvoke(UriTemplate = "/", Method = "POST", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        void Add(Book book);

        [WebGet(UriTemplate = "/getById/{id}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Book GetById(string id);

        [WebGet(UriTemplate = "/getByAuthor/{author}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Book> GetByAuthor(string author);

        [WebGet(UriTemplate = "/", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Book> GetAllBooks();
    }
}