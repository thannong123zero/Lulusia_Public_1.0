using AutoMapper;
using Common.Models;
using Common.ViewModels.LipstickViewModels;
using LipstickBusinessLogic.ILipstickHelpers;
using LipstickDataAccess;
using LipstickDataAccess.DTOs;

namespace LipstickBusinessLogic.LipstickHelpers
{
    public class ColorHelper : IColorHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ColorHelper(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool Create(ColorViewModel model)
        {
            var data = _mapper.Map<ColorDTO>(model);
            _unitOfWork.ColorRepository.Create(data);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ColorViewModel> GetAll()
        {
            var data = _unitOfWork.ColorRepository.GetAll(filter: s => !s.IsDeleted);
            return _mapper.Map<IEnumerable<ColorViewModel>>(data);
        }

        public IEnumerable<ColorViewModel> GetAllActive()
        {
            var data = _unitOfWork.ColorRepository.GetAll(filter: s => !s.IsDeleted && s.IsActive);
            return _mapper.Map<IEnumerable<ColorViewModel>>(data);
        }

        public async Task<Pagination<ColorViewModel>> GetAllAsync(int pageIndex, int pageSize)
        {
            var model = new Pagination<ColorViewModel>();
            if (pageSize <= 0)
                pageSize = model.PageSize;
            var data = await _unitOfWork.ColorRepository.GetAllAsync(filter: s => !s.IsDeleted && s.IsActive);
            model.TotalItems = data.Count();
            model.CurrentPage = pageIndex;
            model.TotalPages = (int)Math.Ceiling(model.TotalItems / (double)pageSize);

            data = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            IEnumerable<ColorViewModel> viewModels = _mapper.Map<IEnumerable<ColorViewModel>>(data);
            model.Items = viewModels;
            return model;
        }

        public ColorViewModel GetById(int id)
        {
            var data = _unitOfWork.ColorRepository.GetById(id);
            return _mapper.Map<ColorViewModel>(data);
        }

        public bool Restore(int id)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ColorViewModel model)
        {
            var data = _unitOfWork.ColorRepository.GetById(model.Id);
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
