using Common.Custom.CustomDataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.SurveyViewModels
{
    public class SurveyUIViewModel
    {
        public int ID { get; set; }
        public int SurveyFormID { get; set; }
        [Required(ErrorMessage = "em_fullName")]
        [FullNameValidation]
        public string FullName { get; set; }
        [Required(ErrorMessage = "em_phoneNumber")]
        [PhoneNumberValidation(ErrorMessage = "em_phoneNumber")]
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? MallId { get; set; }
        public int? OfficeId { get; set; }
        public List<QuestionGroupUIViewModel> QuestionGroupUIs { get; set; }
        public SurveyUIViewModel()
        {
            QuestionGroupUIs = new List<QuestionGroupUIViewModel>();
        }
    }
    public class QuestionGroupUIViewModel
    {
        public int QuestionGroupID { get; set; }
        public string? QuestionGroupName { get; set; }
        public List<QuestionUIViewModel> QuestionUIs { get; set; }
        public QuestionGroupUIViewModel()
        {
            QuestionUIs = new List<QuestionUIViewModel>();
        }
    }
    public class QuestionUIViewModel
    {
        public int SelectQuestionID { get; set; }
        public int QuestionID { get; set; }
        public int QuestionTypeID { get; set; }
        public string? QuestionName { get; set; }
        public int? AnswerID { get; set; }
        public string? AnswerOfCustomer { get; set; }
        public int? Rating { get; set; }
        public int? Point { get; set; }
        public List<AnswerUIViewModel> Answers { get; set; }
        public QuestionUIViewModel()
        {
            Answers = new List<AnswerUIViewModel>();
        }
    }
    public class AnswerUIViewModel()
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public bool Checked { get; set; } = false;
        public int? Point { get; set; }
    }
}
