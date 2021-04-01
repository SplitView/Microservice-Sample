using System;

namespace Book.Domain.Entities
{
    public class SoftDeleteAbleAuditAbleEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedBy { get; set; }
    }
}
