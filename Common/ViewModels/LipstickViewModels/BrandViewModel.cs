using Microsoft.AspNetCore.Http;

namespace Common.ViewModels.LipstickViewModels
{
    public class BrandViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Note { get; set; }
        public string? Avatar { get; set; }
        public int Priority { get; set; }
        public IFormFile? ImageFile { get; set; }
        public bool IsActive { get; set; }
        public BrandViewModel()
        {
            IsActive = true;
        }
    }
}
