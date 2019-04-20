namespace SparkleTesting.API.Models.Dto
{
    /// <summary>
    /// Тест
    /// </summary>
    public class TestDto
    {
        /// <summary>
        /// Идентификатор теста
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название теста
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Находится ли тест в состоянии выполнения (пока нереализовано)
        /// </summary>
        public bool InProgress { get; set; }

        /// <summary>
        /// Время на попытку прохождения
        /// </summary>
        public string AttemptTime { get; set; }
    }
}
