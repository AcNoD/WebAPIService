using System.ServiceModel;
using System.ServiceModel.Web;
using Model;

namespace WebAPIService
{
    [ServiceContract]
    interface IDocumentService
    {
        [WebGet]
        [OperationContract]
        Document GetDocument();
    }
}
