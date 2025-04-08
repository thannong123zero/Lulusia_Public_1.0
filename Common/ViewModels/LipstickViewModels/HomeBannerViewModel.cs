using Microsoft.AspNetCore.Http;

namespace Common.ViewModels.LipstickViewModels
{
    public class HomeBannerViewModel
    {
        public int Id { get; set; }
        public int BannerTypeId { get; set; }
        public string? Animation { get; set; }
        public string? ImageName { get; set; }
        public string? SubjectEN { get; set; }
        public string? SubjectVN { get; set; }
        public string? DescriptionEN { get; set; }
        public string? DescriptionVN { get; set; }
        public string? RedirectUrl { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }
        public string? Tag { get; set; }
        public IFormFile? ImageFile { get; set; }
        public HomeBannerViewModel()
        {
            IsActive = true;
        }
    }
}
