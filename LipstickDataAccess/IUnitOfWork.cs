using LipstickDataAccess.IRepositories;

namespace LipstickDataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        #region Lipstick
        IBlogRepository BlogRepository { get; }
        IBrandRepository BrandRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ISubCategoryRepository SubCategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        ITopicRepository TopicRepository { get; }
        ISizeRepository SizeRepository { get; }
        IColorRepository ColorRepository { get; }
        IPageContentRepository PageContentRepository { get; }
        IPageTypeRepository PageTypeRepository { get; }
        IHomeBannerRepository HomeBannerRepository { get; }
        #endregion

        void BeginTransaction();
        void Commit();
        void Rollback();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
