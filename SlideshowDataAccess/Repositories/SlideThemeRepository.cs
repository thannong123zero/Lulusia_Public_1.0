using SlideshowDataAccess.DTOs;
using SlideshowDataAccess.IRepositories;

namespace SlideshowDataAccess.Repositories
{
    public class SlideThemeRepository : GenericRepository<SlideThemeDTO, ApplicationContext>, ISlideThemeRepository
    {
        public SlideThemeRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
