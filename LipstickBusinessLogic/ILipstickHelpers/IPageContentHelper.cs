using Common.Models;
using Common.ViewModels.LipstickViewModels;

namespace LipstickBusinessLogic.ILipstickHelpers
{
    public interface IPageContentHelper : IBaseHelper<PageContentViewModel>
    {
        public IEnumerable<PageContentViewModel> GetByPageTypeId(int pageTypeId);
        public Task<Pagination<PageContentViewModel>> GetAllAsync(int pageIndex,int pageSize);
    }
}
