namespace Common.ViewModels.SurveyViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public int QuestionTypeId { get; set; }
        public int QuestionGroupId { get; set; }
        public string ChartLabel { get; set; }
        public string NameEN { get; set; }
        public string NameVN { get; set; }
        public bool IsEdited { get; set; }
        public ICollection<PredefinedAnswerViewModel> PredefinedAnswers { get; set; }
        public QuestionViewModel()
        {
            NameVN = string.Empty;
            NameEN = string.Empty;
            PredefinedAnswers = new List<PredefinedAnswerViewModel>();
        }
    }
}
