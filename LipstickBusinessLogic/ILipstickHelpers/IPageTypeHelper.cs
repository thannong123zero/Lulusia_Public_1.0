using Common.Models;
using Common.ViewModels.LipstickViewModels;

namespace LipstickBusinessLogic.ILipstickHelpers
{
    public interface IPageTypeHelper 
    {
        public Task<Pagination<PageTypeViewModel>> GetAllAsync(int pageIndex, int pageSize);
        public IEnumerable<PageTypeViewModel> GetAllActive();
        public Task<List<string>> GetEPageTypesAsync();
        public bool Create(PageTypeViewModel model);
        public bool Update(PageTypeViewModel model);
        public PageTypeViewModel GetById(int id);
    }
}
