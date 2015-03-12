using System.Collections.Generic;
using System.Web.Http;
using Model;

namespace WebAPIService.WebAPI
{
    public class DocumentsController : ApiController
    {
        readonly Document[] _documents = new []  
        {  
            new Document { Id = 1, Name = "DocOne", Content = "ContentOne"},  
            new Document { Id = 2, Name = "DocTwo", Content = "ContentTwo" },  
            new Document { Id = 3, Name = "DocThree", Content = "ContentThree" }  
        };

        public IEnumerable<Document> GetAllDocuments()
        {
            return _documents;
        }
    }
}
