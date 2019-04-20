using SparkleTesting.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace SparkleTesting.Domain.Entities
{
    public class Test : IAuditableEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan AttemptTime { get; set; }
        public bool InProgress { get; set; }
        public DateTime CreateDate { get ; set ; }
        public bool IsDeleted { get; set; }

        public ICollection<TestMark> Marks { get; set; } = new HashSet<TestMark>();
        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        public ICollection<Attempt> Attempts { get; set; } = new HashSet<Attempt>();
    }
}
