using System;
using System.Collections.Generic;

namespace SparkleTesting.Domain.Entities
{
    public class Attempt
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Assigned { get; set; }
        public int TestId { get; set; }
        public TimeSpan? MaxTime { get; set; }
        public TimeSpan? ElapsedTime { get; set; }
        public DateTime? StartTime { get; set; }
        public int? PointsAcquired { get; set; }
        public int? MarkId { get; set; }

        public Test Test { get; set; }
        public User User { get; set; }
        public TestMark Mark { get; set; }
        public ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
    }
}
