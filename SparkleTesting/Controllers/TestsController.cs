using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkleTesting.API.Models;
using System;
using System.Collections.Generic;

namespace SparkleTesting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestsController : ControllerBase
    {
        public TestsController()
        {

        }

        [HttpGet]
        public IEnumerable<TestDto> GetTests()
        {
            return new List<TestDto>()
            {
                new TestDto()
                {
                    Id = 1,
                    InProgress = true,
                    Name = "Первый Mock-тест!",
                    AttemptTime = TimeSpan.FromMinutes(15).ToString()
                },
                new TestDto()
                {
                    Id = 2,
                    InProgress = false,
                    Name = "Второй Mock-тест!",
                    AttemptTime = TimeSpan.FromMinutes(15).ToString()
                },
                new TestDto()
                {
                    Id = 3,
                    InProgress = true,
                    Name = "Третий все я устал мокать...",
                    AttemptTime = TimeSpan.FromMinutes(15).ToString()
                }
            };
        }
    }
}