using Common.ViewModels.VOCViewModelModels;

namespace VOCBusinessLogic.IHelpers
{
    public interface IFeedbackPriorityHelper
    {
        Task<IEnumerable<FeedbackPriorityViewModel>> GetAllAsync();
        Task<FeedbackPriorityViewModel> GetByIdAsync(int id);
        Task UpdateAsync(FeedbackPriorityViewModel model);
    }
}
