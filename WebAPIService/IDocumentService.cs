using System.ServiceModel;
using Model;

namespace WebAPIService
{
    [ServiceContract]
    interface IDocumentService
    {
        [OperationContract]
        Document GetDocument(long id);
    }
}
