using SurveyDataAccess.DTOs;
using SurveyDataAccess.IRepositories;

namespace SurveyDataAccess.Repositories
{
    public class PredefinedAnswerRepository : GenericRepository<PredefinedAnswerDTO, ApplicationContext>, IPredefinedAnswerRepository
    {
        public PredefinedAnswerRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
