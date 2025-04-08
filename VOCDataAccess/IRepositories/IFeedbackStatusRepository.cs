using VOCDataAccess.DTOs;

namespace VOCDataAccess.IRepositories
{
    public interface IFeedbackStatusRepository : IGenericRepository<FeedbackStatusDTO, ApplicationContext>
    {
        public Task<FeedbackStatusDTO?> GetTheFirstAsync();
    }
}
