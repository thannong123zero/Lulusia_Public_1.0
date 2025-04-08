using AutoMapper;
using Common;
using Common.Models;
using Common.ViewModels.SurveyViewModels;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using SurveyBusinessLogic.IHelpers;
using SurveyDataAccess;
using SurveyDataAccess.DTOs;
namespace SurveyBusinessLogic.Helpers
{
    public class ParticipantHelper : IParticipantHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IQuestionGroupHelper _questionGroupHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ParticipantHelper(IUnitOfWork unitOfWork, IMapper mapper, IQuestionGroupHelper questionGroupHelper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _questionGroupHelper = questionGroupHelper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Pagination<ParticipantViewModel>> GetAllAsync(DateTime? startDate, DateTime? endDate, int surveyFormId, int pageIndex)
        {
            Pagination<ParticipantViewModel> model = new Pagination<ParticipantViewModel>();

            IEnumerable<ParticipantDTO> data = await _unitOfWork.ParticipantRepository.GetAllAsync(filter: s =>
                s.SurveyFormId == surveyFormId &&
                (startDate == null ? true : s.CreatedOn.Date >= startDate.Value.Date) &&
                (endDate == null ? true : s.CreatedOn.Date <= endDate.Value.Date) &&
                !s.IsDeleted,
                orderBy: p => p.OrderByDescending(s => s.CreatedOn));
            model.TotalItems = data.Count();
            model.CurrentPage = pageIndex;
            model.TotalPages = (int)Math.Ceiling(model.TotalItems / (double)model.PageSize);
            data = data.Skip((pageIndex - 1) * model.PageSize).Take(model.PageSize);

            IEnumerable<ParticipantViewModel> customerSurveys = _mapper.Map<IEnumerable<ParticipantViewModel>>(data);
            model.Items = customerSurveys;
            return model;
        }
        public async Task CreateAsync(SurveyUIViewModel model)
        {
            ParticipantDTO customerSurvey = new ParticipantDTO();
            customerSurvey.SurveyFormId = model.SurveyFormID;
            if (!string.IsNullOrEmpty(model.PhoneNumber))
            {
                customerSurvey.PhoneNumber = model.PhoneNumber;
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                customerSurvey.Email = model.Email;
            }
            customerSurvey.FullName = model.FullName;
            customerSurvey.CreatedOn = DateTime.Now;
            customerSurvey.ModifiedOn = DateTime.Now;
            await _unitOfWork.ParticipantRepository.CreateAsync(customerSurvey);
            _unitOfWork.SaveChanges();
            foreach (var item in model.QuestionGroupUIs)
            {
                item.QuestionUIs.ForEach(async s =>
                {
                    AnswerDTO surveyResult = new AnswerDTO();
                    surveyResult.ParticipantId = customerSurvey.Id;
                    surveyResult.QuestionGroupId = item.QuestionGroupID;
                    surveyResult.QuestionId = s.QuestionID;
                    surveyResult.QuestionTypeId = s.QuestionTypeID;
                    if (s.QuestionTypeID == (int)EQuestionType.Option)
                    {
                        surveyResult.AnswerId = s.AnswerID;
                        surveyResult.Point = s.Point;
                    }
                    else if (s.QuestionTypeID == (int)EQuestionType.OptionOpen)
                    {
                        surveyResult.AnswerId = s.AnswerID;
                        surveyResult.Answer = s.AnswerOfCustomer;
                    }
                    else if (s.QuestionTypeID == (int)EQuestionType.Rating)
                    {
                        surveyResult.Rating = s.Rating;
                    }
                    else if (s.QuestionTypeID == (int)EQuestionType.Open)
                    {
                        surveyResult.Answer = s.AnswerOfCustomer;
                    }
                    await _unitOfWork.AnswerRepository.CreateAsync(surveyResult);
                });
            }
            _unitOfWork.SaveChanges();
        }
        public SurveyUIViewModel GetEagerCustomerSurveyByID(int ID)
        {
            ParticipantDTO data = _unitOfWork.ParticipantRepository.GetEagerParticipantById(ID);
            //if (!string.IsNullOrEmpty(data.PhoneNumber))
            //{
            //    data.PhoneNumber = Global.Decrypt(data.PhoneNumber);
            //}
            //if (!string.IsNullOrEmpty(data.Email))
            //{
            //    data.Email = Global.Decrypt(data.Email);
            //}
            SurveyUIViewModel surveyUIViewModel = new SurveyUIViewModel();
            surveyUIViewModel.ID = data.Id;
            surveyUIViewModel.PhoneNumber = data.PhoneNumber;
            surveyUIViewModel.Email = data.Email;
            surveyUIViewModel.FullName = data.FullName;
            List<QuestionGroupUIViewModel> questionGroupUIs = QuestionGroupUI(ELanguages.VN.ToString(), data.Answers);
            surveyUIViewModel.QuestionGroupUIs = questionGroupUIs;
            return surveyUIViewModel;
        }
        private List<QuestionGroupUIViewModel> QuestionGroupUI(string language, ICollection<AnswerDTO> surveyResults)
        {
            List<QuestionGroupUIViewModel> questionGroupUIs = new List<QuestionGroupUIViewModel>();

            //Buoc 2: Nhom cau hoi theo nhom
            var groupedData = surveyResults.GroupBy(x => x.QuestionGroupId);
            //Buoc 3: Duyet danh sach nhom cau hoi
            foreach (var group in groupedData)
            {
                QuestionGroupUIViewModel tempQuestionGroupUI = new QuestionGroupUIViewModel();
                //Buoc 4: lay thong tin nhom cau hoi
                QuestionGroupViewModel questionGroup = _questionGroupHelper.GetEagerQuestionGroupByID(group.Key);
                if (questionGroup != null)
                {
                    tempQuestionGroupUI.QuestionGroupID = questionGroup.Id;
                    if (string.Equals(language, ELanguages.VN.ToString()))
                    {
                        tempQuestionGroupUI.QuestionGroupName = questionGroup.NameVN;
                    }
                    else
                    {
                        tempQuestionGroupUI.QuestionGroupName = questionGroup.NameEN;
                    }
                    //Buoc 5: Thuc hien duyet danh cau hoi trong nhom cau hoi
                    foreach (var item in group)
                    {
                        //Buoc 6: Lay thong tin cau hoi
                        QuestionUIViewModel tempQuestionUI = new QuestionUIViewModel();
                        QuestionViewModel question = questionGroup.Questions.Where(s => s.Id == item.QuestionId).FirstOrDefault();
                        if (question != null)
                        {
                            tempQuestionUI.QuestionID = question.Id;
                            tempQuestionUI.QuestionTypeID = question.QuestionTypeId;
                            tempQuestionUI.SelectQuestionID = item.Id;
                            if (string.Equals(language, ELanguages.VN.ToString()))
                            {
                                tempQuestionUI.QuestionName = question.NameVN;
                            }
                            else
                            {
                                tempQuestionUI.QuestionName = question.NameEN;
                            }
                            // neu cau hoi nay thuoc vao loai cau hoi lua chon
                            if (question.QuestionTypeId == (int)EQuestionType.Option)
                            {
                                foreach (var optionAnswer in question.PredefinedAnswers)
                                {
                                    AnswerUIViewModel answerViewModel = new AnswerUIViewModel();
                                    answerViewModel.ID = optionAnswer.Id;
                                    if (string.Equals(language, ELanguages.VN.ToString()))
                                    {
                                        answerViewModel.Name = optionAnswer.NameVN;
                                    }
                                    else
                                    {
                                        answerViewModel.Name = optionAnswer.NameEN;
                                    }
                                    if (optionAnswer.Id == item.AnswerId)
                                    {
                                        answerViewModel.Checked = true;
                                    }
                                    tempQuestionUI.Answers.Add(answerViewModel);
                                }
                            }
                            else if (question.QuestionTypeId == (int)EQuestionType.OptionOpen)
                            {
                                foreach (var optionAnswer in question.PredefinedAnswers)
                                {
                                    AnswerUIViewModel answerViewModel = new AnswerUIViewModel();
                                    answerViewModel.ID = optionAnswer.Id;
                                    if (string.Equals(language, ELanguages.VN.ToString()))
                                    {
                                        answerViewModel.Name = optionAnswer.NameVN;
                                    }
                                    else
                                    {
                                        answerViewModel.Name = optionAnswer.NameEN;
                                    }
                                    if (optionAnswer.Id == item.AnswerId)
                                    {
                                        answerViewModel.Checked = true;
                                    }
                                    tempQuestionUI.AnswerOfCustomer = item.Answer;
                                    tempQuestionUI.Answers.Add(answerViewModel);
                                }
                                tempQuestionUI.AnswerOfCustomer = item.Answer;
                            }
                            else if (question.QuestionTypeId == (int)EQuestionType.Rating)
                            {
                                tempQuestionUI.Rating = item.Rating;
                            }
                            else if (question.QuestionTypeId == (int)EQuestionType.Open)
                            {
                                tempQuestionUI.AnswerOfCustomer = item.Answer;
                            }
                            tempQuestionGroupUI.QuestionUIs.Add(tempQuestionUI);
                        }
                    }
                    questionGroupUIs.Add(tempQuestionGroupUI);
                }
            }
            return questionGroupUIs;
        }

        public async Task<string> ExportExcel(DateTime? startDate, DateTime? endDate, int surveyFormId)
        {
            try
            {
                IEnumerable<ParticipantDTO> participants = await _unitOfWork.ParticipantRepository.GetAllAsync(filter: s =>
                    s.SurveyFormId == surveyFormId &&
                    (startDate == null ? true : s.CreatedOn.Date >= startDate.Value.Date) &&
                    (endDate == null ? true : s.CreatedOn.Date <= endDate.Value.Date) &&
                    !s.IsDeleted,
                    orderBy: p => p.OrderByDescending(s => s.CreatedOn));

                var formSurvey = _unitOfWork.SurveyFormRepository.GetEagerSurveyFormByID(surveyFormId);
                List<QuestionDTO> questions = new List<QuestionDTO>();
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("SurveyReport"); var stream = new MemoryStream();

                    worksheet.Cells[1, 1].Value = "STT";
                    worksheet.Cells[1, 2].Value = "Ngày Tạo";
                    worksheet.Cells[1, 3].Value = "Họ Và Tên";
                    worksheet.Cells[1, 4].Value = "Email";
                    worksheet.Cells[1, 5].Value = "SĐT";
                    int temp = 1;
                    foreach (var item in formSurvey.SurveyQuestions)
                    {
                        QuestionDTO question = _unitOfWork.QuestionRepository.GetEagerQuestionById(item.QuestionId);
                        worksheet.Cells[1, temp + 5].Value = question.NameVN;
                        questions.Add(question);
                        temp++;
                    }

                    int index = 1;
                    foreach (var item in participants)
                    {
                        var data = _unitOfWork.ParticipantRepository.GetEagerParticipantById(item.Id);


                        //List<QuestionGroupUIViewModel> questionGroupUIs = QuestionGroupUI(language, data.SurveyResults);

                        worksheet.Cells[index + 1, 1].Value = index;
                        worksheet.Cells[index + 1, 2].Value = data.CreatedOn.ToString("dd/MM/yyyy HH:mm");
                        worksheet.Cells[index + 1, 3].Value = data.FullName;
                        worksheet.Cells[index + 1, 4].Value = data.Email;
                        worksheet.Cells[index + 1, 5].Value = data.PhoneNumber;
                        int tempIndex = 1;
                        foreach (var selectedQuestion in formSurvey.SurveyQuestions)
                        {
                            var result = data.Answers.Where(s => s.QuestionId == selectedQuestion.QuestionId).First();
                            var question = questions.Where(s => s.Id == result.QuestionId).First();
                            if (question.QuestionTypeId == (int)EQuestionType.Option)
                            {
                                worksheet.Cells[index + 1, tempIndex + 5].Value = question.PredefinedAnswers.Where(s => s.Id == result.AnswerId).First().Point;
                            }
                            else if (question.QuestionTypeId == (int)EQuestionType.OptionOpen)
                            {
                                worksheet.Cells[index + 1, tempIndex + 5].Value = question.PredefinedAnswers.Where(s => s.Id == result.AnswerId).First().Point + " || " + result.Answer;
                            }
                            else if (question.QuestionTypeId == (int)EQuestionType.Open)
                            {
                                worksheet.Cells[index + 1, tempIndex + 5].Value = result.Answer;

                            }
                            else if (question.QuestionTypeId == (int)EQuestionType.Rating)
                            {
                                worksheet.Cells[index + 1, tempIndex + 5].Value = result.Rating;
                            }
                            tempIndex++;

                        }

                        index++;
                        // Add values for the remaining columns...
                    }

                    package.SaveAs(stream);

                    var content = stream.ToArray();
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, EFolderNames.ReportFiles.ToString());

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //How to delete all files in a folder?
                    DirectoryInfo directory = new DirectoryInfo(path);
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        file.Delete();
                    }
                    // create file name
                    string fileName = "SurveyReport" + ".xlsx";
                    path = Path.Combine(path, fileName);
                    //save file
                    File.WriteAllBytes(path, content);

                    return path;

                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
