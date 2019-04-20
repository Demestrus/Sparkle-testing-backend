using SparkleTesting.API.Models.Enums;
using System.Collections.Generic;

namespace SparkleTesting.API.Models.Dto
{
    /// <summary>
    /// Вопрос теста
    /// </summary>
    public class QuestionDto
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Тип вопроса:
        /// 1 - выбор одного ответа
        /// 2 - выбор нескольких ответов
        /// 3 - краткий ответ
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// Варианты ответа для типов 1 и 2
        /// </summary>
        public ICollection<OptionDto> Options { get; set; }
    }
}
