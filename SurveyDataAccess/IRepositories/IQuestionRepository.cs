using SurveyDataAccess.DTOs;

namespace SurveyDataAccess.IRepositories
{
    public interface IQuestionRepository : IGenericRepository<QuestionDTO, ApplicationContext>
    {
        public QuestionDTO? GetEagerQuestionById(int id);
        public bool CheckExistenceByQuestionGroupId(int questionGroupId);
    }
}
