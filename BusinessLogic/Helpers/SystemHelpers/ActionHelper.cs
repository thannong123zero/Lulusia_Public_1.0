using AutoMapper;
using BusinessLogic.IHelpers.ISystemHelpers;
using Common;
using Common.ViewModels.SystemViewModels;
using DataAccess;
using DataAccess.DTOs;

namespace BusinessLogic.Helpers.SystemHelpers
{
    public class ActionHelper : IActionHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ActionHelper(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(ActionViewModel model)
        {
            var data = _mapper.Map<ActionDTO>(model);
            _unitOfWork.ActionRepository.Create(data);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ActionViewModel>> GetAllAsync()
        {
            var data = await _unitOfWork.ActionRepository.GetAllAsync(orderBy: p => p.OrderBy(s => s.Priority));
            return _mapper.Map<IEnumerable<ActionViewModel>>(data);
        }

        public async Task<ActionViewModel> GetByIdAsync(int id)
        {
            var data = await _unitOfWork.ActionRepository.GetByIdAsync(id);
            return _mapper.Map<ActionViewModel>(data);
        }

        public async Task<List<string>> GetEActionsAsync()
        {
            var data = await _unitOfWork.ActionRepository.GetAllAsync();
            List<string> eActionList = Enum.GetNames(typeof(EActions)).ToList();
            foreach (var item in data)
            {
                eActionList.Remove(item.Label);
            }
            return eActionList;
        }

        public async Task<bool> UpdateAsync(ActionViewModel model)
        {
            var data = await _unitOfWork.ActionRepository.GetByIdAsync(model.Id);
            if (data == null)
            {
                return false;
            }
            data.Name = model.Name;
            data.Priority = model.Priority;
            data.ModifiedOn = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
