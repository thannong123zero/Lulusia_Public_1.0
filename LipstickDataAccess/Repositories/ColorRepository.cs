using LipstickDataAccess.DTOs;
using LipstickDataAccess.IRepositories;

namespace LipstickDataAccess.Repositories
{
    public class ColorRepository : GenericRepository<ColorDTO>, IColorRepository
    {
        public ColorRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}