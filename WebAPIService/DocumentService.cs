using System.ServiceModel;
using Model;
using NLog;

namespace WebAPIService
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)] 
    class DocumentService : IDocumentService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Document GetDocument(string id)
        {            
            Logger.Trace("GetDocument, id={0}", id);
            return DocumentStorage.Storage.GetDocument(long.Parse(id));
        }

        public long AddDocument(Document document)
        {
            Logger.Trace("AddDocument");
            return DocumentStorage.Storage.AddDocument(document);
        }

        public string GetData(string value)
        {
            Logger.Trace("GetData, value={0}", value);
            return "You have sent " + value;
        }

        public string PostData(string value)
        {
            Logger.Trace("PostData, value={0}", value);
            return "You have sent " + value;
        }

        public void SendToQueue(string value)
        {
            Logger.Trace("SendToQueue, value={0}", value);
            Msmq.SendMessage(value);
        }
    }
}
