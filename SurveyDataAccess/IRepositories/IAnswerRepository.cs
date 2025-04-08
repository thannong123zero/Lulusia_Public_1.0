using SurveyDataAccess.DTOs;

namespace SurveyDataAccess.IRepositories
{
    public interface IAnswerRepository : IGenericRepository<AnswerDTO, ApplicationContext>
    {
        public Task<List<AverageScoreDTO>> GetAverageScoreByDateAsync(int surveyId, DateTime startTime, DateTime finishTime, int optionQuestionTypeId);
    }
}
