using Common.ViewModels.SurveyViewModels;

namespace SurveyBusinessLogic.IHelpers
{
    public interface IQuestionGroupHelper : IBaseAsyncHelper<QuestionGroupViewModel>
    {
        public IEnumerable<QuestionGroupViewModel> GetEagerAllElements(bool getActive = false);
        public QuestionGroupViewModel GetEagerQuestionGroupByID(int ID);
        public Task<IEnumerable<QuestionGroupViewModel>> GetAllAsync(int applyTo, bool getActive = false);
        public Task<IEnumerable<QuestionGroupViewModel>> GetAllByActiveAsync(bool getActive = false);
    }
}
