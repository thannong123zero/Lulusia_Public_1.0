using AutoMapper;
using Common.Models;
using Common.ViewModels.LipstickViewModels;
using LipstickBusinessLogic.ILipstickHelpers;
using LipstickDataAccess;
using LipstickDataAccess.DTOs;

namespace LipstickBusinessLogic.LipstickHelpers
{
    public class CategoryHelper : ICategoryHelper
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CategoryHelper(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public bool Create(CategoryViewModel model)
        {
            var data = _mapper.Map<CategoryDTO>(model);
            _unitOfWork.CategoryRepository.Create(data);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            var data = _unitOfWork.CategoryRepository.GetAll(filter: s => !s.IsDeleted);
            return _mapper.Map<IEnumerable<CategoryViewModel>>(data);
        }

        public IEnumerable<CategoryViewModel> GetAllActive()
        {
            var data = _unitOfWork.CategoryRepository.GetAll(filter: s => !s.IsDeleted && s.IsActive);
            return _mapper.Map<IEnumerable<CategoryViewModel>>(data);
        }

        public async Task<Pagination<CategoryViewModel>> GetAllAsync(int pageIndex, int pageSize)
        {
            var model = new Pagination<CategoryViewModel>();
            if(pageSize <= 0)
                pageSize = model.PageSize;
            var data = await _unitOfWork.CategoryRepository.GetAllAsync(filter: s => !s.IsDeleted && s.IsActive);
            model.TotalItems = data.Count();
            model.CurrentPage = pageIndex;
            model.TotalPages = (int)Math.Ceiling(model.TotalItems / (double)pageSize);

            data = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            IEnumerable<CategoryViewModel> viewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(data);
            model.Items = viewModels;
            return model;
        }

        public CategoryViewModel GetById(int id)
        {
            var data = _unitOfWork.CategoryRepository.GetById(id);
            return _mapper.Map<CategoryViewModel>(data);
        }

        public bool Restore(int id)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(CategoryViewModel model)
        {
            var data = _unitOfWork.CategoryRepository.GetById(model.Id);
            if (data == null)
            {
                return false;
            }
            data.NameEN = model.NameEN;
            data.NameVN = model.NameVN;
            data.Priority = model.Priority;
            data.IsActive = model.IsActive;
            data.InNavbar = model.InNavbar;
            data.Note = model.Note;
            data.ModifiedOn = DateTime.Now;
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
