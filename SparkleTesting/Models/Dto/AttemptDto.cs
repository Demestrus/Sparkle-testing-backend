using System.Collections.Generic;

namespace SparkleTesting.API.Models.Dto
{
    /// <summary>
    /// Данные о текущей попытке прохождения теста
    /// </summary>
    public class AttemptDto
    {
        /// <summary>
        /// Идентификатор попытки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название теста
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Время на попытку прохождения
        /// </summary>
        public string MaxTime { get; set; }

        /// <summary>
        /// Вопросы текущей попытки
        /// </summary>
        public ICollection<QuestionDto> Questions { get; set; }
    }
}
