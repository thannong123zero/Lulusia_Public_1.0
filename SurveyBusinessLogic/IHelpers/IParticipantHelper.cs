using Common.Models;
using Common.ViewModels.SurveyViewModels;

namespace SurveyBusinessLogic.IHelpers
{
    public interface IParticipantHelper
    {
        public Task<Pagination<ParticipantViewModel>> GetAllAsync(DateTime? startDate, DateTime? endDate, int surveyFormId, int pageIndex);
        public Task CreateAsync(SurveyUIViewModel model);
        public SurveyUIViewModel GetEagerCustomerSurveyByID(int ID);
        public Task<string> ExportExcel(DateTime? startDate, DateTime? endDate, int surveyFormId);
    }
}
