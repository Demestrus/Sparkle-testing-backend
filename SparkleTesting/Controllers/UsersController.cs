using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkleTesting.API.Models.Dto;
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
        /// Получение профиля текущего пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("profile")]
        [Authorize]
        public UserProfileDto GetProfile()
        {
            return new UserProfileDto
            {
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = "Иванович",
                StudyYear = "365 год"
            };
        }

        /// <summary>
        /// Изменение профиля текущего пользователя
        /// </summary>
        /// <param name="profile">Измененный профиль</param>
        /// <returns></returns>
        [HttpPut]
        [Route("profile")]
        [Authorize]
        public ActionResult<UserProfileDto> ChangeProfile([FromBody] UserProfileDto profile)
        {
            return profile;
        }
    }
}