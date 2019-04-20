using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkleTesting.API.Models.Dto;
using SparkleTesting.Application.Models;
using SparkleTesting.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparkleTesting.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class TestsController : ControllerBase
    {
        private readonly AttemptsService _service;
        private readonly IMapper _mapper;

        public TestsController(AttemptsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение списка назначенных тестов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<TestDto>> GetTests()
        {
            var attempts = await _service.GetAssignedTests();
            return _mapper.Map<IEnumerable<TestDto>>(attempts);
        }

        /// <summary>
        /// Запуск попытки прохождения теста и получение списка вопросов
        /// </summary>
        /// <param name="id">Идентификатор теста</param>
        /// <returns></returns>
        [HttpGet]
        [Route("start/{id}")]
        public async Task<AttemptDto> GetTest(int id)
        {
            var attempt = await _service.StartAssignedAttempt(id);

            return _mapper.Map<AttemptDto>(attempt);
        }

        /// <summary>
        /// Завершение попытки прохождения теста и отправка ответов
        /// </summary>
        /// <param name="id">Идентификатор попытки</param>
        /// <param name="answersDto">Список ответов</param>
        /// <returns></returns>
        [HttpPost]
        [Route("finish/{id}")]
        public async Task<ActionResult> SetAnswers(int id, [FromBody] IEnumerable<AnswerDto> answersDto)
        {
            var answers = _mapper.Map<ICollection<UserAnswer>>(answersDto);

            await _service.SetAttemptResults(id, answers);
            return Ok();
        }
    }
}