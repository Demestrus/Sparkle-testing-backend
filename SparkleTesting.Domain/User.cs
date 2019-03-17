using System;
using Microsoft.AspNetCore.Identity;

namespace SparkleTesting.Domain
{
    public class User : IdentityUser
    {
        public string Pin { get; set; }
    }
}
