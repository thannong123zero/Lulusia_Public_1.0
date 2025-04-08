namespace VOCBusinessLogic.IHelpers
{
    public interface IBaseHelper<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public IEnumerable<T> GetAllActive();
        public T GetById(int id);
        public void Create(T model);
        public void Update(T model);
        public bool SoftDelete(int id);
        public void Restore(int id);
        public void Delete(int id);
    }
}
