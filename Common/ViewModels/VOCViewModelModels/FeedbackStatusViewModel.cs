namespace Common.ViewModels.VOCViewModelModels
{
    public class FeedbackStatusViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Priority { get; set; }
        public string? Description { get; set; }
        public string? BackgroundStyle { get; set; }

    }
}
