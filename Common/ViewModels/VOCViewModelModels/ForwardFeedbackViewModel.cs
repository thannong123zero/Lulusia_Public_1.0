namespace Common.ViewModels.VOCViewModelModels
{
    public class ForwardFeedbackViewModel
    {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int DepartmentId { get; set; }
        public bool IsReceived { get; set; }
        public string? Message { get; set; }
        public string? Response { get; set; }
        public DateTime CreatedOn { get; set; }
        public ForwardFeedbackViewModel()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
