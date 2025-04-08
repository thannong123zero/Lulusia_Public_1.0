using LipstickDataAccess.DTOs;
using LipstickDataAccess.IRepositories;
namespace LipstickDataAccess.Repositories
{
    public class ProductRepository : GenericRepository<ProductDTO>, IProductRepository
    {
        public ProductRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
