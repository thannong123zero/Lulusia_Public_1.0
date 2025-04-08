using Common.ViewModels.SurveyViewModels;

namespace SurveyBusinessLogic.IHelpers
{
    public interface IQuestionHelper : IBaseAsyncHelper<QuestionViewModel>
    {
        public Task<IEnumerable<QuestionViewModel>> GetAllAsync(int questionGroupID);
        public Task<IEnumerable<QuestionViewModel>> GetAllAsync(int applyTo, int? questionGroupId);
    }
}
