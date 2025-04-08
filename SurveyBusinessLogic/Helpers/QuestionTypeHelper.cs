using AutoMapper;
using Common.ViewModels.SurveyViewModels;
using SurveyBusinessLogic.IHelpers;
using SurveyDataAccess;

namespace SurveyBusinessLogic.Helpers
{
    public class QuestionTypeHelper : IQuestionTypeHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuestionTypeHelper(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<QuestionTypeViewModel>> GetAllAsync()
        {
            var data = await _unitOfWork.QuestionTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<QuestionTypeViewModel>>(data);
        }
    }
}
