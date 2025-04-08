using Common.Models;
using Common.ViewModels.LipstickViewModels;

namespace LipstickBusinessLogic.ILipstickHelpers
{
    public interface ICategoryHelper : IBaseHelper<CategoryViewModel>
    {
        public Task<Pagination<CategoryViewModel>> GetAllAsync(int pageIndex, int pageSize);
    }
}
