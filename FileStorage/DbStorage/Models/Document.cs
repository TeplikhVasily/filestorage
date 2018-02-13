using System;

namespace DbStorage.Models
{
    public class Document : IEntity
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string FileType { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual User Author { get; set; }

        public virtual byte[] Data { get; set; }
    }
}
