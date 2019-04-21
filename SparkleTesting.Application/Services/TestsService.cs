using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SparkleTesting.Domain.Entities;
using SparkleTesting.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleTesting.Application.Services
{
    public class TestsService
    {
        private readonly SparkleDbContext _db;

        public TestsService(SparkleDbContext db)
        {
            _db = db;
        }

        private IQueryable<Test> QueryTests()
        {
            return _db.Test;
        }

        public IEnumerable<Test> GetTests()
        {
            return QueryTests()
                .Include(s=>s.Attempts)
                .ThenInclude(s=>s.User);
        }

        public async Task AssignToTest(int id, string assigne)
        {
            var test = await QueryTests().SingleOrDefaultAsync(s => s.Id == id);

            if (test == null)
            {
                throw new DllNotFoundException("Тест");
            }

            test.Attempts.Add(new Attempt
            {
                Assigned = assigne
            });

            await _db.SaveChangesAsync();
        }
    }
}
