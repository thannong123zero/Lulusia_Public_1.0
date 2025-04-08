using AutoMapper;
using Common;
using Common.ViewModels.VOCClientViewModels;
using Common.ViewModels.VOCViewModelModels;
using VOCBusinessLogic.IHelpers;
using VOCDataAccess;
using VOCDataAccess.DTOs;

namespace VOCBusinessLogic.Helpers
{
    public class FeedbackTypeHelper : IFeedbackTypeHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FeedbackTypeHelper(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FeedbackTypeViewModel>> GetAllAsync()
        {
            IEnumerable<FeedbackTypeDTO> data = await _unitOfWork.FeedbackTypeRepository.
                GetAllAsync(filter: s => !s.IsDeleted, orderBy: p => p.OrderBy(s => s.Priority).ThenByDescending(s => s.ModifiedOn));
            return _mapper.Map<IEnumerable<FeedbackTypeViewModel>>(data);
        }

        public async Task<IEnumerable<FeedbackTypeViewModel>> GetAllAsync(int typeId)
        {
            IEnumerable<FeedbackTypeDTO> data = await _unitOfWork.FeedbackTypeRepository
                .GetAllAsync(filter: s => !s.IsDeleted && (typeId == -1 ? true : s.ApplyTo == typeId),
                orderBy: p => p.OrderBy(s => s.Priority));

            return _mapper.Map<IEnumerable<FeedbackTypeViewModel>>(data);
        }

        public async Task<IEnumerable<FeedbackTypeViewModel>> GetAllActiveAsync(int typeId)
        {
            IEnumerable<FeedbackTypeDTO> data = await _unitOfWork.FeedbackTypeRepository
                .GetAllAsync(filter: s => s.IsActive && !s.IsDeleted && (typeId == -1 ? true : s.ApplyTo == typeId),
                orderBy: p => p.OrderBy(s => s.Priority));

            return _mapper.Map<IEnumerable<FeedbackTypeViewModel>>(data);
        }

        public async Task<FeedbackTypeViewModel> GetByIdAsync(int ID)
        {
            var data = await _unitOfWork.FeedbackTypeRepository.GetByIdAsync(ID);
            return _mapper.Map<FeedbackTypeViewModel>(data);
        }

        public async Task CreateAsync(FeedbackTypeViewModel model)
        {
            FeedbackTypeDTO feedback = _mapper.Map<FeedbackTypeDTO>(model);
            await _unitOfWork.FeedbackTypeRepository.CreateAsync(feedback);
            _unitOfWork.SaveChanges();
        }

        public async Task UpdateAsync(FeedbackTypeViewModel model)
        {
            FeedbackTypeDTO feedbackType = await _unitOfWork.FeedbackTypeRepository.GetByIdAsync(model.Id);
            if (feedbackType == null)
            {
                return;
            }

            feedbackType.NameEN = model.NameEN;
            feedbackType.NameVN = model.NameVN;
            feedbackType.Priority = model.Priority;
            feedbackType.Description = model.Description;
            feedbackType.ModifiedOn = DateTime.Now;
            feedbackType.IsActive = model.IsActive;
            feedbackType.IsDeleted = model.IsDeleted;
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> SoftDeleteAsync(int ID)
        {
            var feedbackType = await _unitOfWork.FeedbackTypeRepository.GetByIdAsync(ID);
            if (feedbackType != null)
            {
                feedbackType.IsDeleted = true;
                feedbackType.ModifiedOn = DateTime.Now;
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

        public async Task<IEnumerable<FeedbackTypeClientViewModel>> GetAllAsync(string language, int typeId)
        {
            List<FeedbackTypeClientViewModel> models = new List<FeedbackTypeClientViewModel>();
            IEnumerable<FeedbackTypeDTO> data = await _unitOfWork.FeedbackTypeRepository
                .GetAllAsync(filter: s => !s.IsDeleted && s.IsActive && s.ApplyTo == typeId,
                orderBy: p => p.OrderBy(s => s.Priority));
            foreach (var item in data)
            {

                FeedbackTypeClientViewModel model = new FeedbackTypeClientViewModel
                {
                    Id = item.Id,
                    Name = string.Equals(language, ELanguages.EN.ToString()) ? item.NameEN : item.NameVN
                };
                models.Add(model);
            }
            return models;
        }
    }
}
