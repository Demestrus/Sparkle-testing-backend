using System.ComponentModel.DataAnnotations;

namespace SparkleTesting.API.Models.Dto
{
    /// <summary>
    /// Данные о профиле пользователя
    /// </summary>
    public class UserProfileDto
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Год обучения
        /// </summary>
        public string StudyYear { get; set; }
    }
}
