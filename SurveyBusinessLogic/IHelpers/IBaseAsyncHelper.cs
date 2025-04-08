namespace SurveyBusinessLogic.IHelpers
{
    public interface IBaseAsyncHelper<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task CreateAsync(T model);
        public Task UpdateAsync(T model);
        public Task<bool> SoftDeleteAsync(int id);
        public Task RestoreAsync(int id);
        public Task DeleteAsync(int id);
    }
}
