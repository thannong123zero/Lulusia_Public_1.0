using Microsoft.EntityFrameworkCore;
using SurveyDataAccess.DTOs;
using SurveyDataAccess.IRepositories;

namespace SurveyDataAccess.Repositories
{
    public class QuestionRepository : GenericRepository<QuestionDTO, ApplicationContext>, IQuestionRepository
    {
        private readonly DbSet<QuestionDTO> _questions;
        public QuestionRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _questions = dbContext.Set<QuestionDTO>();
        }



        public QuestionDTO? GetEagerQuestionById(int id)
        {
            var question = _questions.Where(s => s.Id == id).Include(s => s.PredefinedAnswers).FirstOrDefault();
            return question;
        }
        public bool CheckExistenceByQuestionGroupId(int questionGroupId)
        {
            return _questions.Any(s => s.QuestionGroupId == questionGroupId);
        }

    }
}
