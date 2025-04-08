using LipstickDataAccess.DTOs;
using LipstickDataAccess.IRepositories;

namespace LipstickDataAccess.Repositories
{
    public class SizeRepository : GenericRepository<SizeDTO>, ISizeRepository
    {
        public SizeRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
