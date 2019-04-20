using System;
using System.Collections.Generic;
using System.Text;

namespace SparkleTesting.Domain.Interfaces
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
