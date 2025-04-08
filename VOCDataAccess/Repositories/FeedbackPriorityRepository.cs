using Microsoft.EntityFrameworkCore;
using VOCDataAccess.DTOs;
using VOCDataAccess.IRepositories;

namespace VOCDataAccess.Repositories
{
    public class FeedbackPriorityRepository : GenericRepository<FeedbackPriorityDTO, ApplicationContext>, IFeedbackPriorityRepository
    {
        private DbSet<FeedbackPriorityDTO> _feedbackPriorities;
        public FeedbackPriorityRepository(ApplicationContext context) : base(context)
        {
            _feedbackPriorities = context.Set<FeedbackPriorityDTO>();
        }

        public async Task<FeedbackPriorityDTO?> GetTheFirstAsync()
        {
            var data = await _feedbackPriorities.OrderBy(s => s.Priority).FirstOrDefaultAsync();
            return data;
        }
    }
}
