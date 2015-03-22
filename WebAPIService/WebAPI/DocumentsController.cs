using System.Collections.Generic;
using System.Web.Http;
using Model;

namespace WebAPIService.WebAPI
{
    /// <summary>
    /// WebAPI Controller
    /// Contains API to manage documents collection
    /// </summary>
    public class DocumentsController : ApiController
    {
        /// <summary>
        /// Get all documents from storage
        /// </summary>
        /// <returns>Documents collection</returns>
        public IEnumerable<Document> GetAllDocuments()
        {
            return DocumentStorage.Storage.GetAllDocumets();
        }

        /// <summary>
        /// Add new document to storage
        /// </summary>
        /// <param name="document">Document</param>
        /// <returns>Identifier of saved document</returns>
        public long AddDocument([FromBody]Document document)
        {
            return DocumentStorage.Storage.AddDocument(document);
        }
    }
}
