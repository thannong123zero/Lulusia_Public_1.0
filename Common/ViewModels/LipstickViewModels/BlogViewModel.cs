using Microsoft.AspNetCore.Http;

namespace Common.ViewModels.LipstickViewModels
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string SubjectEN { get; set; }
        public string SubjectVN { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionVN { get; set; }
        public string ContentEN { get; set; }
        public string ContentVN { get; set; }
        public string? Avatar { get; set; }
        public bool IsActive { get; set; }
        public IFormFile ImageFile { get; set; }
        public BlogViewModel()
        {
            IsActive = true;
        }
    }
}
