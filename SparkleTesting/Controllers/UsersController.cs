using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkleTesting.API.Models.Dto;
using SparkleTesting.Application.Models;
using SparkleTesting.Application.Services;
using System.Threading.Tasks;

namespace SparkleTesting.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;
        private readonly IMapper _mapper;

        public UsersController(UsersService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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

            decoded.Claims.TryGetValue("phone_number", out var phoneNumber);

            decoded.Claims.TryGetValue("email", out var email);

            await _service.RegisterIfNotExists(decoded.Uid, phoneNumber?.ToString(), email?.ToString());
        }

        /// <summary>
        /// Получение профиля текущего пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("profile")]
        [Authorize]
        public async Task<UserProfileDto> GetProfile()
        {
            var profile = await _service.GetCurrentUserProfile();

            return _mapper.Map<UserProfileDto>(profile);
        }

        /// <summary>
        /// Изменение профиля текущего пользователя
        /// </summary>
        /// <param name="profileDto">Измененный профиль</param>
        /// <returns></returns>
        [HttpPut]
        [Route("profile")]
        [Authorize]
        public async Task<UserProfileDto> ChangeProfile([FromBody] UserProfileDto profileDto)
        {
            var profile = _mapper.Map<UserProfile>(profileDto);

            profile = await _service.ChangeCurrentUserProfile(profile);

            return _mapper.Map<UserProfileDto>(profile);
        }
    }
}