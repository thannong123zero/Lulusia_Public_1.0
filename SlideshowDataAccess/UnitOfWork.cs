using Microsoft.EntityFrameworkCore.Storage;
using SlideshowDataAccess.IRepositories;
using SlideshowDataAccess.Repositories;

namespace SlideshowDataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext context;
        private IDbContextTransaction? _transaction;
        private bool disposed = false;


        public ISlideThemeRepository SlideThemeRepository { get; private set; }

        public ISlideRepository SlideRepository { get; private set; }


        public UnitOfWork(ApplicationContext databaseContext)
        {
            context = databaseContext;
            SlideThemeRepository = new SlideThemeRepository(context);
            SlideRepository = new SlideRepository(context);

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
        public IDbContextTransaction BeginTransaction()
        {
            _transaction = context.Database.BeginTransaction();
            return _transaction;
        }
        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }
        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
