using AutoMapper;
using Common.ViewModels.SurveyViewModels;
using SurveyBusinessLogic.IHelpers;
using SurveyDataAccess;
using SurveyDataAccess.DTOs;

namespace SurveyBusinessLogic.Helpers
{
    public class QuestionHelper : IQuestionHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuestionHelper(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuestionViewModel>> GetAllAsync()
        {
            IEnumerable<QuestionDTO> data = await _unitOfWork.QuestionRepository.GetAllAsync(filter: s => !s.IsDeleted);
            return _mapper.Map<IEnumerable<QuestionViewModel>>(data);
        }
        public async Task<IEnumerable<QuestionViewModel>> GetAllAsync(int questionGroupID)
        {
            IEnumerable<QuestionDTO> data = await _unitOfWork.QuestionRepository
                .GetAllAsync(filter: s => questionGroupID == -1 ? !s.IsDeleted : s.QuestionGroupId == questionGroupID && !s.IsDeleted,
                orderBy: p => p.OrderByDescending(s => s.ModifiedOn));
            return _mapper.Map<IEnumerable<QuestionViewModel>>(data);
        }
        public async Task<IEnumerable<QuestionViewModel>> GetAllAsync(int applyTo, int? questionGroupId)
        {
            IEnumerable<QuestionGroupDTO> questionGroups = await _unitOfWork.QuestionGroupRepository.GetAllAsync(filter: s =>
                !s.IsDeleted &&
                s.IsActive,
                orderBy: p => p.OrderBy(s => s.ModifiedOn));

            List<int> questionGroupIds = questionGroups.ToList().Select(s => s.Id).ToList();

            IEnumerable<QuestionDTO> data = await _unitOfWork.QuestionRepository.GetAllAsync(filter: s =>
                (questionGroupId == null ? questionGroupIds.Contains(s.QuestionGroupId) :
                questionGroupId == -1 ? questionGroupIds.Contains(s.QuestionGroupId) :
                s.QuestionGroupId == questionGroupId) &&
                !s.IsDeleted,
                orderBy: p => p.OrderByDescending(s => s.ModifiedOn));
            return _mapper.Map<IEnumerable<QuestionViewModel>>(data);
        }
        public async Task<QuestionViewModel> GetByIdAsync(int id)
        {
            var data = _unitOfWork.QuestionRepository.GetEagerQuestionById(id);
            if (data == null)
            {
                return null;
            }
            QuestionViewModel questionViewModel = _mapper.Map<QuestionViewModel>(data);
            questionViewModel.IsEdited = _unitOfWork.SurveyQuestionRepository.CheckExistenceByQuestionID(id);
            return questionViewModel;
        }
        public async Task CreateAsync(QuestionViewModel model)
        {
            QuestionDTO question = _mapper.Map<QuestionDTO>(model);
            await _unitOfWork.QuestionRepository.CreateAsync(question);
            _unitOfWork.SaveChanges();
            //question.PredefinedAnswers.ToList().ForEach(s =>
            //{
            //    s.QuestionId = question.Id;
            //    _unitOfWork.PredefinedAnswerRepository.Create(s);
            //});
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateAsync(QuestionViewModel model)
        {
            QuestionDTO question = _unitOfWork.QuestionRepository.GetEagerQuestionById(model.Id);
            if (question == null)
                return;

            question.NameVN = model.NameVN;
            question.NameEN = model.NameEN;
            question.ChartLabel = model.ChartLabel;
            question.ModifiedOn = DateTime.Now;
            question.QuestionGroupId = model.QuestionGroupId;
            question.QuestionTypeId = model.QuestionTypeId;

            var optionAnswers = question.PredefinedAnswers.ToList();
            optionAnswers.ForEach(s =>
            {
                _unitOfWork.PredefinedAnswerRepository.Delete(s);
            });
            foreach (var item in model.PredefinedAnswers)
            {
                PredefinedAnswerDTO predefinedAnswer = _mapper.Map<PredefinedAnswerDTO>(item);
                predefinedAnswer.QuestionId = model.Id;
                _unitOfWork.PredefinedAnswerRepository.Create(predefinedAnswer);

            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> SoftDeleteAsync(int ID)
        {
            // Is question existed?
            bool checkExist = _unitOfWork.SurveyQuestionRepository.CheckExistenceByQuestionID(ID);
            // The question is existed!
            if (checkExist)
            {
                return false;
            }

            var employee = await _unitOfWork.QuestionRepository.GetByIdAsync(ID);
            if (employee != null)
            {
                employee.IsDeleted = true;
                employee.ModifiedOn = DateTime.Now;
                //employee.ModifiedBy = _userInformation.GetUserName();
                _unitOfWork.SaveChanges();
            }
            return true;
        }


        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RestoreAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
