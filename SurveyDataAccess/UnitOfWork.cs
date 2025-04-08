using SurveyDataAccess.IRepositories;
using SurveyDataAccess.Repositories;

namespace SurveyDataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext context;
        private bool disposed = false;

        public IParticipantRepository ParticipantRepository { get; private set; }

        public IPredefinedAnswerRepository PredefinedAnswerRepository { get; private set; }

        public IQuestionGroupRepository QuestionGroupRepository { get; private set; }
        public IQuestionTypeRepository QuestionTypeRepository { get; private set; }

        public IQuestionRepository QuestionRepository { get; private set; }

        public ISurveyQuestionRepository SurveyQuestionRepository { get; private set; }

        public ISurveyFormRepository SurveyFormRepository { get; private set; }

        public IAnswerRepository AnswerRepository { get; private set; }
        public UnitOfWork(ApplicationContext databaseContext)
        {
            context = databaseContext;
            ParticipantRepository = new ParticipantRepository(context);
            PredefinedAnswerRepository = new PredefinedAnswerRepository(context);
            QuestionGroupRepository = new QuestionGroupRepository(context);
            QuestionTypeRepository = new QuestionTypeRepository(context);
            QuestionRepository = new QuestionRepository(context);
            SurveyQuestionRepository = new SurveyQuestionRepository(context);
            SurveyFormRepository = new SurveyFormRepository(context);
            AnswerRepository = new AnswerRepository(context);
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }
        public void Commit()
        {
            throw new NotImplementedException();
        }
        public void Rollback()
        {
            throw new NotImplementedException();
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
