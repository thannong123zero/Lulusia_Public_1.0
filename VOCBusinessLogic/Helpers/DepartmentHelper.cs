using AutoMapper;
using Common.ViewModels.VOCViewModelModels;
using VOCBusinessLogic.IHelpers;
using VOCDataAccess;
using VOCDataAccess.DTOs;

namespace VOCBusinessLogic.Helpers
{
    public class DepartmentHelper : IDepartmentHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentHelper(IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetAllAsync()
        {
            IEnumerable<DepartmentDTO> data = await _unitOfWork.DepartmentRepository.
                GetAllAsync(filter: s => !s.IsDeleted,
                orderBy: p => p.OrderBy(s => s.Priority).ThenByDescending(s => s.ModifiedOn));
            return _mapper.Map<IEnumerable<DepartmentViewModel>>(data);
        }
        public async Task<IEnumerable<DepartmentViewModel>> GetAllActiveAsync()
        {
            IEnumerable<DepartmentDTO> data = await _unitOfWork.DepartmentRepository.
                GetAllAsync(filter: s => !s.IsDeleted && s.IsActive && !string.IsNullOrEmpty(s.Emails),
                orderBy: p => p.OrderBy(s => s.Priority).ThenByDescending(s => s.ModifiedOn));
            return _mapper.Map<IEnumerable<DepartmentViewModel>>(data);
        }
        public async Task<DepartmentViewModel> GetByIdAsync(int id)
        {
            var data = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            return _mapper.Map<DepartmentViewModel>(data);
        }

        public async Task CreateAsync(DepartmentViewModel model)
        {
            DepartmentDTO department = _mapper.Map<DepartmentDTO>(model);
            await _unitOfWork.DepartmentRepository.CreateAsync(department);
            _unitOfWork.SaveChanges();
        }

        public async Task UpdateAsync(DepartmentViewModel model)
        {
            DepartmentDTO department = await _unitOfWork.DepartmentRepository.GetByIdAsync(model.Id);
            if (department == null)
            {
                return;
            }
            department.Name = model.Name;
            department.Description = model.Description;
            department.ModifiedOn = DateTime.Now;
            department.Priority = model.Priority;
            department.IsActive = model.IsActive;
            department.Emails = model.Emails;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return false;
            }
            department.IsDeleted = true;
            department.ModifiedOn = DateTime.Now;
            _unitOfWork.SaveChanges();
            return true;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RestoreAsync(int id)
        {
            throw new NotImplementedException();
        }


    }
}
