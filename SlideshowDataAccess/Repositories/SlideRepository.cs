using SlideshowDataAccess.DTOs;
using SlideshowDataAccess.IRepositories;

namespace SlideshowDataAccess.Repositories
{
    public class SlideRepository : GenericRepository<SlideDTO, ApplicationContext>, ISlideRepository
    {
        public SlideRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
