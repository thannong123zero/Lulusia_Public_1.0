using Common.ViewModels.VOCViewModelModels;

namespace VOCBusinessLogic.IHelpers
{
    public interface IDepartmentHelper : IBaseAsyncHelper<DepartmentViewModel>
    {
        public Task<IEnumerable<DepartmentViewModel>> GetAllActiveAsync();
    }
}
