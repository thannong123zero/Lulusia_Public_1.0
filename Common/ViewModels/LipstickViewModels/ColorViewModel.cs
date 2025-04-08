namespace Common.ViewModels.LipstickViewModels
{
    public class ColorViewModel
    {
        public int Id { get; set; }
        public string NameVN { get; set; }
        public string NameEN { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public ColorViewModel() 
        { 
            IsActive = true;
        }
    }
}
