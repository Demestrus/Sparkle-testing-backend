using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace SparkleTesting.Persistence
{
    public class Initializer
    {
        private readonly SparkleDbContext _db;
        public Initializer(SparkleDbContext db)
        {
            _db = db;
        }

        public Task Initialize()
        {
            _db.Database.Migrate();

            return Task.CompletedTask;
        }
    }
}
