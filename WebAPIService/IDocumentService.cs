using System.ServiceModel;
using System.ServiceModel.Web;
using Model;

namespace WebAPIService
{
    [ServiceContract]
    interface IDocumentService
    {
        [WebGet(UriTemplate = "GetDocument/{id}")]
        [OperationContract]
        Document GetDocument(string id);
        
        [OperationContract]
        long AddDocument(Document document);

        [WebGet(UriTemplate = "GetData/{value}")]
        [OperationContract]
        string GetData(string value);

        [OperationContract]
        string PostData(string value);

        [OperationContract]
        void SendToQueue(string value);
    }
}
