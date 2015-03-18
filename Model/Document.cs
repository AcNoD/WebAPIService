using System.Runtime.Serialization;

namespace Model
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class Document
    {
        [DataMember(Order = 0)]
        public long Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string Content { get; set; }

        public Document()
        {
        }

        public Document(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}
