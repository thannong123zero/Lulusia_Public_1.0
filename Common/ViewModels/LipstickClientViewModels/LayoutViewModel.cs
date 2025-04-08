namespace Common.ViewModels.LipstickClientViewModels
{
    public class LayoutViewModel
    {
        public string HostName { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string YoutubeLink { get; set; }
        public string GithubLink { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public IEnumerable<CategoryClientViewModel>? Categories { get; set; }

    }
}
