using Microsoft.EntityFrameworkCore.Storage;
using SlideshowDataAccess.IRepositories;

namespace SlideshowDataAccess
{
    public interface IUnitOfWork : IDisposable
    {

        public ISlideThemeRepository SlideThemeRepository { get; }
        public ISlideRepository SlideRepository { get; }
        IDbContextTransaction BeginTransaction();
        void Commit();
        void Rollback();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
