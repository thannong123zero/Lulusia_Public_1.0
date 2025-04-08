using VOCDataAccess.DTOs;
using VOCDataAccess.IRepositories;

namespace VOCDataAccess.Repositories
{
    public class ForwardFeedbackRepository : GenericRepository<ForwardFeedbackDTO, ApplicationContext>, IForwardFeedbackRepository
    {
        public ForwardFeedbackRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
