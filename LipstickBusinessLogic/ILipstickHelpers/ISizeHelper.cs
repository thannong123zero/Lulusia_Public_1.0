using Common.Models;
using Common.ViewModels.LipstickViewModels;

namespace LipstickBusinessLogic.ILipstickHelpers
{
    public interface ISizeHelper : IBaseHelper<SizeViewModel>
    {
        public Task<Pagination<SizeViewModel>> GetAllAsync(int pageIndex,int pageSize);
    }
}
