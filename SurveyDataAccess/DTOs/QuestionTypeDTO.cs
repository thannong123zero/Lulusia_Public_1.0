namespace SurveyDataAccess.DTOs
{
    public class QuestionTypeDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public ICollection<QuestionDTO> Questions { get; set; }
        public QuestionTypeDTO()
        {
            IsActive = true;
            CreatedOn = DateTime.Now;
            Questions = new List<QuestionDTO>();
        }
    }
}
