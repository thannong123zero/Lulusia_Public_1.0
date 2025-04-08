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
    public class BrandHelper : IBrandHelper
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IFileStorageService _imageStorageService;
        public BrandHelper(IMapper mapper, IUnitOfWork unitOfWork,
            IFileStorageService imageStorageService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _imageStorageService = imageStorageService;
        }

        public bool Create(BrandViewModel model)
        {
            var data = _mapper.Map<BrandDTO>(model);
            if (model.ImageFile != null)
            {
                data.Avatar = _imageStorageService.SaveImageFile([EModules.Lipstick.ToString(), EFolderNames.Brands.ToString()], model.ImageFile);
            }
            _unitOfWork.BrandRepository.Create(data);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BrandViewModel> GetAll()
        {
            var data = _unitOfWork.BrandRepository.GetAll(filter: s => !s.IsDeleted);
            return _mapper.Map<IEnumerable<BrandViewModel>>(data);
        }

        public IEnumerable<BrandViewModel> GetAllActive()
        {
            var data = _unitOfWork.BrandRepository.GetAll(filter: s => !s.IsDeleted && s.IsActive);
            return _mapper.Map<IEnumerable<BrandViewModel>>(data);
        }

        public async Task<Pagination<BrandViewModel>> GetAllAsync(int pageIndex, int pageSize)
        {
            var model = new Pagination<BrandViewModel>();
            if (pageSize <= 0)
                pageSize = model.PageSize;
            var data = await _unitOfWork.BrandRepository.GetAllAsync(filter: s => !s.IsDeleted && s.IsActive);
            model.TotalItems = data.Count();
            model.CurrentPage = pageIndex;
            model.TotalPages = (int)Math.Ceiling(model.TotalItems / (double)pageSize);
            data = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            IEnumerable<BrandViewModel> viewModels = _mapper.Map<IEnumerable<BrandViewModel>>(data);
            model.Items = viewModels;
            return model;
        }

        public BrandViewModel GetById(int id)
        {
            var data = _unitOfWork.BrandRepository.GetById(id);
            return _mapper.Map<BrandViewModel>(data);
        }

        public bool Restore(int id)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(BrandViewModel model)
        {
            var data = _unitOfWork.BrandRepository.GetById(model.Id);
            if (data == null)
            {
                return false;
            }
            data.Priority = model.Priority;
            data.IsActive = model.IsActive;
            data.Name = model.Name;
            data.Note = model.Note;
            if (model.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(data.Avatar))
                {
                    _imageStorageService.DeleteFile(data.Avatar);
                }
                data.Avatar = _imageStorageService.SaveImageFile([EModules.Lipstick.ToString(), EFolderNames.Brands.ToString()], model.ImageFile);
            }
            _unitOfWork.SaveChanges();
            return true;

        }
    }
}
