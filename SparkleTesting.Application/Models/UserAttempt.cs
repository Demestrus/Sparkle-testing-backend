using SparkleTesting.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SparkleTesting.Application.Models
{
    public class UserAttempt
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public TimeSpan MaxTime { get; set; }

        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
    }
}
