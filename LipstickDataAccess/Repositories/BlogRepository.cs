using LipstickDataAccess.DTOs;
using LipstickDataAccess.IRepositories;

namespace LipstickDataAccess.Repositories
{
    public class BlogRepository : GenericRepository<BlogDTO>, IBlogRepository
    {
        public BlogRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
