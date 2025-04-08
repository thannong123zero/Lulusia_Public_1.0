namespace VOCDataAccess.DTOs
{
    public class FeedbackDTO : BaseDTO
    {
        public int Id { get; set; }
        public int ApplyTo { get; set; }
        public int? MallId { get; set; }
        public int? OfficeId { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public int FeedbackTypeId { get; set; }
        public string? Solution { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string? Images { get; set; }
        public bool IsForwarded { get; set; }
        public bool IsMobile { get; set; }
        public bool IsOverdue { get; set; }
        public required string Code { get; set; }
    }
}
