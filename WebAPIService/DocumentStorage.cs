using System.Collections.Generic;
using System.Linq;
using Model;

namespace WebAPIService
{
    /// <summary>
    /// Document storage
    /// </summary>
    public class DocumentStorage
    {
        /// <summary>
        /// Storage instance
        /// </summary>
        public static DocumentStorage Storage = new DocumentStorage();

        /// <summary>
        /// Syncronization oject
        /// </summary>
        private static readonly object Sync = new object();
        
        /// <summary>
        /// Internal documents collection
        /// </summary>
        private readonly List<Document> _documents; 

        /// <summary>
        /// Ctor
        /// </summary>
        public DocumentStorage()
        {
            _documents = new List<Document>
            {
                new Document {Id = 1, Name = "Doc1", Content = "the doc content 1"},
                new Document {Id = 2, Name = "Doc2", Content = "the doc content 2"},
                new Document {Id = 3, Name = "Doc3", Content = "the doc content 3"},
            };
        }

        /// <summary>
        /// Returns all documents form storage
        /// </summary>
        /// <returns>Documents collection</returns>
        public List<Document> GetAllDocumets()
        {
            return _documents;
        }

        /// <summary>
        /// Gets documetns by document identifier
        /// </summary>
        /// <param name="id">Document identifier</param>
        /// <returns>Document</returns>
        public Document GetDocument(long id)
        {
            return _documents.SingleOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Adds new document to storage
        /// </summary>
        /// <param name="document">New document</param>
        /// <returns>Identifier of saved document</returns>
        public long AddDocument(Document document)
        {
            lock (Sync)
            {
                var maxId = _documents.Max(x => x.Id);
                document.Id = ++maxId;
                _documents.Add(document);
                return document.Id;
            }
        }

        /// <summary>
        /// Gets max identifier from documents collection
        /// </summary>
        /// <returns>Max identifier</returns>
        public long GetMaxId()
        {
            lock (Sync)
            {
                return _documents.Max(x => x.Id);
            }
        }
    }
}
