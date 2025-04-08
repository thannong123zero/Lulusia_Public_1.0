using LipstickDataAccess.DTOs;
using LipstickDataAccess.IRepositories;

namespace LipstickDataAccess.Repositories
{
    public class SubCategoryRepository : GenericRepository<SubCategoryDTO>, ISubCategoryRepository
    {
        public SubCategoryRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
