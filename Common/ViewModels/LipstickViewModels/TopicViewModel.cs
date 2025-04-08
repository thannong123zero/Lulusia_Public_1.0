using Microsoft.AspNetCore.Http;

namespace Common.ViewModels.LipstickViewModels
{
    public class TopicViewModel
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameVN { get; set; }
        public string? Note { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public bool InHomePage { get; set; }
        public string? Avatar { get; set; }
        public IFormFile? ImageFile { get; set; }
        public TopicViewModel()
        {
            IsActive = true;
        }
    }
}
