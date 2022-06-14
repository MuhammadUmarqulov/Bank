using System;


namespace BankNTProject.Domain.Entities
{
    public class Auditable
    {
        public long Id { get; set; }
        public Guid PrimeryKey { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
