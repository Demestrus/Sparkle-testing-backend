using System;
using System.Collections.Generic;
using System.Text;

namespace SparkleTesting.Domain.Interfaces
{
    interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
