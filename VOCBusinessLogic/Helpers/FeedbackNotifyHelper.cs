//using AutoMapper;
//using CRM.BusinessLogic.IHelpers.SystemIHelpers;
//using CRM.BusinessLogic.IHelpers.VOCIHelpers;
//using CRM.Common;
//using CRM.Common.Services;
//using CRM.Common.Services.EMailServices;
//using CRM.Common.Services.FileStorageServices;
//using CRM.Common.Services.JwtServices;
//using CRM.DataAccess.DBContexts.VOCDBContexts;
//using Microsoft.AspNetCore.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CRM.BusinessLogic.Helpers.VOCHelpers
//{
//    public class FeedbackNotifyHelper : IFeedbackNotifyHelper
//    {
//        private readonly IVOCUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;
//        private readonly IFileStorageService _fileStorageService;
//        private readonly IMailService _mailService;
//        private readonly AppConfig _appConfig;
//        private readonly IJwtService _jwtService;
//        private readonly IWebTemplateHelper _webTemplateHelper;
//        private readonly ISettingHelper _settingHelper;
//        private readonly IWebHostEnvironment _hostingEnvironment;
//        public FeedbackNotifyHelper(IVOCUnitOfWork unitOfWork, IMapper mapper,
//            IMailService mailService, IFileStorageService fileStorageService,
//            IWebTemplateHelper webTemplateHelper, IWebHostEnvironment hostingEnvironment,
//            IJwtService jwtService, AppConfig appConfig, ISettingHelper settingHelper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//            _fileStorageService = fileStorageService;
//            _mailService = mailService;
//            _appConfig = appConfig;
//            _jwtService = jwtService;
//            _webTemplateHelper = webTemplateHelper;
//            _settingHelper = settingHelper;
//            _hostingEnvironment = hostingEnvironment;
//        }

//        public async Task<int> GetNumberOfNewFeedbackOverdueAsync()
//        {
//            try
//            {
//                var data = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s => s.Status == (int)EStatus.New && s.ModifiedOn.AddMinutes(_appConfig.NewFeedbackOverdue) < DateTime.Now);
//                if (data.Count() > 0)
//                {
//                    foreach (var item in data)
//                    {
//                        item.IsOverdue = true;
//                    }
//                    await _unitOfWork.SaveChangesAsync();
//                }
//                return data.Count();
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }



//        public async Task<int> GetNumberOfForwardingFeedbackOverdueAsync()
//        {
//            try
//            {
//                var data = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s => s.Status == (int)EStatus.New && s.IsForwarded && s.ModifiedOn.AddMinutes(_appConfig.ForwardingFeedbackOverdue) < DateTime.Now);
//                if (data.Count() > 0)
//                {
//                    foreach (var item in data)
//                    {
//                        item.IsOverdue = true;
//                    }
//                    await _unitOfWork.SaveChangesAsync();
//                }
//                return data.Count();
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task NotifyEmail()
//        {
//            try
//            {
//                var data = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s => s.IsForwarded && !s.IsReceived && s.ModifiedOn.AddMinutes(_appConfig.ForwardingFeedbackUnread) < DateTime.Now || s.Status == (int)EStatus.New && s.IsForwarded && s.IsReceived && s.ModifiedOn.AddMinutes(_appConfig.ForwardingFeedbackOverdue) < DateTime.Now);
//                // create instance of email service
//                // Create an instance of UrlHelper
//                foreach (var item in data)
//                {
//                    if (item.Department == null)
//                    {
//                        continue;
//                    }
//                    // lay email phong ban
//                    string url = _appConfig.BaseUrl + "Admin/Feedback/DepartmentHandle?code=" + item.Code;
//                    var department = await _unitOfWork.DepartmentRepository.GetByIdAsync((int)item.DepartmentId);
//                    if (department == null || string.IsNullOrEmpty(department.Emails))
//                    {
//                        continue;
//                    }
//                    MultipleEmailModel multipleEmailViewModel = new MultipleEmailModel();
//                    multipleEmailViewModel.To = department.Emails.Split(';').ToList();
//                    multipleEmailViewModel.Subject = "subject";//Globle.GetWebMessageValueByKey(EWebMassageKey.ForwardFeedbackOverdueSubject, ELanguage.VN.ToString());
//                    string body = "body";//Globle.GetWebMessageValueByKey(EWebMassageKey.ForwardFeedbackBody, ELanguage.VN.ToString());
//                    multipleEmailViewModel.Body = body.Replace("#Url#", url);
//                    // thuc hien gui email
//                    bool result = await _mailService.SendMultipleEmailAsync(multipleEmailViewModel, new CancellationToken());
//                    //if (result)
//                    //{
//                    //    item.IsReceived = true;
//                    //    _unitOfWork.SaveChanges();
//                    //}
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//    }
//}
