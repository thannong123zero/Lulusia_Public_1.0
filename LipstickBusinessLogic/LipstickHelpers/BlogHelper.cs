using AutoMapper;
using Common;
using Common.Models;
using Common.Services.FileStorageServices;
using Common.ViewModels.LipstickViewModels;
using Common.ViewModels.VOCViewModelModels;
using LipstickBusinessLogic.ILipstickHelpers;
using LipstickDataAccess;
using LipstickDataAccess.DTOs;

namespace LipstickBusinessLogic.LipstickHelpers
{
    public class BlogHelper : IBlogHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _imageStorageService;
        public BlogHelper(IMapper mapper, IUnitOfWork unitOfWork, IFileStorageService imageStorageService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _imageStorageService = imageStorageService;

        }

        public bool Create(BlogViewModel model)
        {
            var data = _mapper.Map<BlogDTO>(model);
            if (model.ImageFile != null)
            {
                data.Avatar = _imageStorageService.SaveImageFile([EModules.Lipstick.ToString(), EFolderNames.Blogs.ToString()], model.ImageFile);
            }
            _unitOfWork.BlogRepository.Create(data);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogViewModel> GetAll()
        {
            var data = _unitOfWork.BlogRepository.GetAll(filter: s => !s.IsDeleted);
            return _mapper.Map<IEnumerable<BlogViewModel>>(data);
        }

        public IEnumerable<BlogViewModel> GetAllActive()
        {
            var data = _unitOfWork.BlogRepository.GetAll(filter: s => !s.IsDeleted && s.IsActive);
            return _mapper.Map<IEnumerable<BlogViewModel>>(data);
        }

        public async Task<Pagination<BlogViewModel>> GetAllAsync(int pageIndex)
        {
            var model = new Pagination<BlogViewModel>();
            var data = await _unitOfWork.BlogRepository.GetAllAsync(filter: s => !s.IsDeleted && s.IsActive);
            model.TotalItems = data.Count();
            model.CurrentPage = pageIndex;
            model.TotalPages = (int)Math.Ceiling(model.TotalItems / (double)model.PageSize);

            data = data.Skip((pageIndex - 1) * model.PageSize).Take(model.PageSize);

            IEnumerable<BlogViewModel> viewModels = _mapper.Map<IEnumerable<BlogViewModel>>(data);
            model.Items = viewModels;
            return model;
        }

        public BlogViewModel GetById(int id)
        {
            var data = _unitOfWork.BlogRepository.GetById(id);
            return _mapper.Map<BlogViewModel>(data);
        }

        public IEnumerable<BlogViewModel> GetByTopicId(int topicId)
        {
            var data = _unitOfWork.BlogRepository.GetAll(filter: s => !s.IsDeleted && (topicId == -1 ? true : s.TopicId == topicId));
            return _mapper.Map<IEnumerable<BlogViewModel>>(data);
        }

        public bool Restore(int id)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(BlogViewModel model)
        {
            var data = _unitOfWork.BlogRepository.GetById(model.Id);
            if (data == null)
            {
                return false;
            }
            data.IsActive = model.IsActive;
            data.DescriptionEN = model.DescriptionEN;
            data.DescriptionVN = model.DescriptionVN;
            data.ContentEN = model.ContentEN;
            data.ContentVN = model.ContentVN;
            data.SubjectEN = model.SubjectEN;
            data.SubjectVN = model.SubjectVN;
            data.ModifiedOn = DateTime.Now;
            if (model.ImageFile != null)
            {
                if (data.Avatar != null)
                {
                    _imageStorageService.DeleteFile(data.Avatar);
                }
                data.Avatar = _imageStorageService.SaveImageFile([EModules.Lipstick.ToString(), EFolderNames.Blogs.ToString()], model.ImageFile);
            }
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
