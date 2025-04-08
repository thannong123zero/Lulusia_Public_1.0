using Common.Models;
using Common.ViewModels.LipstickViewModels;

namespace LipstickBusinessLogic.ILipstickHelpers
{
    public interface IColorHelper : IBaseHelper<ColorViewModel>
    {
        public Task<Pagination<ColorViewModel>> GetAllAsync(int pageIndex,int pageSize);
    }
}
