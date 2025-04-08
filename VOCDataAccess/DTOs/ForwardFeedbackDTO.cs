namespace VOCDataAccess.DTOs
{
    public class ForwardFeedbackDTO
    {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int DepartmentId { get; set; }
        public bool IsReceived { get; set; }
        public string? Message { get; set; }
        public string? Response { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ReplyOn { get; set; }
        public ForwardFeedbackDTO()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
