using AutoMapper;
using Common.Models;
using Common.ViewModels.LipstickViewModels;
using LipstickBusinessLogic.ILipstickHelpers;
using LipstickDataAccess;
using LipstickDataAccess.DTOs;

namespace LipstickBusinessLogic.LipstickHelpers
{
    public class SizeHelper : ISizeHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SizeHelper(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool Create(SizeViewModel model)
        {
            var data = _mapper.Map<SizeDTO>(model);
            _unitOfWork.SizeRepository.Create(data);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SizeViewModel> GetAll()
        {
            var data = _unitOfWork.SizeRepository.GetAll(filter: s => !s.IsDeleted);
            return _mapper.Map<IEnumerable<SizeViewModel>>(data);
        }

        public IEnumerable<SizeViewModel> GetAllActive()
        {
            var data = _unitOfWork.SizeRepository.GetAll(filter: s => !s.IsDeleted && s.IsActive);
            return _mapper.Map<IEnumerable<SizeViewModel>>(data);
        }

        public async Task<Pagination<SizeViewModel>> GetAllAsync(int pageIndex, int pageSize)
        {
            var model = new Pagination<SizeViewModel>();
            if (pageSize <= 0)
                pageSize = model.PageSize;
            var data = await _unitOfWork.SizeRepository.GetAllAsync(filter: s => !s.IsDeleted && s.IsActive);
            model.TotalItems = data.Count();
            model.CurrentPage = pageIndex;
            model.TotalPages = (int)Math.Ceiling(model.TotalItems / (double)pageSize);

            data = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            IEnumerable<SizeViewModel> viewModels = _mapper.Map<IEnumerable<SizeViewModel>>(data);
            model.Items = viewModels;
            return model;
        }

        public SizeViewModel GetById(int id)
        {
            var data = _unitOfWork.SizeRepository.GetById(id);
            return _mapper.Map<SizeViewModel>(data);
        }

        public bool Restore(int id)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(SizeViewModel model)
        {
            var data = _unitOfWork.SizeRepository.GetById(model.Id);
            if (data == null)
            {
                return false;
            }
            data.Priority = model.Priority;
            data.NameEN = model.NameEN;
            data.NameVN = model.NameVN;
            data.ModifiedOn = DateTime.Now;
            data.IsActive = model.IsActive;
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
