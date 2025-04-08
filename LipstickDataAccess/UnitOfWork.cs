using LipstickDataAccess.IRepositories;
using LipstickDataAccess.Repositories;
namespace LipstickDataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext context;
        private bool disposed = false;
        #region Lipstick
        public IBlogRepository BlogRepository { get; private set; }
        public IBrandRepository BrandRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public ISubCategoryRepository SubCategoryRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public ITopicRepository TopicRepository { get; private set; }
        public ISizeRepository SizeRepository { get; private set; }
        public IColorRepository ColorRepository { get; private set; }
        public IPageContentRepository PageContentRepository { get; private set; }
        public IPageTypeRepository PageTypeRepository { get; private set; }
        public IHomeBannerRepository HomeBannerRepository { get; private set; }
        #endregion

        public UnitOfWork(ApplicationContext context)
        {
            this.context = context;
            #region Lipstick
            BlogRepository = new BlogRepository(context);
            BrandRepository = new BrandRepository(context);
            CategoryRepository = new MenuGroupRepository(context);
            SubCategoryRepository = new SubCategoryRepository(context);
            ProductRepository = new ProductRepository(context);
            TopicRepository = new TopicRepository(context);
            SizeRepository = new SizeRepository(context);
            ColorRepository = new ColorRepository(context);
            TopicRepository = new TopicRepository(context);
            PageContentRepository = new PageContentRepository(context);
            PageTypeRepository = new PageTypeRepository(context);
            HomeBannerRepository = new HomeBannerRepository(context);
            #endregion
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }
        public void Commit()
        {
            throw new NotImplementedException();
        }
        public void Rollback()
        {
            throw new NotImplementedException();
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
