using SparkleTesting.Domain.Interfaces;

namespace SparkleTesting.Domain.Entities
{
    public class TestMark : ISoftDelete
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int PointsThreshold { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public Test Test { get; set; }
    }
}
