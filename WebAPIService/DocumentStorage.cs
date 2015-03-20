using System.Collections.Generic;
using System.Linq;
using Model;

namespace WebAPIService
{
    public class DocumentStorage
    {
        public static DocumentStorage Storage = new DocumentStorage();

        private readonly List<Document> _documets; 

        public DocumentStorage()
        {
            _documets = new List<Document>
            {
                new Document {Id = 1, Name = "Doc1", Content = "the doc content 1"},
                new Document {Id = 2, Name = "Doc2", Content = "the doc content 2"},
                new Document {Id = 3, Name = "Doc3", Content = "the doc content 3"},
            };
        }

        public List<Document> GetAllDocumets()
        {
            return _documets;
        }

        public Document GetDocument(long id)
        {
            return _documets.SingleOrDefault(x => x.Id == id);
        }

        public long AddDocument(Document document)
        {
            var maxId = _documets.Max(x => x.Id);
            document.Id = ++maxId;
            _documets.Add(document);
            return document.Id;
        }
    }
}
