using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MoreLinq;
using SparkleTesting.Application.Exceptions;
using SparkleTesting.Application.Models;
using SparkleTesting.Domain.Entities;
using SparkleTesting.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SparkleTesting.Application.Services
{
    public class UsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _context;
        private readonly SparkleDbContext _db;

        public UsersService(UserManager<User> userManager, IHttpContextAccessor context, SparkleDbContext db)
        {
            _userManager = userManager;
            _context = context;
            _db = db;
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

        private async Task<User> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("Пользователь");
            }

            return user;
        }

        private async Task<User> GetCurrentUser()
        {
            if (!_context.HttpContext.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var name = _context.HttpContext.User.Identity.Name;

            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                throw new NotFoundException("Пользователь");
            }

            return user;
        }

        public async Task<UserProfile> GetUserProfile(string id)
        {
            var user = await GetUser(id);

            return new UserProfile(user);
        }

        public async Task<UserProfile> GetCurrentUserProfile()
        {
            var user = await GetCurrentUser();

            return new UserProfile(user);
        }

        public async Task<UserProfile> ChangeUserProfile(string id, UserProfile profile)
        {
            var user = await GetUser(id);

            await UpdateUserProfile(user, profile);

            return new UserProfile(user);
        }

        public async Task<UserProfile> ChangeCurrentUserProfile(UserProfile profile)
        {
            var user = await GetCurrentUser();

            await UpdateUserProfile(user, profile);

            return new UserProfile(user);
        }

        private async Task UpdateUserProfile(User user, UserProfile profile)
        {
            user.Name = profile.Name;
            user.Surname = profile.Surname;
            user.Patronymic = profile.Patronymic;
            user.StudyYear = profile.StudyYear;
            user.Photo = profile.Photo;

            await _db.SaveChangesAsync();
        }
    }
}
