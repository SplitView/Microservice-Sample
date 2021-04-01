using System.Collections.Generic;

namespace Book.Domain.Entities
{
    public class Publishor : SoftDeleteAbleAuditAbleEntity
    {
        public Publishor()
        {
            PublishedBooks= new HashSet<Book>();
        }

        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Book> PublishedBooks { get; set; }
    }
}
