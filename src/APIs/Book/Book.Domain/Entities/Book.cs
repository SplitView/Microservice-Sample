using System;
using System.Collections.Generic;

namespace Book.Domain.Entities
{
    public class Book : SoftDeleteAbleAuditAbleEntity
    {
        public Book()
        {
            Authors = new HashSet<Author>();
        }

        public string Name { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public Guid PublishorId { get; set; }

        public ICollection<Author> Authors { get; set; }
        public Publishor Publishor { get; set; }
    }
}
