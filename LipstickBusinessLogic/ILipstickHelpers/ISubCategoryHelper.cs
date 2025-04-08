using Common.Models;
using Common.ViewModels.LipstickViewModels;

namespace LipstickBusinessLogic.ILipstickHelpers
{
    public interface ISubCategoryHelper : IBaseHelper<SubCategoryViewModel>
    {
        public Task<Pagination<SubCategoryViewModel>> GetAllAsync(int pageIndex, int pageSize, int categoryId);
    }
}
