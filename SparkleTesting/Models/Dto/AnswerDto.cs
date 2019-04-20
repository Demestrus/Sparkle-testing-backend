using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SparkleTesting.API.Models.Dto
{
    /// <summary>
    /// Ответ
    /// </summary>
    public class AnswerDto
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        [Required]
        public int QuestionId { get; set; }

        /// <summary>
        /// Список идентификаторов выбранных вариантов
        /// </summary>
        public ICollection<int> SelectedOptions { get; set; }

        /// <summary>
        /// Список заполненных пропусков
        /// </summary>
        public ICollection<PassFillDto> Answers { get; set; }
    }
}
