using Microsoft.EntityFrameworkCore;
using SparkleTesting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkleTesting.Persistence
{
    public class Initializer
    {
        private readonly SparkleDbContext _db;
        public Initializer(SparkleDbContext db)
        {
            _db = db;
        }

        public async Task Initialize()
        {
            _db.Database.Migrate();

            if (_db.Test.Any())
            {
                return;
            }

            var test = new Test()
            {
                Name = "Проверочный тест",
                AttemptTime = TimeSpan.FromMinutes(5),
                InProgress = false,
            };

            test.Marks.Add(new TestMark
            {
                Name = "Все очень плохо",
                PointsThreshold = 0,
            });

            test.Marks.Add(new TestMark
            {
                Name = "Уже получше",
                PointsThreshold = 1,
            });

            test.Marks.Add(new TestMark
            {
                Name = "Cool",
                PointsThreshold = 2,
            });

            var optQuestion = new OptionsQuestion
            {
                Text = "Простой выбор одного ответа",
                SortOrder = 3,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseOne,
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Неверный",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Верный",
                IsCorrect = true,
            });

            test.Questions.Add(optQuestion);

            optQuestion = new OptionsQuestion
            {
                Text = "Выбор нескольких ответов",
                SortOrder = 1,
                Points = 1,
                Type = Domain.Enum.OptionQuestionType.ChooseMany
            };

            optQuestion.Options.Add(new Option
            {
                Text = "Неверный",
                IsCorrect = false,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Верный",
                IsCorrect = true,
            });

            optQuestion.Options.Add(new Option
            {
                Text = "Верный",
                IsCorrect = true,
            });

            test.Questions.Add(optQuestion);

            var fillQuestion = new PassFillingQuestion
            {
                Text = "Заполните несколько пропусков",
                SortOrder = 2,
                Points = 1,
            };

            fillQuestion.Passes.Add(new PassFilling
            {
                CorrectAnswers = new List<string>()
                {
                    "Верно",
                    "Верняк"
                },
                SortOrder = 1
            });

            fillQuestion.Passes.Add(new PassFilling
            {
                CorrectAnswers = new List<string>()
                {
                    "Ее",
                    "уу"
                },
                SortOrder = 2
            });

            test.Questions.Add(fillQuestion);

            test.Attempts.Add(new Attempt
            {
                Assigned = "+15555550555"
            });

            _db.Test.Add(test);

            await _db.SaveChangesAsync();
        }
    }
}
