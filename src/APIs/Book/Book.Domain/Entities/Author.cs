using System.Collections.Generic;

namespace Book.Domain.Entities
{
    public class Author : SoftDeleteAbleAuditAbleEntity
    {
        public Author()
        {
            AuthoredBooks = new HashSet<Book>();
        }

        public Author(string name)
        {
            Name= name;
        }

        public string Name { get; set; }
        public ICollection<Book> AuthoredBooks { get; set; }
    }
}
