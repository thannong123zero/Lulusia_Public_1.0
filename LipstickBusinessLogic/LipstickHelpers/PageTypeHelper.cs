using AutoMapper;
using Common;
using Common.Models;
using Common.ViewModels.LipstickViewModels;
using LipstickBusinessLogic.ILipstickHelpers;
using LipstickDataAccess;
using LipstickDataAccess.DTOs;

namespace LipstickBusinessLogic.LipstickHelpers
{
    public class PageTypeHelper : IPageTypeHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PageTypeHelper(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool Create(PageTypeViewModel model)
        {
            var data = _mapper.Map<PageTypeDTO>(model);
            _unitOfWork.PageTypeRepository.Create(data);
            _unitOfWork.SaveChanges();
            return true;
        }

        public IEnumerable<PageTypeViewModel> GetAllActive()
        {
            var data = _unitOfWork.PageTypeRepository.GetAll();
            return _mapper.Map<IEnumerable<PageTypeViewModel>>(data);
        }

        public async Task<Pagination<PageTypeViewModel>> GetAllAsync(int pageIndex, int pageSize)
        {
            var model = new Pagination<PageTypeViewModel>();
            if (pageSize <= 0)
                pageSize = model.PageSize;
            var data = await _unitOfWork.PageTypeRepository.GetAllAsync();
            model.TotalItems = data.Count();
            model.CurrentPage = pageIndex;
            model.TotalPages = (int)Math.Ceiling(model.TotalItems / (double)pageSize);

            data = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            IEnumerable<PageTypeViewModel> viewModels = _mapper.Map<IEnumerable<PageTypeViewModel>>(data);
            model.Items = viewModels;
            return model;
        }

        public PageTypeViewModel GetById(int id)
        {
            var data = _unitOfWork.PageTypeRepository.GetById(id);
            return _mapper.Map<PageTypeViewModel>(data);
        }

        public async Task<List<string>> GetEPageTypesAsync()
        {
            var data = await _unitOfWork.PageTypeRepository.GetAllAsync();
            List<string> enumList = Enum.GetNames(typeof(EPageTypes)).ToList();
            foreach (var item in data)
            {
                enumList.Remove(item.Label);
            }
            return enumList;
        }
        
        public bool Update(PageTypeViewModel model)
        {
            var data = _unitOfWork.PageTypeRepository.GetById(model.Id);
            if (data == null)
            {
                return false;
            }
            data.Name = model.Name;
            data.Label = model.Label;
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
