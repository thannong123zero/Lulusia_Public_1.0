namespace SlideshowDataAccess.DTOs
{
    public class SlideThemeDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<SlideDTO> Slides { get; set; }
        public SlideThemeDTO()
        {
            Title = string.Empty;
            Description = string.Empty;
            Slides = new List<SlideDTO>();
        }
    }
}
