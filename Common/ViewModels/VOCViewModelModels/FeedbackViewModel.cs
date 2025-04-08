namespace Common.ViewModels.VOCViewModelModels
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public int ApplyTo { get; set; }
        public int OfficeId { get; set; }
        public int MallId { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public int FeedbackTypeId { get; set; }
        public string? Solution { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> ImageUrls { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsForwarded { get; set; }
        public bool IsMobile { get; set; }
        public string? Code { get; set; }
        public string? NoteOfDepartment { get; set; }
        public bool IsOverdue { get; set; }
        public string? MallName { get; set; }
        public string? OfficeName { get; set; }
        public IEnumerable<ForwardFeedbackViewModel> ForwardFeedbacks { get; set; }
        public FeedbackViewModel()
        {
            FullName = string.Empty;
            PhoneNumber = string.Empty;
            Title = string.Empty;
            Content = string.Empty;
            ImageUrls = new List<string>();
            ForwardFeedbacks = new List<ForwardFeedbackViewModel>();
        }
    }
}
