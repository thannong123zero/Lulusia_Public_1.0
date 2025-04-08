namespace Common.ViewModels.LipstickClientViewModels
{
    public class CategoryClientViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<SubCategoryClientViewModel> SubCategories { get; set; }
        public CategoryClientViewModel()
        {
            SubCategories = new List<SubCategoryClientViewModel>();
        }
    }
}
