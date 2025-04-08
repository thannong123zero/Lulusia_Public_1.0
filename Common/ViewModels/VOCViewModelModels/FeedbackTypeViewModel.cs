namespace Common.ViewModels.VOCViewModelModels
{
    public class FeedbackTypeViewModel
    {
        public int Id { get; set; }
        public int ApplyTo { get; set; }
        public required string NameEN { get; set; }
        public required string NameVN { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsEdited { get; set; }
        public FeedbackTypeViewModel()
        {
            IsActive = true;
            IsDeleted = false;
            Priority = 1;
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
    }
}
