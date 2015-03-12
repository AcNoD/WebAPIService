using System;
using System.Runtime.Serialization;

namespace Model
{

    //[DataContract(Namespace = "http://www.test.com/Docns")]
    [DataContract]
    public class Document
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        /*[DataMember]
        public DateTime CreationDate { get; set; }*/

        [DataMember]
        public string Content { get; set; }
    }
}
