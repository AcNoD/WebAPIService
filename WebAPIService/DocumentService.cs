using System.ServiceModel;
using Model;
using NLog;

namespace WebAPIService
{
    /// <summary>
    ///  Contains API to manage documents collection
    /// </summary>
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)] 
    class DocumentService : IDocumentService
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get documents from storage by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Document GetDocument(string id)
        {            
            Logger.Trace("GetDocument, id={0}", id);
            return DocumentStorage.Storage.GetDocument(long.Parse(id));
        }

        /// <summary>
        /// Adds new document to storage
        /// </summary>
        /// <param name="document">New document</param>
        /// <returns>Identifier of saved document</returns>
        public long AddDocument(Document document)
        {
            Logger.Trace("AddDocument");
            return DocumentStorage.Storage.AddDocument(document);
        }

        /// <summary>
        /// Modifies incoming string
        /// </summary>
        /// <param name="value">Incoming string</param>
        /// <returns>Modified string</returns>
        public string GetData(string value)
        {
            Logger.Trace("GetData, value={0}", value);
            return "You have sent " + value;
        }

        /// <summary>
        /// Modifies incoming string
        /// </summary>
        /// <param name="value">Incoming string</param>
        /// <returns>Modified string</returns>
        public string PostData(string value)
        {
            Logger.Trace("PostData, value={0}", value);
            return "You have sent " + value;
        }

        /// <summary>
        /// Send incoming string in a queue
        /// </summary>
        /// <param name="value">Incoming string</param>
        public void SendToQueue(string value)
        {
            Logger.Trace("SendToQueue, value={0}", value);
            Msmq.SendMessage(value);
        }
    }
}
