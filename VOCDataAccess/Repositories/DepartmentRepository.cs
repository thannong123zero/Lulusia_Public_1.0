using VOCDataAccess.DTOs;
using VOCDataAccess.IRepositories;

namespace VOCDataAccess.Repositories
{
    public class DepartmentRepository : GenericRepository<DepartmentDTO, ApplicationContext>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
