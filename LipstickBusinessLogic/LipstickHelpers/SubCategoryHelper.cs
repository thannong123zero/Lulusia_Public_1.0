using AutoMapper;
using Common.Models;
using Common.ViewModels.LipstickViewModels;
using LipstickBusinessLogic.ILipstickHelpers;
using LipstickDataAccess;
using LipstickDataAccess.DTOs;

namespace LipstickBusinessLogic.LipstickHelpers
{
    public class SubCategoryHelper : ISubCategoryHelper
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public SubCategoryHelper(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public bool Create(SubCategoryViewModel model)
        {
            var data = _mapper.Map<SubCategoryDTO>(model);
            _unitOfWork.SubCategoryRepository.Create(data);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubCategoryViewModel> GetAll()
        {
            var data = _unitOfWork.SubCategoryRepository.GetAll(filter: s => !s.IsDeleted);
            return _mapper.Map<IEnumerable<SubCategoryViewModel>>(data);
        }

        public IEnumerable<SubCategoryViewModel> GetAllActive()
        {
            var data = _unitOfWork.SubCategoryRepository.GetAll(filter: s => !s.IsDeleted && s.IsActive);
            return _mapper.Map<IEnumerable<SubCategoryViewModel>>(data);
        }

        public async Task<Pagination<SubCategoryViewModel>> GetAllAsync(int pageIndex, int pageSize, int categoryId)
        {
            var model = new Pagination<SubCategoryViewModel>();
            if (pageSize <= 0)
                pageSize = model.PageSize;
            var data = await _unitOfWork.SubCategoryRepository
                .GetAllAsync(filter: s => !s.IsDeleted && s.IsActive &&
                (categoryId > 0 ? s.CategoryId == categoryId : true),
                orderBy: s => s.OrderBy(p => p.CreatedOn));
            model.TotalItems = data.Count();
            model.CurrentPage = pageIndex;
            model.TotalPages = (int)Math.Ceiling(model.TotalItems / (double)pageSize);

            data = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            IEnumerable<SubCategoryViewModel> viewModels = _mapper.Map<IEnumerable<SubCategoryViewModel>>(data);
            model.Items = viewModels;
            return model;
        }

        public IEnumerable<SubCategoryViewModel> GetByCategoryId(int categoryId)
        {
            var data = _unitOfWork.SubCategoryRepository.GetAll(filter: s => !s.IsDeleted && (categoryId == -1 ? true : s.CategoryId == categoryId));
            return _mapper.Map<IEnumerable<SubCategoryViewModel>>(data);
        }

        public SubCategoryViewModel GetById(int id)
        {
            var data = _unitOfWork.SubCategoryRepository.GetById(id);
            return _mapper.Map<SubCategoryViewModel>(data);
        }

        public bool Restore(int id)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(SubCategoryViewModel model)
        {
            var data = _unitOfWork.SubCategoryRepository.GetById(model.Id);
            if (data == null)
            {
                return false;
            }
            data.Priority = model.Priority;
            data.Note = model.Note;
            data.NameEN = model.NameEN;
            data.NameVN = model.NameVN;
            data.ModifiedOn = DateTime.Now;
            data.CategoryId = model.CategoryId;
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
