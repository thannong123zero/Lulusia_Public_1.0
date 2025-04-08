using SurveyDataAccess.DTOs;
namespace SurveyDataAccess.IRepositories
{
    public interface ISurveyQuestionRepository : IGenericRepository<SurveyQuestionDTO, ApplicationContext>
    {
        public bool CheckExistenceByQuestionID(int questionID);
        public bool CheckExistenceByQuestionGroupID(int questionGroupID);
        public Task<IEnumerable<SurveyQuestionDTO>> GetBySurveyFormID(int surveyFormId);

    }
}
