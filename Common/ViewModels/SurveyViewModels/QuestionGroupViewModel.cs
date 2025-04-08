namespace Common.ViewModels.SurveyViewModels
{
    public class QuestionGroupViewModel
    {
        public int Id { get; set; }
        public int ApplyTo { get; set; }
        public string NameEN { get; set; }
        public string NameVN { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public ICollection<QuestionViewModel> Questions { get; set; }
        public QuestionGroupViewModel()
        {
            Priority = 1;
            IsActive = true;
            Questions = new List<QuestionViewModel>();
        }
    }
}
