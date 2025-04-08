using Common.Models;
using Common.ViewModels.LipstickViewModels;
namespace LipstickBusinessLogic.ILipstickHelpers
{
    public interface ITopicHelper : IBaseHelper<TopicViewModel>
    {
        public Task<Pagination<TopicViewModel>> GetAllAsync(int pageIndex, int pageSize);
    }
}
