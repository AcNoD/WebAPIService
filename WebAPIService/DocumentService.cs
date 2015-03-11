using System;
using Model;

namespace WebAPIService
{
    class DocumentService : IDocumentService
    {
        public Document GetDocument(long id)
        {
            var document = new Document
            {
                Name = "Doc1",
                CreationDate = DateTime.Now,
                Content = "Document text content"
            };

            return document;
        }
    }
}
