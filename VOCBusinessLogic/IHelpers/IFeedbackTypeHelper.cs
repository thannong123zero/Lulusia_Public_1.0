using Common.ViewModels.VOCClientViewModels;
using Common.ViewModels.VOCViewModelModels;

namespace VOCBusinessLogic.IHelpers
{
    public interface IFeedbackTypeHelper : IBaseAsyncHelper<FeedbackTypeViewModel>
    {
        public Task<IEnumerable<FeedbackTypeViewModel>> GetAllActiveAsync(int typeId);
        public Task<IEnumerable<FeedbackTypeViewModel>> GetAllAsync(int typeId);
        public Task<IEnumerable<FeedbackTypeClientViewModel>> GetAllAsync(string language, int typeId);
    }
}
