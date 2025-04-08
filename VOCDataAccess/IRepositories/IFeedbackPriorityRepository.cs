using VOCDataAccess.DTOs;

namespace VOCDataAccess.IRepositories
{
    public interface IFeedbackPriorityRepository : IGenericRepository<FeedbackPriorityDTO, ApplicationContext>
    {
        public Task<FeedbackPriorityDTO?> GetTheFirstAsync();

    }
}
