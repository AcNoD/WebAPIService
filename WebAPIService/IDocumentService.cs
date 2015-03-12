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
        
        [OperationContract]
        //[WebInvoke(Method = "POST", BodyStyle=WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        //[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        long AddDocument(Document document);

        [OperationContract]
        string AddSimple(string document);
    }
}
