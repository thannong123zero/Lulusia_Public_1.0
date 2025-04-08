namespace Common.ViewModels.LipstickViewModels
{
    public class SizeViewModel
    {
        public int Id { get; set; }
        public string NameVN { get; set; }
        public string NameEN { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public SizeViewModel()
        {
            IsActive = true;
        }

    }
}
