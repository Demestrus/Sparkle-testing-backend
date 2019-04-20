namespace SparkleTesting.Domain.Entities
{
    public class AnswerOption
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public int OptionId { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsChoosen { get; set; }

        public OptionsAnswer Answer { get; set; }

        public AnswerOption()
        {

        }
        public AnswerOption(Option option)
        {
            OptionId = option.Id;
            OptionText = option.Text;
            IsCorrect = option.IsCorrect;
        }
    }
}
