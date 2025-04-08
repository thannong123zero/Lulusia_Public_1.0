namespace SurveyDataAccess.DTOs
{
    public class QuestionGroupDTO : BaseDTO
    {
        public int Id { get; set; }
        public required string NameEN { get; set; }
        public required string NameVN { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }
        public ICollection<QuestionDTO> Questions { get; set; }
        public QuestionGroupDTO()
        {
            Questions = new List<QuestionDTO>();
        }

    }
}
