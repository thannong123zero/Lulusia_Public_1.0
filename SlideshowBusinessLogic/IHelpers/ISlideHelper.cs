using Common.ViewModels.SlideshowViewModels;

namespace SlideshowBusinessLogic.IHelpers
{
    public interface ISlideHelper : IBaseAsyncHelper<SlideViewModel>
    {
        public Task<IEnumerable<SlideViewModel>> GetSlidesByThemeIdAsync(int slideThemeId);
    }
}
