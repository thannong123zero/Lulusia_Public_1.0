namespace SurveyDataAccess.DTOs
{
    public class SurveyFormDTO : BaseDTO
    {
        public int Id { get; set; }
        public int? MallId { get; set; }
        public int? OfficeId { get; set; }
        public bool IsPeriodic { get; set; }
        public required string NameEN { get; set; }
        public required string NameVN { get; set; }
        public required string TitleEN { get; set; }
        public required string TitleVN { get; set; }
        public required string DescriptionEN { get; set; }
        public required string DescriptionVN { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<ParticipantDTO> Participants { get; set; }
        public ICollection<SurveyQuestionDTO> SurveyQuestions { get; set; }
        public SurveyFormDTO()
        {
            Participants = new List<ParticipantDTO>();
            SurveyQuestions = new List<SurveyQuestionDTO>();
        }
    }
}
