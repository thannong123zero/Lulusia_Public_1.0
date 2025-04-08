using AutoMapper;
using Common;
using Common.Models;
using Common.Services.EMailServices;
using Common.Services.FileStorageServices;
using Common.ViewModels.VOCViewModelModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using VOCBusinessLogic.IHelpers;
using VOCDataAccess;
using VOCDataAccess.DTOs;

namespace VOCBusinessLogic.Helpers
{
    public class FeedbackHelper : IFeedbackHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMailService _mailService;
        //private readonly AppConfig _appConfig;
        //private readonly IJwtService _jwtService;
        //private readonly IWebTemplateHelper _webTemplateHelper;
        //private readonly ISettingHelper _settingHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly IMasterUnitOfWork _masterUnitOfWork;
        public FeedbackHelper(IUnitOfWork unitOfWork, IMapper mapper,
            IMailService mailService, IFileStorageService fileStorageService,
            //IWebTemplateHelper webTemplateHelper,
            IWebHostEnvironment webHostEnvironment
            //IJwtService jwtService, AppConfig appConfig, ISettingHelper settingHelper,
            //IMasterUnitOfWork masterUnitOfWork
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _mailService = mailService;
            _webHostEnvironment = webHostEnvironment;
            //_appConfig = appConfig;
            //_jwtService = jwtService;
            //_webTemplateHelper = webTemplateHelper;
            //_settingHelper = settingHelper;
            //_masterUnitOfWork = masterUnitOfWork;
        }

