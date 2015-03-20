using System.Collections.Generic;
using System.Web.Http;
using Model;

namespace WebAPIService.WebAPI
{
    public class DocumentsController : ApiController
    {
        public IEnumerable<Document> GetAllDocuments()
        {
            return DocumentStorage.Storage.GetAllDocumets();
        }

        public long AddDocument([FromBody]Document document)
        {
            return DocumentStorage.Storage.AddDocument(document);
        }
    }
}
