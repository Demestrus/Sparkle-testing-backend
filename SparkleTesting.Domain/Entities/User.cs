using System;
using Microsoft.AspNetCore.Identity;

namespace SparkleTesting.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string StudyYear { get; set; }
        public byte[] Photo { get; set; }

        public User()
        {

        }
    }
}
