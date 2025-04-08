using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.LipstickViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameVN { get; set; }
        public string? Note { get; set; }
        public bool InNavbar { get; set; }
        [Range(1, 9999)]
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public CategoryViewModel()
        {
            IsActive = true;
        }
    }
}
