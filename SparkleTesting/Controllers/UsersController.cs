using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkleTesting.Application.Services;
using System.Threading.Tasks;

namespace SparkleTesting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;

        public UsersController(UsersService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("register")]
        public async Task Register(string token)
        {
            var decoded = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            await _service.RegisterIfNotExists(decoded.Uid);
        }

        [HttpPost]
        [Route("register2")]
        [Authorize]
        public async Task Register2()
        {
            var user = User;
        }
    }
}