namespace SurveyDataAccess.DTOs
{
    public class PredefinedAnswerDTO
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int Point { get; set; }
        public required string NameVN { get; set; }
        public required string NameEN { get; set; }
        public string? Description { get; set; }
        public QuestionDTO? Question { get; set; }
    }
}
