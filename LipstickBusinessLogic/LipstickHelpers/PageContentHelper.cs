using AutoMapper;
using Common.Models;
using Common.ViewModels.LipstickViewModels;
using LipstickBusinessLogic.ILipstickHelpers;
using LipstickDataAccess;
using LipstickDataAccess.DTOs;

namespace LipstickBusinessLogic.LipstickHelpers
{
    public class PageContentHelper : IPageContentHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PageContentHelper(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public bool Create(PageContentViewModel model)
        {
            var data = _mapper.Map<PageContentDTO>(model);
            _unitOfWork.PageContentRepository.Create(data);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PageContentViewModel> GetAll()
        {
            var data = _unitOfWork.PageContentRepository.GetAll(filter: s => !s.IsDeleted);
            return _mapper.Map<IEnumerable<PageContentViewModel>>(data);
        }

        public IEnumerable<PageContentViewModel> GetAllActive()
        {
            var data = _unitOfWork.PageContentRepository.GetAll(filter: s => !s.IsDeleted && s.IsActive);
            return _mapper.Map<IEnumerable<PageContentViewModel>>(data);
        }

        public async Task<Pagination<PageContentViewModel>> GetAllAsync(int pageIndex, int pageSize)
        {
            var model = new Pagination<PageContentViewModel>();
            if (pageSize < 1)
                pageSize = model.PageSize;
            var data = await _unitOfWork.PageContentRepository.GetAllAsync(filter: s => !s.IsDeleted && s.IsActive);
            model.TotalItems = data.Count();
            model.CurrentPage = pageIndex;
            model.TotalPages = (int)Math.Ceiling(model.TotalItems / (double)pageSize);
            data = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            IEnumerable<PageContentViewModel> viewModels = _mapper.Map<IEnumerable<PageContentViewModel>>(data);
            model.Items = viewModels;
            return model;
        }

        public PageContentViewModel GetById(int id)
        {
            var data = _unitOfWork.PageContentRepository.GetById(id);
            return _mapper.Map<PageContentViewModel>(data);
        }

        public IEnumerable<PageContentViewModel> GetByPageTypeId(int pageTypeId)
        {
            var data = _unitOfWork.PageContentRepository.GetAll(filter: s => !s.IsDeleted && (pageTypeId == -1 ? true : s.PageTypeId == pageTypeId));
            return _mapper.Map<IEnumerable<PageContentViewModel>>(data);
        }

        public bool Restore(int id)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(PageContentViewModel model)
        {
            var data = _unitOfWork.PageContentRepository.GetById(model.Id);
            if (data == null)
            {
                return false;
            }
            data.TitleEN = model.TitleEN;
            data.TitleVN = model.TitleVN;
            data.ContentEN = model.ContentEN;
            data.ContentVN = model.ContentVN;
            data.IsActive = model.IsActive;
            data.ModifiedOn = DateTime.Now;
            data.Priority = model.Priority;
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
