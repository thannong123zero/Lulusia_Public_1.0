namespace VOCDataAccess.DTOs
{
    public class FeedbackTypeDTO : BaseDTO
    {
        public int Id { get; set; }
        public int ApplyTo { get; set; }
        public required string NameEN { get; set; }
        public required string NameVN { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }
    }
}
