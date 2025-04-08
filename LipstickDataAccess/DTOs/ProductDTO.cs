namespace LipstickDataAccess.DTOs
{
    public class ProductDTO : BaseDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public string NameEN { get; set; }
        public string NameVN { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionVN { get; set; }
        public string DetailsEN { get; set; }
        public string DetailsVN { get; set; }
        public string Images { get; set; }
        public string Avatar { get; set; }
        public string BackgroundImage { get; set; }
        public bool IsRecommended { get; set; }
        //
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int? DiscountPercent { get; set; }
        public double? SalePrice { get; set; }
        public DateTime? StartDiscountDate { get; set; }
        public DateTime? EndDiscountDate { get; set; }
        public bool SaleOff { get; set; }
    }
}
