using AutoMapper;
using Common;
using Common.ViewModels.SurveyViewModels;
using SurveyBusinessLogic.IHelpers;
using SurveyDataAccess;
using SurveyDataAccess.DTOs;

namespace SurveyBusinessLogic.Helpers
{
    public class SurveyFormHelper : ISurveyFormHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IQuestionGroupHelper _questionGroupHelper;
        private readonly IQuestionHelper _questionHelper;
        public SurveyFormHelper(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IQuestionGroupHelper questionGroupHelper,
            IQuestionHelper questionHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _questionGroupHelper = questionGroupHelper;
            _questionHelper = questionHelper;
        }
        public async Task<IEnumerable<SurveyFormViewModel>> GetAllAsync()
        {
            IEnumerable<SurveyFormDTO> data = await _unitOfWork.SurveyFormRepository.GetAllAsync(s => s.IsDeleted == false);
            return _mapper.Map<IEnumerable<SurveyFormViewModel>>(data);
        }
        public async Task<IEnumerable<SurveyFormViewModel>> GetAllAsync(int storeID)
        {
            IEnumerable<SurveyFormDTO> data = await _unitOfWork.SurveyFormRepository.GetAllAsync(s => s.IsDeleted == false && storeID == -1 ? true : s.MallId == storeID);
            return _mapper.Map<IEnumerable<SurveyFormViewModel>>(data);
        }
        public async Task<SurveyFormViewModel> GetByIdAsync(int ID)
        {
            var data = await _unitOfWork.SurveyFormRepository.GetByIdAsync(ID);
            return _mapper.Map<SurveyFormViewModel>(data);
        }
        public SurveyFormViewModel GetEagerSurveyFormByID(int ID)
        {
            var data = _unitOfWork.SurveyFormRepository.GetEagerSurveyFormByID(ID);
            return _mapper.Map<SurveyFormViewModel>(data);
        }

