using SparkleTesting.Domain.Enum;
using SparkleTesting.Domain.Interfaces;
using System;
using System.Collections.Generic;
using MoreLinq;

namespace SparkleTesting.Domain.Entities
{
    public abstract class Question : IAuditableEntity, ISoftDelete
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Text { get; set; }
        public int SortOrder { get; set; }
        public int Points { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }

        public Test Test { get; set; }
    }

    public class OptionsQuestion : Question
    {
        public OptionQuestionType Type { get; set; }

        public ICollection<Option> Options { get; set; } = new HashSet<Option>();
    }

    public class PassFillingQuestion : Question
    {
        public ICollection<PassFilling> Passes { get; set; } = new HashSet<PassFilling>();
    }

}
