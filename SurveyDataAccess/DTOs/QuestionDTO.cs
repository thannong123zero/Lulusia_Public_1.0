namespace SurveyDataAccess.DTOs
{
    public class QuestionDTO : BaseDTO
    {
        public int Id { get; set; }
        public int QuestionGroupId { get; set; }
        public int QuestionTypeId { get; set; }
        public required string ChartLabel { get; set; }
        public required string NameEN { get; set; }
        public required string NameVN { get; set; }
        public string? Description { get; set; }
        public QuestionTypeDTO? QuestionType { get; set; }
        public QuestionGroupDTO? QuestionGroup { get; set; }
        public ICollection<PredefinedAnswerDTO> PredefinedAnswers { get; set; }
        public ICollection<SurveyQuestionDTO> SurveyQuestions { get; set; }
        public QuestionDTO()
        {
            PredefinedAnswers = new List<PredefinedAnswerDTO>();
            SurveyQuestions = new List<SurveyQuestionDTO>();
        }
    }
}