        public async Task<SurveyUIViewModel> GetEagerSurveyUIByID(int ID, string language)
        {
            var data = _unitOfWork.SurveyFormRepository.GetEagerSurveyFormByID(ID);
            if (data == null)
                return null;
            SurveyFormViewModel surveyFormViewModel = _mapper.Map<SurveyFormViewModel>(data);

            SurveyUIViewModel surveyUIViewModel = new SurveyUIViewModel();
            surveyUIViewModel.SurveyFormID = surveyFormViewModel.Id;
            surveyUIViewModel.MallId = surveyFormViewModel.MallId;
            surveyUIViewModel.OfficeId = surveyFormViewModel.OfficeId;
            if (string.Equals(language, ELanguages.VN.ToString()))
            {
                surveyUIViewModel.Title = surveyFormViewModel.TitleVN;
                surveyUIViewModel.Description = surveyFormViewModel.DescriptionVN;
            }
            else
            {
                surveyUIViewModel.Title = surveyFormViewModel.TitleEN;
                surveyUIViewModel.Description = surveyFormViewModel.DescriptionEN;
            }
            List<QuestionGroupUIViewModel> questionGroupUIs = await QuestionGroupUI(language, surveyFormViewModel);
            surveyUIViewModel.QuestionGroupUIs = questionGroupUIs;
            return surveyUIViewModel;
        }
        public SurveyFormViewModel GetSurveyFormByID(int ID)
        {
            SurveyFormDTO surveyForm = _unitOfWork.SurveyFormRepository.GetEagerActiveSurverFormByID(ID);
            return _mapper.Map<SurveyFormViewModel>(surveyForm);
        }
        public async Task CreateAsync(SurveyFormViewModel model)
        {
            SurveyFormDTO surveyForm = _mapper.Map<SurveyFormDTO>(model);
            //surveyForm.CreatedBy = _userInformation.GetUserName();
            await _unitOfWork.SurveyFormRepository.CreateAsync(surveyForm);
            _unitOfWork.SaveChanges();
            //var selectedQuestionList = model.SelectQuestions.Where(s => s.Checked);
            foreach (var item in model.SurveyQuestions)
            {
                SurveyQuestionDTO selectedQuestion = new SurveyQuestionDTO();
                selectedQuestion.SurveyFormId = surveyForm.Id;
                selectedQuestion.QuestionId = item.QuestionID;
                selectedQuestion.QuestionGroupId = item.QuestionGroupID;
                selectedQuestion.Priority = item.Priority;
                _unitOfWork.SurveyQuestionRepository.Create(selectedQuestion);
            }
            _unitOfWork.SaveChanges();
        }
        public async Task UpdateAsync(SurveyFormViewModel model)
        {
            SurveyFormDTO surveyForm = await _unitOfWork.SurveyFormRepository.GetByIdAsync(model.Id);
            if (surveyForm == null)
                return;
            surveyForm.MallId = model.MallId;
            surveyForm.OfficeId = model.OfficeId;
            surveyForm.IsPeriodic = model.IsPeriodic;
            surveyForm.NameEN = model.NameEN;
            surveyForm.NameVN = model.NameVN;
            surveyForm.TitleEN = model.TitleEN;
            surveyForm.TitleVN = model.TitleVN;
            surveyForm.DescriptionEN = model.DescriptionEN;
            surveyForm.DescriptionVN = model.DescriptionVN;
            surveyForm.IsActive = model.IsActive;
            surveyForm.StartDate = model.StartDate;
            surveyForm.EndDate = model.EndDate;
            surveyForm.ModifiedOn = DateTime.Now;
            _unitOfWork.SaveChanges();
            //surveyForm.ModifiedBy = _userInformation.GetUserName();
            _unitOfWork.SurveyFormRepository.RemoveSelectQuestionBySurveyFormID(surveyForm.Id);
            //var selectedQuestionList = model.SelectQuestions.Where(s => s.Checked);
            foreach (var item in model.SurveyQuestions)
            {
                SurveyQuestionDTO selectedQuestion = new SurveyQuestionDTO();
                selectedQuestion.SurveyFormId = surveyForm.Id;
                selectedQuestion.QuestionId = item.QuestionID;
                selectedQuestion.QuestionGroupId = item.QuestionGroupID;
                selectedQuestion.Priority = item.Priority;
                _unitOfWork.SurveyQuestionRepository.Create(selectedQuestion);
            }
            _unitOfWork.SaveChanges();
        }
        public async Task<bool> SoftDeleteAsync(int ID)
        {
            var surveyForm = await _unitOfWork.SurveyFormRepository.GetByIdAsync(ID);
            if (surveyForm != null)
            {
                surveyForm.IsDeleted = true;
                surveyForm.ModifiedOn = DateTime.Now;
                //surveyForm.ModifiedBy = _userInformation.GetUserName();
                _unitOfWork.SaveChanges();
            }

            return true;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SurveyFormViewModel>> GetAllAsync(int applyTo, int mallId, int officeId)
        {
            IEnumerable<SurveyFormDTO> data = await _unitOfWork.SurveyFormRepository.GetAllAsync(s =>
            s.IsDeleted == false &&
            (mallId == -1 ? true : s.MallId == mallId) &&
            (officeId == -1 ? true : s.OfficeId == officeId));
            return _mapper.Map<IEnumerable<SurveyFormViewModel>>(data);
        }

        public Task RestoreAsync(int id)
        {
            throw new NotImplementedException();
        }
        private async Task<List<QuestionGroupUIViewModel>> QuestionGroupUI(string language, SurveyFormViewModel surveyFormViewModel)
        {
            List<QuestionGroupUIViewModel> questionGroupUIs = new List<QuestionGroupUIViewModel>();

            IEnumerable<QuestionGroupViewModel> groupUIViewModels = _questionGroupHelper.GetEagerAllElements();
            groupUIViewModels = groupUIViewModels.OrderBy(g => g.Priority);
            foreach (var questionGroup in groupUIViewModels)
            {
                var tasks = new List<Task>();
                bool checkQuestionGroup = surveyFormViewModel.SurveyQuestions.Any(s => s.QuestionGroupID == questionGroup.Id);
                if (checkQuestionGroup)
                {
                    QuestionGroupUIViewModel tempQuestionGroupUI = new QuestionGroupUIViewModel();
                    tempQuestionGroupUI.QuestionGroupID = questionGroup.Id;

                    tempQuestionGroupUI.QuestionGroupName = string.Equals(language, ELanguages.VN.ToString()) ? questionGroup.NameVN : questionGroup.NameEN;

                    List<SelectedQuestionViewModel> selectedQuestions = surveyFormViewModel.SurveyQuestions.Where(s => s.QuestionGroupID == questionGroup.Id).OrderBy(s => s.Priority).ToList();
                    selectedQuestions.ForEach(async s =>
                    {
                        QuestionViewModel question = await _questionHelper.GetByIdAsync(s.QuestionID);
                        QuestionUIViewModel tempQuestionUI = new QuestionUIViewModel();
                        tempQuestionUI.QuestionID = question.Id;
                        tempQuestionUI.QuestionTypeID = question.QuestionTypeId;
                        tempQuestionUI.SelectQuestionID = s.ID;
                        tempQuestionUI.QuestionName = string.Equals(language, ELanguages.VN.ToString()) ? question.NameVN : question.NameEN;

                        if (question.QuestionTypeId == (int)EQuestionType.Option || question.QuestionTypeId == (int)EQuestionType.OptionOpen)
                        {
                            foreach (var optionAnswer in question.PredefinedAnswers)
                            {
                                AnswerUIViewModel answerViewModel = new AnswerUIViewModel();
                                answerViewModel.ID = optionAnswer.Id;
                                answerViewModel.Name = string.Equals(language, ELanguages.VN.ToString()) ? optionAnswer.NameVN : optionAnswer.NameEN;
                                answerViewModel.Point = optionAnswer.Point;
                                tempQuestionUI.Answers.Add(answerViewModel);
                            }
                        }

                        tempQuestionGroupUI.QuestionUIs.Add(tempQuestionUI);
                    });

                    questionGroupUIs.Add(tempQuestionGroupUI);
                }
            }

            return questionGroupUIs;
        }
    }
}
