using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SparkleTesting.Domain.Entities;
using SparkleTesting.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return QueryTests();
        }
    }
}
