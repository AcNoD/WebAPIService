using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace WebAPIService
{
    public class DocumentStorage
    {
        public static DocumentStorage Storage = new DocumentStorage();
        private static object _sync = new object();

        private readonly List<Document> _documents; 

        public DocumentStorage()
        {
            _documents = new List<Document>
            {
                new Document {Id = 1, Name = "Doc1", Content = "the doc content 1"},
                new Document {Id = 2, Name = "Doc2", Content = "the doc content 2"},
                new Document {Id = 3, Name = "Doc3", Content = "the doc content 3"},
            };
        }

        public List<Document> GetAllDocumets()
        {
            return _documents;
        }

        public Document GetDocument(long id)
        {
            return _documents.SingleOrDefault(x => x.Id == id);
        }

        public long AddDocument(Document document)
        {
            lock (_sync)
            {
                var maxId = _documents.Max(x => x.Id);
                document.Id = ++maxId;
                _documents.Add(document);
                return document.Id;
            }
        }

        public long GetMaxId()
        {
            lock (_sync)
            {
                return _documents.Max(x => x.Id);
            }
        }
    }
}
