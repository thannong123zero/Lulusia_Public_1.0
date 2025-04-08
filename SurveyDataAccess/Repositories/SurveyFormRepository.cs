using Microsoft.EntityFrameworkCore;
using SurveyDataAccess.DTOs;
using SurveyDataAccess.IRepositories;

namespace SurveyDataAccess.Repositories
{
    public class SurveyFormRepository : GenericRepository<SurveyFormDTO, ApplicationContext>, ISurveyFormRepository
    {
        private readonly DbSet<SurveyFormDTO> _surveyForms;
        private readonly DbSet<SurveyQuestionDTO> _surveyQuestion;

        public SurveyFormRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _surveyForms = dbContext.Set<SurveyFormDTO>();
            _surveyQuestion = dbContext.Set<SurveyQuestionDTO>();
        }
        public void RemoveSelectQuestionBySurveyFormID(int surveyFormID)
        {
            List<SurveyQuestionDTO> selectQuestions = _surveyQuestion.Where(s => s.SurveyFormId == surveyFormID).ToList();
            selectQuestions.ForEach(s =>
            {
                _surveyQuestion.Remove(s);
            });
        }
        public SurveyFormDTO? GetEagerSurveyFormByID(int ID)
        {
            var data = _surveyForms.Where(s => s.Id == ID).Include(s => s.SurveyQuestions).FirstOrDefault();
            return data;
        }
        public SurveyFormDTO GetEagerActiveSurverFormByID(int ID)
        {
            SurveyFormDTO surveyForm = _surveyForms.Where(s => s.Id == ID && s.IsActive && s.StartDate.Date <= DateTime.Now.Date && s.EndDate.Date >= DateTime.Now.Date).FirstOrDefault();
            if (surveyForm != null)
            {
                surveyForm.SurveyQuestions = _surveyQuestion.Where(s => s.SurveyFormId == surveyForm.Id).ToList();
                return surveyForm;
            }
            return null;
        }
        public bool CheckExistenceByStoreID(int storeID)
        {
            return _surveyForms.Any(s => s.MallId == storeID);
        }
    }
}
