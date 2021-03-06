﻿using SparkleTesting.Domain.Interfaces;

namespace SparkleTesting.Domain.Entities
{
    public class Option : ISoftDelete
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsDeleted { get; set ; }

        public OptionsQuestion Question { get; set; }
    }
}
