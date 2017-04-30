using System.ServiceModel;

namespace LibraryService.Service
{
    [ServiceContract]
    public interface ILibraryServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnCallback();
    }
}