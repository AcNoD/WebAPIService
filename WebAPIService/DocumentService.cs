using System;
using System.ServiceModel;
using Model;

namespace WebAPIService
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)] 
    class DocumentService : IDocumentService
    {
        public Document GetDocument()
        {
            var document = new Document
            {
                Name = "Doc_" + 100,
                CreationDate = DateTime.Now,
                Content = "Document text content"
            };

            return document;
        }
    }
}
