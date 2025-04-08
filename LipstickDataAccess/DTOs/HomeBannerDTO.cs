namespace LipstickDataAccess.DTOs
{
    public class HomeBannerDTO : BaseDTO
    {
        public int Id { get; set; }
        public int BannerTypeId { get; set; }
        public string ImageName { get; set; }
        public string? SubjectEN { get; set; }
        public string? SubjectVN { get; set; }
        public string? DescriptionEN { get; set; }
        public string? DescriptionVN { get; set; }
        public string? RedirectUrl { get; set; }
        public int Priority { get; set; }
    }
}
