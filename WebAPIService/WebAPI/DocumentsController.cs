using System.Collections.Generic;
using System.Web.Http;
using Model;

namespace WebAPIService.WebAPI
{
    public class DocumentsController : ApiController
    {
        readonly Document[] _documents = new []  
        {  
            new Document { Id = 1, Name = "DocOne", MyContent = "ContentOne"},  
            new Document { Id = 2, Name = "DocTwo", MyContent = "ContentTwo" },  
            new Document { Id = 3, Name = "DocThree", MyContent = "ContentThree" }  
        };

        public IEnumerable<Document> GetAllDocuments()
        {
            return _documents;
        }
    }
}
