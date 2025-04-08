using Common.Models;
using Common.ViewModels.LipstickViewModels;

namespace LipstickBusinessLogic.ILipstickHelpers
{
    public interface IHomeBannerHelper : IBaseHelper<HomeBannerViewModel>
    {
        public IEnumerable<HomeBannerViewModel> GetByBannerTypeId(int bannerTypeId);
        public Task<Pagination<HomeBannerViewModel>> GetAllAsync(int pageIndex,int pageSize, int bannerTypeId);
    }
}
