using Microsoft.EntityFrameworkCore;
using VOCDataAccess.DTOs;
using VOCDataAccess.IRepositories;

namespace VOCDataAccess.Repositories
{
    public class FeedbackRepository : GenericRepository<FeedbackDTO, ApplicationContext>, IFeedbackRepository
    {
        private readonly DbSet<FeedbackDTO> _feedbacks;
        public FeedbackRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _feedbacks = dbContext.Set<FeedbackDTO>();
        }
        public bool CheckExistenceByFeedbackTypeID(int feedbackTypeID)
        {
            return _feedbacks.Any(s => s.FeedbackTypeId == feedbackTypeID);
        }
        public bool CheckExistenceByStoreID(int storeID)
        {
            return _feedbacks.Any(s => s.MallId == storeID);
        }
        //public bool CheckExistenceByDepartmentID(int departmentID)
        //{
        //    return _feedbacks.Any(s => s.DepartmentId == departmentID);
        //}


        public async Task<FeedbackDTO> GetByCodeAsync(string code)
        {
            return await _feedbacks.FirstOrDefaultAsync(s => s.Code == code);
        }
    }
}
