using Microsoft.EntityFrameworkCore.Storage;
using VOCDataAccess.IRepositories;
using VOCDataAccess.Repositories;

namespace VOCDataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext context;
        private bool disposed = false;
        private IDbContextTransaction? _transaction;

        public IFeedbackPriorityRepository FeedbackPriorityRepository { get; private set; }
        public IFeedbackStatusRepository FeedbackStatusRepository { get; private set; }
        public IDepartmentRepository DepartmentRepository { get; private set; }

        public IFeedbackRepository FeedbackRepository { get; private set; }

        public IForwardFeedbackRepository ForwardFeedbackRepository { get; private set; }
        public IFeedbackTypeRepository FeedbackTypeRepository { get; private set; }

        public UnitOfWork(ApplicationContext databaseContext)
        {
            context = databaseContext;
            FeedbackPriorityRepository = new FeedbackPriorityRepository(context);
            FeedbackStatusRepository = new FeedbackStatusRepository(context);
            DepartmentRepository = new DepartmentRepository(context);
            FeedbackRepository = new FeedbackRepository(context);
            ForwardFeedbackRepository = new ForwardFeedbackRepository(context);
            FeedbackTypeRepository = new FeedbackTypeRepository(context);
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
