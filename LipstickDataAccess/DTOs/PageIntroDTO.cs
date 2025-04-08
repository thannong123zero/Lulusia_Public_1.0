namespace LipstickDataAccess.DTOs
{
    public class PageIntroDTO : BaseDTO
    {
        public int Id { get; set; }
        public int PageTypeId { get; set; }
        public string TitleEN { get; set; }
        public string TitleVN { get; set; }
        public string ContentEN { get; set; }
        public string ContentVN { get; set; }
    }
}
