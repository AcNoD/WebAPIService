using System;
using System.Runtime.Serialization;

namespace Model
{

    [DataContract(Namespace = "http://tempuri.org/")]
    public class Document
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string MyContent { get; set; }
    }
}
