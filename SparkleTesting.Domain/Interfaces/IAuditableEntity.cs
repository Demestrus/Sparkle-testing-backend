using System;

namespace SparkleTesting.Domain.Interfaces
{
    public interface IAuditableEntity
    {
        DateTime CreateDate { get; set; }
    }
}
