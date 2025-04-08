namespace SurveyDataAccess.DTOs
{
    public class ParticipantDTO : BaseDTO
    {
        public int Id { get; set; }
        public int SurveyFormId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Note { get; set; }
        public SurveyFormDTO? SurveyForm { get; set; }
        public ICollection<AnswerDTO> Answers { get; set; }
        public ParticipantDTO()
        {
            Answers = new List<AnswerDTO>();
        }
    }
}
