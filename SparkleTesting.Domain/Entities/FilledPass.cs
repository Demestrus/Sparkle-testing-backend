using MoreLinq;
using System.Collections.Generic;

namespace SparkleTesting.Domain.Entities
{
    public class FilledPass
    {
        public int Id { get; set; }

        private string _answerStrings = string.Empty;
        public ICollection<string> CorrectAnswers { get => _answerStrings.Split("/;"); set => _answerStrings = value.ToDelimitedString("/;"); }
        public string UserAnswer { get; set; } = string.Empty;

        public int SortOrder { get; set; }

        public FilledPass()
        {
            UserAnswer = "";
        }
        public FilledPass(PassFilling pass) : this()
        {
            CorrectAnswers = pass.CorrectAnswers;
            SortOrder = pass.SortOrder;
        }
    }
}
