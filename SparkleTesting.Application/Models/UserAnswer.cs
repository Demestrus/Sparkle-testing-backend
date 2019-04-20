using System.Collections.Generic;

namespace SparkleTesting.Application.Models
{
    public class UserAnswer
    {
        public int QuestionId { get; set; }

        public ICollection<int> SelectedOptions { get; set; }

        public Dictionary<int, string> Answers { get; set; }
    }
}
