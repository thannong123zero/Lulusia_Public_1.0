using Common.ViewModels.LipstickClientViewModels;

namespace LipstickBusinessLogic.ILipstickClientHelpers
{
    public interface IBrandClientHelper
    {
        public IEnumerable<BrandClientViewModel> GetAllActive(string language);
        //public IEnumerable<BrandClientViewModel> GetMainBanners(string language);
        //public IEnumerable<BrandClientViewModel> GetSubBanners(string language);

    }
}
