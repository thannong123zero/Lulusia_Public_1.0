using Common.Custom.CustomDataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.VOCViewModelModels
{
    public class FeebackClientViewModel
    {
        public int? MallId { get; set; }
        public int? OfficeId { get; set; }
        [Required]
        public int FeedbackTypeId { get; set; }
        public int PriorityId { get; set; }
        [Required]
        [FullNameValidation]
        public string FullName { get; set; }
        [Required]
        [PhoneNumberValidation]
        public string PhoneNumber { get; set; }
        [EmailValidation]
        //[EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public bool IsMobile { get; set; }
        public List<IFormFile> ImageFiles { get; set; }
        public int ApplyTo { get; set; }
        public FeebackClientViewModel()
        {
            ImageFiles = new List<IFormFile>();
            IsMobile = true;
            FullName = string.Empty;
            Content = string.Empty;
            PhoneNumber = string.Empty;
            Title = string.Empty;
        }
    }
}
