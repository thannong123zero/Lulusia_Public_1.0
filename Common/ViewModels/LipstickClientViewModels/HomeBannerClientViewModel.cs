namespace Common.ViewModels.LipstickClientViewModels
{
    public class HomeBannerClientViewModel
    {
        public int Id { get; set; }
        public int BannerTypeId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public string? RedirectUrl { get; set; }
        public List<string> Tags { get; set; }
        public HomeBannerClientViewModel()
        {
            Tags = new List<string>();
        }
    }
}
