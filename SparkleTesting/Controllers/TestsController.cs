using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkleTesting.API.Models.Dto;
using SparkleTesting.API.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparkleTesting.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class TestsController : ControllerBase
    {
        public TestsController()
        {

        }

        /// <summary>
        /// Получение списка назначенных тестов
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Запуск попытки прохождения теста и получение списка вопросов
        /// </summary>
        /// <param name="id">Идентификатор теста</param>
        /// <returns></returns>
        [HttpGet]
        [Route("start/{id}")]
        public AttemptDto GetTest(int id)
        {
            return new AttemptDto
            {
                Id = 1,
                TestName = "Тестовый мок-тест",
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto
                    {
                        Id = 1,
                        Text = "Текст вопроса. Выберите один вариант ответа.",
                        QuestionType = QuestionType.SingleOption,
                        Options = new List<OptionDto>()
                        {
                            new OptionDto
                            {
                                Id = 1,
                                Text = "Первый вариант"
                            },
                            new OptionDto
                            {
                                Id = 2,
                                Text = "Второй вариант"
                            },
                            new OptionDto
                            {
                                Id = 3,
                                Text = "Третий вариант"
                            },
                        }
                    },
                    new QuestionDto
                    {
                        Id = 2,
                        Text = "Текст вопроса. Выберите несколько вариантов ответа.",
                        QuestionType = QuestionType.ManyOptions,
                        Options = new List<OptionDto>()
                        {
                            new OptionDto
                            {
                                Id = 4,
                                Text = "Первый вариант"
                            },
                            new OptionDto
                            {
                                Id = 5,
                                Text = "Второй вариант"
                            },
                            new OptionDto
                            {
                                Id = 6,
                                Text = "Третий вариант"
                            },
                        }
                    },
                    new QuestionDto
                    {
                        Id = 3,
                        Text = "Текст вопроса. Заполните пропуск/дайте краткий ответ.",
                        QuestionType = QuestionType.ShortAnswers,
                        Options = new List<OptionDto>()
                    }
                }
            };
        }

        /// <summary>
        /// Завершение попытки прохождения теста и отправка ответов
        /// </summary>
        /// <param name="id">Идентификатор попытки</param>
        /// <param name="answers">Список ответов</param>
        /// <returns></returns>
        [HttpPost]
        [Route("finish/{id}")]
        public ActionResult SetAnswers(int id, [FromBody] IEnumerable<AnswerDto> answers)
        {
            return Ok();
        }
    }
}