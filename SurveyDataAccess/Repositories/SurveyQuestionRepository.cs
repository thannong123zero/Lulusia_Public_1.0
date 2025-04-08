using Microsoft.EntityFrameworkCore;
using SurveyDataAccess.DTOs;
using SurveyDataAccess.IRepositories;

namespace SurveyDataAccess.Repositories
{
    public class SurveyQuestionRepository : GenericRepository<SurveyQuestionDTO, ApplicationContext>, ISurveyQuestionRepository
    {
        private readonly DbSet<SurveyQuestionDTO> _surveyQuestion;
        public SurveyQuestionRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _surveyQuestion = dbContext.Set<SurveyQuestionDTO>();
        }
        public bool CheckExistenceByQuestionID(int questionID)
        {
            return _surveyQuestion.Any(s => s.QuestionId == questionID);
        }
        public bool CheckExistenceByQuestionGroupID(int questionGroupID)
        {
            return _surveyQuestion.Any(s => s.QuestionGroupId == questionGroupID);

        }

        public async Task<IEnumerable<SurveyQuestionDTO>> GetBySurveyFormID(int surveyFormId)
        {
            return await _surveyQuestion.Where(s => s.SurveyFormId == surveyFormId).ToListAsync();
        }
    }
}
