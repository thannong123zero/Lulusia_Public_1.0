namespace LipstickDataAccess.DTOs
{
    public class BrandDTO : BaseDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Note { get; set; }
        public string? Avatar { get; set; }
        public int Priority { get; set; }
        //public ICollection<ProductDTO> Products { get; set; }
        //public BrandDTO()
        //{
        //    Products = new List<ProductDTO>();
        //}
    }
}
