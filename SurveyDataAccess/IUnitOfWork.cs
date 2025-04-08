using SurveyDataAccess.IRepositories;

namespace SurveyDataAccess
{
    public interface IUnitOfWork : IDisposable
    {

        IParticipantRepository ParticipantRepository { get; }
        IPredefinedAnswerRepository PredefinedAnswerRepository { get; }
        IQuestionGroupRepository QuestionGroupRepository { get; }
        IQuestionTypeRepository QuestionTypeRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        ISurveyQuestionRepository SurveyQuestionRepository { get; }
        ISurveyFormRepository SurveyFormRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
