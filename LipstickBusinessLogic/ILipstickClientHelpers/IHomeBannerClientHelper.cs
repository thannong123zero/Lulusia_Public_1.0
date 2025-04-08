using Common.ViewModels.LipstickClientViewModels;
namespace LipstickBusinessLogic.ILipstickClientHelpers
{
    public interface IHomeBannerClientHelper
    {
        public IEnumerable<HomeBannerClientViewModel> GetAllActive(string language);
    }
}
