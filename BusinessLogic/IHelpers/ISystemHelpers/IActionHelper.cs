using Common.ViewModels.SystemViewModels;

namespace BusinessLogic.IHelpers.ISystemHelpers
{
    public interface IActionHelper
    {
        public Task<IEnumerable<ActionViewModel>> GetAllAsync();
        public Task<List<string>> GetEActionsAsync();
        public Task<ActionViewModel> GetByIdAsync(int id);
        public Task<bool> CreateAsync(ActionViewModel model);
        public Task<bool> UpdateAsync(ActionViewModel model);
    }
}
