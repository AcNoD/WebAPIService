using System.ServiceModel;
using System.ServiceModel.Web;
using Model;

namespace WebAPIService
{
    [ServiceContract]
    interface IDocumentService
    {
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Document GetDocument();
    }
}
