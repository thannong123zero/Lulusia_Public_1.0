using Common.ViewModels.SurveyViewModels;

namespace SurveyBusinessLogic.IHelpers
{
    public interface ISurveyFormHelper : IBaseAsyncHelper<SurveyFormViewModel>
    {
        public Task<IEnumerable<SurveyFormViewModel>> GetAllAsync(int storeID);
        public Task<IEnumerable<SurveyFormViewModel>> GetAllAsync(int applyTo, int mallId, int officeId);
        public SurveyFormViewModel GetEagerSurveyFormByID(int ID);
        public Task<SurveyUIViewModel> GetEagerSurveyUIByID(int ID, string language);
        public SurveyFormViewModel GetSurveyFormByID(int ID);
    }
}
