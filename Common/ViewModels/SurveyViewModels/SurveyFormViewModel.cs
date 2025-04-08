using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.SurveyViewModels
{
    public class SurveyFormViewModel
    {
        public int Id { get; set; }
        public int ApplyTo { get; set; }
        public int? MallId { get; set; }
        public int? OfficeId { get; set; }
        public bool IsPeriodic { get; set; }
        public string NameVN { get; set; }
        public string NameEN { get; set; }
        public string TitleEN { get; set; }
        public string TitleVN { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionVN { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public List<SelectedQuestionViewModel> SurveyQuestions { get; set; }
        public SurveyFormViewModel()
        {
            SurveyQuestions = new List<SelectedQuestionViewModel>();
            IsActive = true;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddMonths(1);
        }
    }
    public class SelectedQuestionViewModel
    {
        public int ID { get; set; }
        public int QuestionGroupID { get; set; }
        public int QuestionID { get; set; }
        [Range(1, 100)]
        public int Priority { get; set; }
        public bool Checked { get; set; }
        public SelectedQuestionViewModel()
        {
            Priority = 1;
        }
    }
}
