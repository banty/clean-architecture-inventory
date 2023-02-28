using System;
namespace CleanArchitectureInventory.Receiving.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }


    }
}

