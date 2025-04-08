namespace SurveyDataAccess.DTOs
{
    public class SurveyQuestionDTO
    {
        public int Id { get; set; }
        public int SurveyFormId { get; set; }
        public int QuestionGroupId { get; set; }
        public int QuestionId { get; set; }
        public int Priority { get; set; }
        public SurveyFormDTO? SurveyFrom { get; set; }
        public QuestionDTO? Question { get; set; }
    }
}
