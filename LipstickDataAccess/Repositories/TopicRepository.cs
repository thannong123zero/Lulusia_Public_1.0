using LipstickDataAccess.DTOs;
using LipstickDataAccess.IRepositories;

namespace LipstickDataAccess.Repositories
{
    public class TopicRepository : GenericRepository<TopicDTO>, ITopicRepository
    {
        public TopicRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
