using LipstickDataAccess.DTOs;

namespace LipstickDataAccess.IRepositories
{
    public interface IPageContentRepository : IGenericRepository<PageContentDTO>
    {
        PageContentDTO? GetFirstDataByPageTypeId(int pageTypeId);
    }
}
