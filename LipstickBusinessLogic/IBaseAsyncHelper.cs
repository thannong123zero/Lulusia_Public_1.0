using Common.Models;

namespace LipstickBusinessLogic
{
    public interface IBaseAsyncHelper<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<Pagination<T>> GetAllAsync(int pageIndex);
        public Task<T> GetByIdAsync(int id);
        public Task<bool> CreateAsync(T model);
        public Task<bool> UpdateAsync(T model);
        public Task<bool> SoftDeleteAsync(int id);
        public Task<bool> RestoreAsync(int id);
        public Task<bool> DeleteAsync(int id);
    }
}
