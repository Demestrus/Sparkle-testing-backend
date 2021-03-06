﻿using MoreLinq;
using System.Collections.Generic;

namespace SparkleTesting.Domain.Entities
{
    public class PassFilling
    {
        public int Id { get; set; }

        private string _answerStrings = string.Empty;
        public ICollection<string> CorrectAnswers { get => _answerStrings.Split("/;"); set => _answerStrings = value.ToDelimitedString("/;"); }

        public int SortOrder { get; set; }
    }
}