        public async Task<IEnumerable<FeedbackViewModel>> GetAllAsync()
        {
            IEnumerable<FeedbackDTO> data = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s => !s.IsDeleted, orderBy: p => p.OrderByDescending(s => s.ModifiedOn));
            return _mapper.Map<IEnumerable<FeedbackViewModel>>(data);
        }
        public async Task<Pagination<FeedbackViewModel>> GetAllAsync(int? applyTo, int? mallId, int? officeId, int? feedbackTypeID, int? statusID, int? priorityId, DateTime startTime, DateTime finishTime, int pageIndex, string? phoneNumber)
        {
            finishTime = finishTime.AddDays(1);
            Pagination<FeedbackViewModel> model = new Pagination<FeedbackViewModel>();
            IEnumerable<FeedbackDTO> data = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s =>
                s.CreatedOn.Date >= startTime.Date &&
                s.CreatedOn.Date < finishTime.Date &&
                (applyTo == -1 ? true : s.ApplyTo == applyTo) &&
                (mallId == -1 ? true : s.MallId == mallId) &&
                (officeId == -1 ? true : s.OfficeId == officeId) &&
                (feedbackTypeID == -1 ? true : s.FeedbackTypeId == feedbackTypeID) &&
                (statusID == -1 ? true : s.StatusId == statusID) &&
                (priorityId == -1 ? true : s.PriorityId == priorityId) &&
                (string.IsNullOrEmpty(phoneNumber) ? true : s.PhoneNumber == null ? false : s.PhoneNumber.Contains(phoneNumber)) &&
                !s.IsDeleted,
                orderBy: p => p.OrderByDescending(s => s.IsOverdue).ThenByDescending(s => s.CreatedOn));
            model.TotalItems = data.Count();
            model.CurrentPage = pageIndex;
            model.TotalPages = (int)Math.Ceiling(model.TotalItems / (double)model.PageSize);

            data = data.Skip((pageIndex - 1) * model.PageSize).Take(model.PageSize);

            IEnumerable<FeedbackViewModel> feedbackViewModels = _mapper.Map<IEnumerable<FeedbackViewModel>>(data);
            model.Items = feedbackViewModels;
            return model;
        }

        public async Task<FeedbackViewModel> GetByIdAsync(int Id)
        {
            var data = await _unitOfWork.FeedbackRepository.GetByIdAsync(Id);
            FeedbackViewModel model = _mapper.Map<FeedbackViewModel>(data);
            if (!string.IsNullOrEmpty(data.Images))
            {
                model.ImageUrls = data.Images.Split(';').ToList();
            }
            if (model.IsForwarded)
            {
                var forwardFeedbacks = await _unitOfWork.ForwardFeedbackRepository.GetAllAsync(filter: s => s.FeedbackId == model.Id);
                model.ForwardFeedbacks = _mapper.Map<IEnumerable<ForwardFeedbackViewModel>>(forwardFeedbacks);
            }
            return model;
        }
        public async Task<FeedbackViewModel> GetByCodeAsync(string code, int forwardFeedbackId, string language)
        {
            var data = await _unitOfWork.FeedbackRepository.GetByCodeAsync(code);
            var forwardFeedback = await _unitOfWork.ForwardFeedbackRepository.GetByIdAsync(forwardFeedbackId);
            if (forwardFeedback.FeedbackId != data.Id)
            {
                return null;
            }
            if (data == null || data.StatusId == (int)EStatus.Completed || data.StatusId == (int)EStatus.Closed || forwardFeedback == null)
            {
                return null;
            }
            ForwardFeedbackViewModel forwardFeedbackViewModel = _mapper.Map<ForwardFeedbackViewModel>(forwardFeedback);
            FeedbackViewModel model = _mapper.Map<FeedbackViewModel>(data);
            //if (language == ELanguages.VN.ToString())
            //{
            //    model.MallName = _masterUnitOfWork.MallRepository.GetById(model.MallId)?.NameVN;
            //    model.OfficeName = _masterUnitOfWork.OfficeRepository.GetById(model.OfficeId)?.NameVN;
            //}
            //else
            //{
            //    model.MallName = _masterUnitOfWork.MallRepository.GetById(model.MallId)?.NameEN;
            //    model.OfficeName = _masterUnitOfWork.OfficeRepository.GetById(model.OfficeId)?.NameEN;
            //}
            model.ForwardFeedbacks = new List<ForwardFeedbackViewModel> { forwardFeedbackViewModel };
            if (!string.IsNullOrEmpty(data.Images))
            {
                model.ImageUrls = data.Images.Split(';').ToList();
            }
            return model;
        }
        public async Task CreateAsync(FeebackClientViewModel model, string token)
        {
            var status = await _unitOfWork.FeedbackStatusRepository.GetTheFirstAsync();
            if (status == null)
            {
                throw new Exception("Status can't get value!");
            }
            FeedbackDTO feedback = _mapper.Map<FeedbackDTO>(model);
            feedback.StatusId = status.Id;
            feedback.Code = Guid.NewGuid().ToString();
            //feedback.CreatedBy = _jwtService.GetUserNameFromToken(token);
            //for (int i = 0; i < model.ImageFiles.Count(); i++)
            //{
            //    //save file
            //    var fileName = _fileStorageService.SaveImageInSubfolder(EFolderName.FeedbackFiles.ToString(), model.PhoneNumber, model.ImageFiles[i], 100);
            //    if (i == model.ImageFiles.Count() - 1)
            //    {
            //        feedback.Images = string.Concat(feedback.Images, fileName);
            //    }
            //    else
            //    {
            //        feedback.Images = string.Concat(feedback.Images, fileName, ";");
            //    }
            //}
            await _unitOfWork.FeedbackRepository.CreateAsync(feedback);
            _unitOfWork.SaveChanges();
        }
        public async Task CreateAsync(FeebackClientViewModel model)
        {
            var status = await _unitOfWork.FeedbackStatusRepository.GetTheFirstAsync();
            var priority = await _unitOfWork.FeedbackPriorityRepository.GetTheFirstAsync();
            if (status == null || priority == null)
            {
                throw new Exception("Status or priority can't get value!");
            }
            FeedbackDTO feedback = _mapper.Map<FeedbackDTO>(model);
            feedback.StatusId = status.Id;
            feedback.PriorityId = priority.Id;
            feedback.Code = Guid.NewGuid().ToString();
            feedback.CreatedBy = "Web App";
            //for (int i = 0; i < model.ImageFiles.Count(); i++)
            //{
            //    //save file
            //    var fileName = _fileStorageService.SaveImageInSubfolder(EFolderName.FeedbackFiles.ToString(), model.PhoneNumber, model.ImageFiles[i], 100);
            //    if (i == model.ImageFiles.Count() - 1)
            //    {
            //        feedback.Images = string.Concat(feedback.Images, fileName);
            //    }
            //    else
            //    {
            //        feedback.Images = string.Concat(feedback.Images, fileName, ";");
            //    }
            //}
            //foreach (var image in model.ImageFiles)
            //{
            //    //save file
            //    var fileName = _fileStorageService.SaveImageInSubfolder(EFolderName.Feedbacks.ToString(), model.PhoneNumber, image, 100);
            //    feedback.Images = string.Concat(feedback.Images, fileName, ";");
            //}
            await _unitOfWork.FeedbackRepository.CreateAsync(feedback);
            _unitOfWork.SaveChanges();
        }
        public async Task UpdateAsync(FeedbackViewModel model, string token)
        {
            FeedbackDTO feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(model.Id);
            if (feedback == null)
            {
                return;
            }
            feedback.StatusId = model.StatusId;
            feedback.Solution = model.Solution;
            feedback.PriorityId = model.PriorityId;
            feedback.ModifiedOn = DateTime.Now;
            //feedback.ModifiedBy = _jwtService.GetUserNameFromToken(token);
            feedback.IsOverdue = false;
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> ForwardFeedbackAsync(ForwardFeedbackViewModel model)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(model.FeedbackId);
                    var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(model.DepartmentId);
                    // check feedback and department
                    if (feedback == null || department == null || string.IsNullOrEmpty(department.Emails))
                        return false;
                    // save forward feedback
                    feedback.IsForwarded = true;
                    ForwardFeedbackDTO forwardFeedback = _mapper.Map<ForwardFeedbackDTO>(model);
                    await _unitOfWork.ForwardFeedbackRepository.CreateAsync(forwardFeedback);
                    _unitOfWork.SaveChanges();
                    // send email
                    bool result = await ForwadEmailAsync(feedback, forwardFeedback.Id, department.Emails);
                    if (!result)
                    {
                        _unitOfWork.Rollback();
                        return false;
                    }
                    _unitOfWork.Commit();
                    return true;
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                    return false;
                }
            }
        }
        public async Task ResponseFeedbackByDeparmentAsync(ForwardFeedbackViewModel model)
        {
            var forwardFeedback = await _unitOfWork.ForwardFeedbackRepository.GetByIdAsync(model.Id);
            if (forwardFeedback == null)
                return;
            forwardFeedback.Response = model.Response;
            await _unitOfWork.SaveChangesAsync();
        }
        private async Task<bool> ForwadEmailAsync(FeedbackDTO feedback, int forwardId, string mails)
        {
            //string url = _settingHelper.GetValueByKey(EWebConfig.ForwardFeedbackUrl);
            ////url = string.Concat(url, feedback.Code);
            //url = url.Replace("#code#", feedback.Code);
            //url = url.Replace("#forwardId#", forwardId.ToString());
            //string mailSubject = _webTemplateHelper.GetValueByKey(EWebTemplate.ForwardFeedbackSubject, ELanguages.VN.ToString());
            //string mailBody = _webTemplateHelper.GetValueByKey(EWebTemplate.ForwardFeedbackBody, ELanguages.VN.ToString());

            //if (string.IsNullOrEmpty(feedback.Images))
            //{
            //    // send email without attachment
            //    MultipleEmailModel mail = new MultipleEmailModel();
            //    mail.To = mails.Split(';').ToList();
            //    mail.Subject = mailSubject;
            //    mail.Body = mailBody.Replace("#Url#", url);
            //    return await _mailService.SendMultipleEmailAsync(mail, new CancellationToken());
            //}
            //else
            //{
            //    // send email with attachment
            //    MailDataWithAttachmentsModel mail = new MailDataWithAttachmentsModel();
            //    mail.To = mails.Split(';').ToList();
            //    mail.Subject = mailSubject;
            //    mail.Body = mailBody.Replace("#Url#", url);
            //    List<string> images = feedback.Images.Split(';').ToList();
            //    var formFileCollection = new FormFileCollection();
            //    foreach (var image in images)
            //    {// Create IFormFileCollection and add the file
            //        string path = Path.Combine(_webHostEnvironment.WebRootPath, EFolderName.FeedbackFiles.ToString(), feedback.PhoneNumber, image);
            //        //check file exist
            //        if (File.Exists(path))
            //        {
            //            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            //            {
            //                var memoryStream = new MemoryStream();
            //                stream.CopyTo(memoryStream);
            //                memoryStream.Position = 0;
            //                string fileName = Path.GetFileName(path);

            //                var file = new FormFile(memoryStream, 0, stream.Length, "file", fileName)
            //                {
            //                    Headers = new HeaderDictionary(),
            //                    ContentType = "image/jpeg" // Change content type to image/jpeg
            //                };
            //                formFileCollection.Add(file);
            //            }
            //        }

            //    }
            //    mail.Attachments = formFileCollection;
            //    return await _mailService.SendWithAttachmentsAsync(mail, new CancellationToken());
            //}
            return true;
        }

        //public VOCParameterViewModel GetVOCParameter(string token, int? applyTo, int? mallId, int? officeId)
        //{
        //    VOCParameterViewModel model = new VOCParameterViewModel();
        //    if (applyTo != null) // request from query has value
        //    {
        //        model.ApplyTo = (int)applyTo;
        //        model.MallId = mallId != null? (int)mallId : -1;
        //        model.OfficeId = officeId != null ? (int)officeId : -1;
        //        return model;
        //    }
        //    model.MallId = _jwtService.GetMallIdFromToken(token);
        //    model.OfficeId = _jwtService.GetOfficeIdFromToken(token);
        //    if (model.MallId > -1 && model.OfficeId == -1)
        //        model.ApplyTo = (int)EVOCType.Mall;
        //    else if (model.OfficeId > -1 && model.MallId == -1)
        //        model.ApplyTo = (int)EVOCType.Office;
        //    else if (model.MallId == -1 && model.OfficeId == -1)
        //        model.ApplyTo = -1;

        //    return model;
        //}

        public async Task<FeedbackReportViewModel> GetReportAsync(int? applyTo, int? mallId, int? officeId, int? feedbackTypeID, int? statusID, int? priorityId, DateTime startTime, DateTime finishTime, string? phoneNumber)
        {
            finishTime = finishTime.AddDays(1);
            FeedbackReportViewModel model = new FeedbackReportViewModel();
            IEnumerable<FeedbackDTO> data = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s =>
                s.CreatedOn.Date >= startTime.Date &&
                s.CreatedOn.Date < finishTime.Date &&
                (applyTo == -1 ? true : s.ApplyTo == applyTo) &&
                (mallId == -1 ? true : s.MallId == mallId) &&
                (officeId == -1 ? true : s.OfficeId == officeId) &&
                (feedbackTypeID == -1 ? true : s.FeedbackTypeId == feedbackTypeID) &&
                (statusID == -1 ? true : s.StatusId == statusID) &&
                (priorityId == -1 ? true : s.PriorityId == priorityId) &&
                (string.IsNullOrEmpty(phoneNumber) ? true : s.PhoneNumber == null ? false : s.PhoneNumber.Contains(phoneNumber)) &&
                !s.IsDeleted);
            model.TotalFeedback = data.Count();
            model.TotalNewFeedback = data.Count(s => s.StatusId == (int)EStatus.New);
            model.TotalProcessingFeedback = data.Count(s => s.StatusId == (int)EStatus.Processing);
            model.TotalRespondedFeedback = data.Count(s => s.StatusId == (int)EStatus.Responsed);
            model.TotalCompletedFeedback = data.Count(s => s.StatusId == (int)EStatus.Completed);
            model.TotalClosedFeedback = data.Count(s => s.StatusId == (int)EStatus.Closed);
            model.TotalReopenFeedback = data.Count(s => s.StatusId == (int)EStatus.ReOpen);
            model.TotalForwardedFeedback = data.Count(s => s.IsForwarded == true);
            model.TotalLowFeedback = data.Count(s => s.StatusId == (int)EPriority.Low);
            model.TotalMediumFeedback = data.Count(s => s.StatusId == (int)EPriority.Medium);
            model.TotalUrgentFeedback = data.Count(s => s.StatusId == (int)EPriority.Urgent);

            return model;
        }
    }
}
