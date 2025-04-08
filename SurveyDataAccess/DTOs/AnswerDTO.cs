namespace SurveyDataAccess.DTOs
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int QuestionGroupId { get; set; }
        public int QuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public int? AnswerId { get; set; }
        public string? Answer { get; set; }
        public int? Rating { get; set; }
        public int? Point { get; set; }
        public ParticipantDTO? Participant { get; set; }
    }
}
