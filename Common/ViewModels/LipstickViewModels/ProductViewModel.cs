using Microsoft.AspNetCore.Http;

namespace Common.ViewModels.LipstickViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public required string NameEN { get; set; }
        public required string NameVN { get; set; }
        public required string DescriptionEN { get; set; }
        public required string DescriptionVN { get; set; }
        public required string DetailsEN { get; set; }
        public required string DetailsVN { get; set; }
        public string? Images { get; set; }
        public string? Avatar { get; set; }
        public string? BackgroundImage { get; set; }
        //public bool InHomePage { get; set; }
        public bool IsRecommended { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int? DiscountPercent { get; set; }
        public DateTime? StartDiscountDate { get; set; }
        public DateTime? EndDiscountDate { get; set; }
        public bool SaleOff { get; set; }
        public double? SalePrice { get; set; }
        public IFormFile? AvatarFile { get; set; }
        public IFormFile? BackgroundFile { get; set; }
        public List<IFormFile>? ImageFiles { get; set; }
        public bool IsActive { get; set; }
        public ProductViewModel()
        {
            IsActive = true;
        }
    }
}
