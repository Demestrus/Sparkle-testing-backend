using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkleTesting.Application.Services;
using System.Threading.Tasks;

namespace SparkleTesting.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;

        public UsersController(UsersService service)
        {
            _service = service;
        }

        /// <summary>
        /// Регистрация пользователя в системе по Firebase-токену 
        /// </summary>
        /// <param name="token">Firebase-токен</param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task Register(string token)
        {
            var decoded = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            await _service.RegisterIfNotExists(decoded.Uid);
        }

        /// <summary>
        /// Тестирование авторизованного запроса с подложенным в хедер Authorization токеном
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("authTest")]
        [Authorize]
        public TestObj AuthTest()
        {
            return new TestObj
            {
                Test = "Hello authorized world!"
            };
        }

        public class TestObj
        {
            public string Test { get; set; }
        }
    }
}