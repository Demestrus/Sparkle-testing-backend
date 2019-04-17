using System;

namespace SparkleTesting.Domain.Interfaces
{
    interface IAuditableEntity
    {
        DateTime CreateDate { get; set; }
    }
}
