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
    public class TopicHelper : ITopicHelper
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IFileStorageService _imageStorageService;

        public TopicHelper(IMapper mapper, IUnitOfWork unitOfWork, IFileStorageService imageStorageService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _imageStorageService = imageStorageService;

        }

        public bool Create(TopicViewModel model)
        {
            var data = _mapper.Map<TopicDTO>(model);
            if (model.ImageFile != null)
            {
                data.Avatar = _imageStorageService.SaveImageFile([EModules.Lipstick.ToString(), EFolderNames.Topics.ToString()], model.ImageFile);
            }
            _unitOfWork.TopicRepository.Create(data);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TopicViewModel> GetAll()
        {
            var data = _unitOfWork.TopicRepository.GetAll(filter: s => !s.IsDeleted);
            return _mapper.Map<IEnumerable<TopicViewModel>>(data);

        }

        public IEnumerable<TopicViewModel> GetAllActive()
        {
            var data = _unitOfWork.TopicRepository.GetAll(filter: s => !s.IsDeleted && s.IsActive);
            return _mapper.Map<IEnumerable<TopicViewModel>>(data);
        }

        public async Task<Pagination<TopicViewModel>> GetAllAsync(int pageIndex, int pageSize)
        {
            var model = new Pagination<TopicViewModel>();
            if(pageSize <= 0)
                pageSize = model.PageSize;
            var data = await _unitOfWork.TopicRepository.GetAllAsync(filter: s => !s.IsDeleted && s.IsActive);
            model.TotalItems = data.Count();
            model.CurrentPage = pageIndex;
            model.TotalPages = (int)Math.Ceiling(model.TotalItems / (double)pageSize);

            data = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            IEnumerable<TopicViewModel> viewModels = _mapper.Map<IEnumerable<TopicViewModel>>(data);
            model.Items = viewModels;
            return model;
        }

        public TopicViewModel GetById(int id)
        {
            var data = _unitOfWork.TopicRepository.GetById(id);
            return _mapper.Map<TopicViewModel>(data);
        }

        public bool Restore(int id)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(TopicViewModel model)
        {
            var data = _unitOfWork.TopicRepository.GetById(model.Id);
            if (data == null)
            {
                return false;
            }
            data.Priority = model.Priority;
            data.IsActive = model.IsActive;
            data.InHomePage = model.InHomePage;
            data.NameEN = model.NameEN;
            data.NameVN = model.NameVN;
            data.Note = model.Note;
            data.ModifiedOn = DateTime.Now;
            if (model.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(data.Avatar))
                {
                    _imageStorageService.DeleteFile(data.Avatar);
                }
                data.Avatar = _imageStorageService.SaveImageFile([EModules.Lipstick.ToString(), EFolderNames.Topics.ToString()], model.ImageFile);
            }
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
