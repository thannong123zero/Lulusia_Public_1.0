using Microsoft.EntityFrameworkCore;
using SurveyDataAccess.DTOs;
using SurveyDataAccess.IRepositories;

namespace SurveyDataAccess.Repositories
{
    public class QuestionGroupRepository : GenericRepository<QuestionGroupDTO, ApplicationContext>, IQuestionGroupRepository
    {
        private readonly DbSet<QuestionGroupDTO> _questionGroups;
        private readonly DbSet<QuestionDTO> _questions;
        public QuestionGroupRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _questionGroups = dbContext.Set<QuestionGroupDTO>();
            _questions = dbContext.Set<QuestionDTO>();
        }
        public IEnumerable<QuestionGroupDTO> GetEagerAllElements()
        {
            var data = _questionGroups.Include(s => s.Questions.Where(p => !p.IsDeleted)).ToList();
            return data;
        }
        public QuestionGroupDTO? GetEagerQuestionGroupById(int id)
        {
            var data = _questionGroups.Where(s => s.Id == id).Include(s => s.Questions).ThenInclude(s => s.PredefinedAnswers).FirstOrDefault();
            return data;
        }
    }
}
