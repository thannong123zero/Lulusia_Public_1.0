using Common.ViewModels.VOCViewModelModels;
namespace VOCBusinessLogic.IHelpers
{
    public interface IFeedbackStatusHelper
    {
        Task<IEnumerable<FeedbackStatusViewModel>> GetAllAsync();
        Task<FeedbackStatusViewModel> GetByIdAsync(int id);
        Task UpdateAsync(FeedbackStatusViewModel model);
    }
}
