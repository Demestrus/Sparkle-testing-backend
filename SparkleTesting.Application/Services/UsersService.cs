using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MoreLinq;
using SparkleTesting.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SparkleTesting.Application.Services
{
    public class UsersService
    {
        private readonly UserManager<User> _userManager;

        public UsersService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task RegisterIfNotExists(string uid)
        {
            var user = await _userManager.FindByIdAsync(uid);

            if (user != null)
            {
                return;
            }

            user = new User()
            {
                Id = uid,
                UserName = uid //TODO подумать над тем, чтобы брать более осмысленные данные
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(s => s.Description).ToDelimitedString(". ");
                throw new Exception(errors);
            }
        }
    }
}
