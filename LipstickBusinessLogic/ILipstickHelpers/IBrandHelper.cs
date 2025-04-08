using Common.Models;
using Common.ViewModels.LipstickViewModels;

namespace LipstickBusinessLogic.ILipstickHelpers
{
    public interface IBrandHelper : IBaseHelper<BrandViewModel>
    {
        public Task<Pagination<BrandViewModel>> GetAllAsync(int pageIndex, int pageSize);
    }
}
