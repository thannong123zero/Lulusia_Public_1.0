using AutoMapper;
using Common;
using Common.Models;
using Common.Services.FileStorageServices;
using Common.ViewModels.LipstickViewModels;
using LipstickBusinessLogic.ILipstickHelpers;
using LipstickDataAccess;
using LipstickDataAccess.DTOs;

namespace LipstickBusinessLogic.LipstickHelpers
{
    public class HomeBannerHelper : IHomeBannerHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _imageStorageService;
        public HomeBannerHelper(IMapper mapper, IUnitOfWork unitOfWork, IFileStorageService imageStorageService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _imageStorageService = imageStorageService;
        }
        public bool Create(HomeBannerViewModel model)
        {
            var data = _mapper.Map<HomeBannerDTO>(model);
            if (model.ImageFile == null)
            {
                return false;
            }
            data.ImageName = _imageStorageService.SaveImageFile([EModules.Lipstick.ToString(), EFolderNames.HomeBanners.ToString()], model.ImageFile);
            _unitOfWork.HomeBannerRepository.Create(data);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HomeBannerViewModel> GetAll()
        {
            var data = _unitOfWork.HomeBannerRepository.GetAll(filter: s => !s.IsDeleted, orderBy: p => p.OrderBy(s => s.Priority));
            return _mapper.Map<IEnumerable<HomeBannerViewModel>>(data);
        }

        public IEnumerable<HomeBannerViewModel> GetAllActive()
        {
            var data = _unitOfWork.HomeBannerRepository.GetAll(filter: s => !s.IsDeleted && s.IsActive, orderBy: p => p.OrderBy(s => s.Priority));
            return _mapper.Map<IEnumerable<HomeBannerViewModel>>(data);
        }

        public async Task<Pagination<HomeBannerViewModel>> GetAllAsync(int pageIndex, int pageSize, int bannerTypeId)
        {
            var model = new Pagination<HomeBannerViewModel>();
            if(pageSize <= 0)
                pageSize = model.PageSize;
            var data = await _unitOfWork.HomeBannerRepository.GetAllAsync(filter: s => !s.IsDeleted && s.IsActive && (bannerTypeId == -1 ? true: s.BannerTypeId == bannerTypeId));
            model.TotalItems = data.Count();
            model.CurrentPage = pageIndex;
            model.TotalPages = (int)Math.Ceiling(model.TotalItems / (double)pageSize);

            data = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            IEnumerable<HomeBannerViewModel> viewModels = _mapper.Map<IEnumerable<HomeBannerViewModel>>(data);
            model.Items = viewModels;
            return model;
        }

        public IEnumerable<HomeBannerViewModel> GetByBannerTypeId(int bannerTypeId)
        {
            var data = _unitOfWork.HomeBannerRepository.GetAll(filter: s => !s.IsDeleted && (bannerTypeId == -1 ? true : s.BannerTypeId == bannerTypeId), orderBy: p => p.OrderBy(s => s.Priority));
            return _mapper.Map<IEnumerable<HomeBannerViewModel>>(data);
        }

        public HomeBannerViewModel GetById(int id)
        {
            var data = _unitOfWork.HomeBannerRepository.GetById(id);
            return _mapper.Map<HomeBannerViewModel>(data);
        }

        public bool Restore(int id)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(HomeBannerViewModel model)
        {
            var data = _unitOfWork.HomeBannerRepository.GetById(model.Id);
            if (data == null)
            {
                return false;
            }
            data.SubjectVN = model.SubjectVN;
            data.SubjectEN = model.SubjectEN;
            data.DescriptionEN = model.DescriptionEN;
            data.DescriptionVN = model.DescriptionVN;
            data.IsActive = model.IsActive;
            data.RedirectUrl = model.RedirectUrl;
            data.Priority = model.Priority;
            data.BannerTypeId = model.BannerTypeId;
            if (model.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(data.ImageName))
                {
                    _imageStorageService.DeleteFile(data.ImageName);
                }
                data.ImageName = _imageStorageService.SaveImageFile([EModules.Lipstick.ToString(), EFolderNames.HomeBanners.ToString()], model.ImageFile);
            }
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
