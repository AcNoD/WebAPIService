using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using Model;

namespace WebAPIService
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)] 
    class DocumentService : IDocumentService
    {
        private static long _cnt = 4;
        private static List<Document> _documets = new List<Document>
            {
                new Document {Id = 1, Name = "Doc1", Content = "the doc content 1"},
                new Document {Id = 2, Name = "Doc2", Content = "the doc content 2"},
                new Document {Id = 3, Name = "Doc3", Content = "the doc content 3"},
            };


        public Document GetDocument(string id)
        {
            return _documets.SingleOrDefault(x => x.Id == long.Parse(id));
        }

        public long AddDocument(Document document)
        {
            document.Id = _cnt++;
            _documets.Add(document);
            return document.Id;
        }

        public string GetData(string value)
        {
            return "You have sent " + value;
        }

        public string PostData(string value)
        {
            return "You have sent " + value;
        }
    }
}
