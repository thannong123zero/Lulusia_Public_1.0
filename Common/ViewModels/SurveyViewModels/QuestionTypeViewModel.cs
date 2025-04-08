namespace Common.ViewModels.SurveyViewModels
{
    public class QuestionTypeViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
