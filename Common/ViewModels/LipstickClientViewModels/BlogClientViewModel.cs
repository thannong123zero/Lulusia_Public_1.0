namespace Common.ViewModels.LipstickClientViewModels
{
    public class BlogClientViewModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
