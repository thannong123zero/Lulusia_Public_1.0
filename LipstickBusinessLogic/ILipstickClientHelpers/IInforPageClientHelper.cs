using Common.ViewModels.LipstickClientViewModels;

namespace LipstickBusinessLogic.ILipstickClientHelpers
{
    public interface IInforPageClientHelper
    {
        public InforPageClientViewModel? GetFirstDataByPageTypeId(int pageTypeId, string languge);
        //public IEnumerable<InforPageViewModel> GetByPageTypeId(int pageTypeId);

    }
}
