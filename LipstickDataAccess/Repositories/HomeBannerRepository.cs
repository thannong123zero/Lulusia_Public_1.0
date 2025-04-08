using LipstickDataAccess.DTOs;
using LipstickDataAccess.IRepositories;

namespace LipstickDataAccess.Repositories
{
    public class HomeBannerRepository : GenericRepository<HomeBannerDTO>, IHomeBannerRepository
    {
        public HomeBannerRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
