using Common.Models;

namespace LipstickBusinessLogic
{
    public interface IBaseHelper<T> where T : class
    {
        //public IEnumerable<T> GetAll();
        public IEnumerable<T> GetAllActive();
        public T GetById(int id);
        public bool Create(T model);
        public bool Update(T model);
        public bool SoftDelete(int id);
        public bool Restore(int id);
        public bool Delete(int id);
    }
}
