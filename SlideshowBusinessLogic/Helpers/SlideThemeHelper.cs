using AutoMapper;
using Common.ViewModels.SlideshowViewModels;
using SlideshowBusinessLogic.IHelpers;
using SlideshowDataAccess;
using SlideshowDataAccess.DTOs;

namespace SlideshowBusinessLogic.Helpers
{
    public class SlideThemeHelper : ISlideThemeHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SlideThemeHelper(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(SlideThemeViewModel model)
        {
            var data = _mapper.Map<SlideThemeDTO>(model);
            data.CreatedOn = DateTime.Now;
            await _unitOfWork.SlideThemeRepository.CreateAsync(data);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<SlideThemeViewModel>> GetAllAsync()
        {
            var data = await _unitOfWork.SlideThemeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SlideThemeViewModel>>(data);
        }

        public async Task<SlideThemeViewModel> GetByIdAsync(int id)
        {
            var data = await _unitOfWork.SlideThemeRepository.GetByIdAsync(id);
            return _mapper.Map<SlideThemeViewModel>(data);
        }

        public Task RestoreAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var data = await _unitOfWork.SlideThemeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return false;
            }
            data.IsActive = true;
            data.ModifiedOn = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public void Update(SlideThemeViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(SlideThemeViewModel model)
        {
            var data = await _unitOfWork.SlideThemeRepository.GetByIdAsync(model.Id);
            if (data == null)
            {
                return;
            }
            data.Title = model.Title;
            data.Description = model.Description;
            data.ModifiedOn = DateTime.Now;
            data.IsActive = model.IsActive;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
