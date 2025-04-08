using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.SurveyViewModels
{
    public class PredefinedAnswerViewModel
    {
        public int Id { get; set; }
        [Range(1, 100)]
        public int Point { get; set; }
        public string NameEN { get; set; }
        public string NameVN { get; set; }
        public string? Description { get; set; }
        public PredefinedAnswerViewModel()
        {
            Point = 0;
            NameEN = string.Empty;
            NameVN = string.Empty;
        }
    }
}
