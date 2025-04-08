using Common.Models;
using Common.ViewModels.LipstickViewModels;

namespace LipstickBusinessLogic.ILipstickHelpers
{
    public interface IBlogHelper : IBaseHelper<BlogViewModel>
    {
        public Task<Pagination<BlogViewModel>> GetAllAsync(int pageIndex);
        public IEnumerable<BlogViewModel> GetByTopicId(int topicId);
    }
}
