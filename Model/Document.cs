using System;

namespace Model
{
    public class Document
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public string Content { get; set; }
    }
}
