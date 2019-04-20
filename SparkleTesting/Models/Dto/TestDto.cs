using Newtonsoft.Json;

namespace SparkleTesting.API.Models.Dto
{
    /// <summary>
    /// Тест
    /// </summary>
    public class TestDto
    {
        /// <summary>
        /// Идентификатор попытки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название теста
        /// </summary>
        [JsonProperty("name")]
        public string TestName { get; set; }

        /// <summary>
        /// Находится ли тест в состоянии выполнения (пока нереализовано)
        /// </summary>
        [JsonProperty("inProgress")]
        public bool TestInProgress { get; set; }

        /// <summary>
        /// Время на попытку прохождения
        /// </summary>
        [JsonProperty("attemptTime")]
        public string TestAttemptTime { get; set; }
    }
}
