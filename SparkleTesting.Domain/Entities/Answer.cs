using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SparkleTesting.Domain.Entities
{
    public abstract class Answer
    {
        public int Id { get; set; }
        public int AttemptId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionPoints { get; set; }

        public Attempt Attempt { get; set; }

        public abstract bool IsCorrect();
    }

    public class OptionsAnswer : Answer
    {
        public ICollection<AnswerOption> Options { get; set; } = new HashSet<AnswerOption>();

        public override bool IsCorrect()
        {
            return Options.All(s => s.IsCorrect == s.IsChoosen);
        }
    }

    public class PassFillingAnswer : Answer
    {
        public ICollection<FilledPass> FilledPasses { get; set; } = new HashSet<FilledPass>();

        public override bool IsCorrect()
        {
            return FilledPasses.All(s => s.CorrectAnswers.Select(a => a.Trim().ToLower()).Contains(s.UserAnswer.Trim().ToLower()));
        }
    }
}
