namespace Common.ViewModels.LipstickViewModels
{
    public class PageContentViewModel
    {
        public int Id { get; set; }
        public int PageTypeId { get; set; }
        public required string TitleEN { get; set; }
        public required string TitleVN { get; set; }
        public required string ContentEN { get; set; }
        public required string ContentVN { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }
        public PageContentViewModel()
        {
            IsActive = true;
        }
    }
}
