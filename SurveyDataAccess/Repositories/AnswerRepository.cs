using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SurveyDataAccess.DTOs;
using SurveyDataAccess.IRepositories;

namespace SurveyDataAccess.Repositories
{
    public class AnswerRepository : GenericRepository<AnswerDTO, ApplicationContext>, IAnswerRepository
    {
        private readonly ApplicationContext _context;
        public AnswerRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<AverageScoreDTO>> GetAverageScoreByDateAsync(int surveyId, DateTime startTime, DateTime finishTime, int optionQuestionTypeId)
        {
            try
            {
                var parameters = new[]
                    {
                    new SqlParameter("@surveyId", surveyId),
                    new SqlParameter("@startTime", startTime),
                    new SqlParameter("@finishTime", finishTime),
                    new SqlParameter("@optionQuestionTypeId", optionQuestionTypeId)
                };

                var results = await _context.Database.SqlQueryRaw<AverageScoreDTO>("EXEC [dbo].[GetAverageScoreByDate] @surveyId, @startTime, @finishTime, @optionQuestionTypeId", parameters).ToListAsync();
                //.FromSqlRaw("EXEC [dbo].[GetAverageScoreByDate] @surveyId, @startTime, @finishTime, @optionQuestionTypeId", parameters)
                //.ToListAsync();

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
