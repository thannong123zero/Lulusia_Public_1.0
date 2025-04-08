using VOCDataAccess.DTOs;

namespace VOCDataAccess.IRepositories
{
    public interface IFeedbackRepository : IGenericRepository<FeedbackDTO, ApplicationContext>
    {
        public Task<FeedbackDTO> GetByCodeAsync(string code);
    }
}
