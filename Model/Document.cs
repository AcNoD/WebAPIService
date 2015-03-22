using System.Runtime.Serialization;

namespace Model
{
    /// <summary>
    /// Document entity
    /// </summary>
    [DataContract(Namespace = "http://tempuri.org/")]
    public class Document
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [DataMember(Order = 0)]
        public long Id { get; set; }

        /// <summary>
        /// Document name
        /// </summary>
        [DataMember(Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Document content
        /// </summary>
        [DataMember(Order = 2)]
        public string Content { get; set; }

        /// <summary>
        /// Paramterless ctor
        /// </summary>
        public Document()
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">Document name</param>
        /// <param name="content">Document content</param>
        public Document(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}
