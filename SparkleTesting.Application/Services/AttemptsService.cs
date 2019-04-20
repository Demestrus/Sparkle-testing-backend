using Microsoft.EntityFrameworkCore;
using SparkleTesting.Application.Exceptions;
using SparkleTesting.Application.Models;
using SparkleTesting.Domain.Entities;
using SparkleTesting.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkleTesting.Application.Services
{
    public class AttemptsService
    {
        private readonly SparkleDbContext _db;
        private readonly UsersService _usersService;

        public AttemptsService(SparkleDbContext db, UsersService usersService)
        {
            _db = db;
            _usersService = usersService;
        }

        private IQueryable<Attempt> QuerryAttempts()
        {
            return _db.Attempts;
        }
        public async Task<IEnumerable<Attempt>> GetAssignedTests()
        {
            var user = await _usersService.GetCurrentUser();

            var attempts = QuerryAttempts()
                .Include(s=>s.Test)
                .Where(s => s.UserId == user.Id || s.Assigned == user.PhoneNumber || s.Assigned == user.Email);

            return attempts;
        }

        public async Task<Attempt> GetAssignedAttempt(int id)
        {
            var user = await _usersService.GetCurrentUser();

            var attempt = await QuerryAttempts()
                .Include(s => s.Test)
                .Include(s => s.Answers)
                .ThenInclude(s => (s as OptionsAnswer).Options)
                .Include(s => s.Answers)
                .ThenInclude(s => (s as PassFillingAnswer).FilledPasses)
                .Where(s => s.UserId == user.Id || s.Assigned == user.PhoneNumber || s.Assigned == user.Email)
                .SingleOrDefaultAsync(s => s.Id == id);

            if (attempt == null)
            {
                throw new NotFoundException("Тест (попытка)");
            }

            return attempt;
        }

        public async Task<UserAttempt> StartAssignedAttempt(int id)
        {
            var attempt = await GetAssignedAttempt(id);

            if (string.IsNullOrEmpty(attempt.UserId))
            {
                attempt. UserId = (await _usersService.GetCurrentUser()).Id;
            }

            var userAttempt = await CreateUserAttempt(attempt);

            foreach (var question in userAttempt.Questions)
            {
                switch (question)
                {
                    case OptionsQuestion optQuestion:
                        attempt.Answers.Add(new OptionsAnswer(optQuestion));
                        break;

                    case PassFillingQuestion passFillQuestion:
                        attempt.Answers.Add(new PassFillingAnswer(passFillQuestion));
                        break;
                }
            }

            attempt.MaxTime = attempt.Test.AttemptTime;
            attempt.StartTime = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return userAttempt;
        }

        public async Task SetAttemptResults(int id, ICollection<UserAnswer> answers)
        {
            var attempt = await GetAssignedAttempt(id);

            if (!attempt.StartTime.HasValue)
            {
                throw new BadRequestException("Невозможно сохранить результаты теста. Попытка прохождения не была начата.");
            }

            if (attempt.ElapsedTime.HasValue)
            {
                throw new BadRequestException("Попытка прохождения уже учтена.");
            }

            attempt.ElapsedTime = DateTime.UtcNow.Subtract(attempt.StartTime.Value);

            answers.AsParallel().ForAll(answer =>
            {
                var entity = attempt.Answers.SingleOrDefault(s => s.QuestionId == answer.QuestionId);

                switch (entity)
                {
                    case OptionsAnswer optAnswer:
                        optAnswer.Options.AsParallel().ForAll(opt=>
                        {
                            opt.IsChoosen = answer.SelectedOptions.Contains(opt.OptionId);
                        });
                        break;

                    case PassFillingAnswer passFillAsnwer:
                        passFillAsnwer.FilledPasses.AsParallel().ForAll(pass =>
                        {
                            if (answer.Answers.TryGetValue(pass.Id, out var text))
                            {
                                pass.UserAnswer = text;
                            }
                        });
                        break;
                }
            });

            //подсчет количества баллов
            var pointsAcquired = attempt.Answers.Where(s => s.IsCorrect()).Sum(s => s.QuestionPoints);
            attempt.PointsAcquired = pointsAcquired;

            if (!_db.Entry(attempt.Test).Collection(s => s.Marks).IsLoaded)
            {
                await _db.Entry(attempt.Test).Collection(s => s.Marks).LoadAsync();
            }

            //установка оценки
            var mark = attempt.Test.Marks
                .OrderByDescending(s => s.PointsThreshold)
                .FirstOrDefault(s => s.PointsThreshold <= pointsAcquired);

            if (mark == null)
            {
                throw new Exception("Критическая ошибка! Не удалось установить оценку за тест.");
            }

            attempt.MarkId = mark.Id;

            await _db.SaveChangesAsync();
        }

        private async Task<UserAttempt> CreateUserAttempt(Attempt attempt)
        {
            await _db.Entry(attempt).Reference(s => s.Test)
                .Query()
                .Include(s => s.Questions)
                .ThenInclude(s => (s as OptionsQuestion).Options)
                .Include(s => s.Questions)
                .ThenInclude(s => (s as PassFillingQuestion).Passes)
                .LoadAsync();

            var userAttempt = new UserAttempt()
            {
                Id = attempt.Id,
                TestName = attempt.Test.Name,
                MaxTime = attempt.Test.AttemptTime,
                Questions = attempt.Test.Questions.OrderBy(s=>s.SortOrder).ToList()
            };

            return userAttempt;
        }
    }
}
