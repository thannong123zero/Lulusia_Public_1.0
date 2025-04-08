using SurveyDataAccess.DTOs;
namespace SurveyDataAccess.IRepositories
{
    public interface IQuestionGroupRepository : IGenericRepository<QuestionGroupDTO, ApplicationContext>
    {
        public IEnumerable<QuestionGroupDTO> GetEagerAllElements();
        public QuestionGroupDTO? GetEagerQuestionGroupById(int id);
    }
}
