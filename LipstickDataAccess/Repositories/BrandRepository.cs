using LipstickDataAccess.DTOs;
using LipstickDataAccess.IRepositories;

namespace LipstickDataAccess.Repositories
{
    public class BrandRepository : GenericRepository<BrandDTO>, IBrandRepository
    {
        public BrandRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
