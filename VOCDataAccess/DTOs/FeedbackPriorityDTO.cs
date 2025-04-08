namespace VOCDataAccess.DTOs
{
    public class FeedbackPriorityDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Priority { get; set; }
        public string? Description { get; set; }
        public string? BackgroundStyle { get; set; }
    }
}
