using LipstickDataAccess.DTOs;
using LipstickDataAccess.IRepositories;

namespace LipstickDataAccess.Repositories
{
    public class MenuGroupRepository : GenericRepository<CategoryDTO>, ICategoryRepository
    {
        public MenuGroupRepository(ApplicationContext dbContext) : base(dbContext) { }
    }
}
