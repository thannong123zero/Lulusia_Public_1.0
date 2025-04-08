namespace SlideshowDataAccess.DTOs
{
    public class SlideDTO : BaseDTO
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int SlideThemeId { get; set; }
        public int SlideTime { get; set; }
        public int SlideType { get; set; }
        public string? VideoName { get; set; }
        public string? ImageName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }
        public SlideThemeDTO? SlideTheme { get; set; }
    }
}
