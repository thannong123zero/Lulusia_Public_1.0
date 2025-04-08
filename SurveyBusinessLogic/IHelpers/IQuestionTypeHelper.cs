using Common.ViewModels.SurveyViewModels;

namespace SurveyBusinessLogic.IHelpers
{
    public interface IQuestionTypeHelper
    {
        public Task<IEnumerable<QuestionTypeViewModel>> GetAllAsync();
    }
}
