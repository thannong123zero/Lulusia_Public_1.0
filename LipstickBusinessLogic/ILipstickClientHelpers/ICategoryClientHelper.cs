using Common.ViewModels.LipstickClientViewModels;

namespace LipstickBusinessLogic.ILipstickClientHelpers
{
    public interface ICategoryClientHelper
    {
        public IEnumerable<CategoryClientViewModel> GetNavigationBar(string language);
        public IEnumerable<CategoryClientViewModel> GetMenu(string language);
    }
}
