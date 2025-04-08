using SlideshowDataAccess.DTOs;

namespace SlideshowDataAccess.IRepositories
{
    public interface ISlideRepository : IGenericRepository<SlideDTO, ApplicationContext>
    {
    }
}
