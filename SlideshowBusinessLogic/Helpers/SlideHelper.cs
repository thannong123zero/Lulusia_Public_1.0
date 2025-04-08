using AutoMapper;
using Common;
using Common.Services.FileStorageServices;
using Common.ViewModels.SlideshowViewModels;
using SlideshowBusinessLogic.IHelpers;
using SlideshowDataAccess;
using SlideshowDataAccess.DTOs;

namespace SlideshowBusinessLogic.Helpers
{
    public class SlideHelper : ISlideHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;


        public SlideHelper(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }
        public void Create(SlideViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(SlideViewModel model)
        {
            try
            {
                var data = _mapper.Map<SlideViewModel, SlideDTO>(model);
                data.CreatedOn = DateTime.Now;
                if (model.ImageFile != null || model.VideoFile != null)
                {

                    if (model.ImageFile != null && model.SlideType == (int)ESlideTypes.Image)
                    {

                        data.ImageName = _fileStorageService.SaveImageFile([EModules.SlideShow.ToString(), EFolderNames.Images.ToString()], model.ImageFile);
                    }
                    if (model.VideoFile != null && model.SlideType == (int)ESlideTypes.Video)
                    {
                        data.VideoName = _fileStorageService.SaveImageFile([EModules.SlideShow.ToString(), EFolderNames.Videos.ToString()], model.VideoFile);
                    }
                }

                await _unitOfWork.SlideRepository.CreateAsync(data);
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SlideViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SlideViewModel> GetAllActive()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SlideViewModel>> GetAllAsync()
        {
            var data = await _unitOfWork.SlideRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SlideDTO>, IEnumerable<SlideViewModel>>(data);
        }

        public SlideViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SlideViewModel> GetByIdAsync(int id)
        {
            var data = await _unitOfWork.SlideRepository.GetByIdAsync(id);
            return _mapper.Map<SlideDTO, SlideViewModel>(data);
        }

        public async Task<IEnumerable<SlideViewModel>> GetSlidesByThemeIdAsync(int slideThemeId)
        {
            var data = await _unitOfWork.SlideRepository
                .GetAllAsync(filter: s => (slideThemeId == -1 ? true : s.SlideThemeId == slideThemeId)
                && !s.IsDeleted && s.IsActive, orderBy: p => p.OrderBy(s => s.SlideThemeId).ThenBy(s => s.Priority));
            return _mapper.Map<IEnumerable<SlideDTO>, IEnumerable<SlideViewModel>>(data);
        }

        public void Restore(int id)
        {
            throw new NotImplementedException();
        }

        public Task RestoreAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(SlideViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(SlideViewModel model)
        {
            var data = await _unitOfWork.SlideRepository.GetByIdAsync(model.Id);
            if (data == null)
            {
                throw new Exception("Data not found");
            }
            data.Title = model.Title;
            data.Description = model.Description;
            data.IsActive = model.IsActive;
            data.SlideTime = model.SlideTime;
            data.SlideType = model.SlideType;
            data.Priority = model.Priority;
            if (model.ImageFile != null || model.VideoFile != null)
            {
                if (model.ImageFile != null && model.SlideType == (int)ESlideTypes.Image)
                {
                    if (!string.IsNullOrEmpty(data.ImageName))
                    {
                        _fileStorageService.DeleteFile(data.ImageName);
                    }
                    data.ImageName = _fileStorageService.SaveImageFile([EModules.SlideShow.ToString(), EFolderNames.Images.ToString()], model.ImageFile);
                }
                if (model.VideoFile != null && model.SlideType == (int)ESlideTypes.Video)
                {
                    if (!string.IsNullOrEmpty(data.VideoName))
                    {
                        _fileStorageService.DeleteFile(data.VideoName);
                    }
                    data.VideoName = _fileStorageService.SaveImageFile([EModules.SlideShow.ToString(), EFolderNames.Images.ToString()], model.VideoFile);
                }
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
