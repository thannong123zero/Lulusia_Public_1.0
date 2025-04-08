using Microsoft.EntityFrameworkCore;
using VOCDataAccess.DTOs;
using VOCDataAccess.IRepositories;

namespace VOCDataAccess.Repositories
{
    public class FeedbackStatusRepository : GenericRepository<FeedbackStatusDTO, ApplicationContext>, IFeedbackStatusRepository
    {
        private DbSet<FeedbackStatusDTO> _feedbackStatuses;
        public FeedbackStatusRepository(ApplicationContext context) : base(context)
        {
            _feedbackStatuses = context.Set<FeedbackStatusDTO>();
        }

        public async Task<FeedbackStatusDTO?> GetTheFirstAsync()
        {
            var data = await _feedbackStatuses.OrderBy(s => s.Priority).FirstOrDefaultAsync();
            return data;
        }
    }
}
