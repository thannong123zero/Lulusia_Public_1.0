namespace LipstickDataAccess.DTOs
{
    public class ColorDTO : BaseDTO
    {
        public int Id { get; set; }
        public string NameVN { get; set; }
        public string NameEN { get; set; }
        public int Priority { get; set; }
    }
}
