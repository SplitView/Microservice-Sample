using System;

namespace Book.Domain.Entities
{
    public class AuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
