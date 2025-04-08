using Common.Models;
using Common.ViewModels.VOCViewModelModels;

namespace VOCBusinessLogic.IHelpers
{
    public interface IFeedbackHelper
    {
        public Task<IEnumerable<FeedbackViewModel>> GetAllAsync();

        public Task<FeedbackViewModel> GetByIdAsync(int Id);
        public Task<FeedbackViewModel> GetByCodeAsync(string code, int forwardFeedbackId, string language);

        public Task UpdateAsync(FeedbackViewModel model, string token);
        /// <summary>
        /// Create feedback from CRM Application
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task CreateAsync(FeebackClientViewModel model, string token);
        /// <summary>
        /// Create feedback from Client Application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task CreateAsync(FeebackClientViewModel model);
        public Task<bool> ForwardFeedbackAsync(ForwardFeedbackViewModel model);
        public Task ResponseFeedbackByDeparmentAsync(ForwardFeedbackViewModel model);

        public Task<Pagination<FeedbackViewModel>> GetAllAsync(int? applyTo, int? mallId, int? pfficeId, int? feedbackTypeID, int? statusID, int? priorityId, DateTime startTime, DateTime finishTime, int pageIndex, string? phoneNumber);
        //public VOCParameterViewModel GetVOCParameter(string token, int? applyTo, int? mallId, int? officeId);
        public Task<FeedbackReportViewModel> GetReportAsync(int? applyTo, int? mallId, int? pfficeId, int? feedbackTypeID, int? statusID, int? priorityId, DateTime startTime, DateTime finishTime, string? phoneNumber);

    }
}