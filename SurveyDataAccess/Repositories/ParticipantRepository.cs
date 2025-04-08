using Microsoft.EntityFrameworkCore;
using SurveyDataAccess.DTOs;
using SurveyDataAccess.IRepositories;

namespace SurveyDataAccess.Repositories
{
    public class ParticipantRepository : GenericRepository<ParticipantDTO, ApplicationContext>, IParticipantRepository
    {
        private readonly DbSet<ParticipantDTO> _participant;
        public ParticipantRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _participant = dbContext.Set<ParticipantDTO>();
        }
        public ParticipantDTO? GetEagerParticipantById(int id)
        {
            var data = _participant.Where(s => s.Id == id).Include(s => s.Answers).FirstOrDefault();
            return data;
        }
        //public bool CheckExistenceBySurveyFormID(int surveyFormID)
        //{
        //    var temp = _participant.Where(s => s.SurveyFormId == surveyFormID).FirstOrDefault();
        //    if (temp != null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
