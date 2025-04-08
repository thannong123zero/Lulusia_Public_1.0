using VOCDataAccess.DTOs;
using VOCDataAccess.IRepositories;

namespace VOCDataAccess.Repositories
{
    public class FeedbackTypeRepository : GenericRepository<FeedbackTypeDTO, ApplicationContext>, IFeedbackTypeRepository
    {
        public FeedbackTypeRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
