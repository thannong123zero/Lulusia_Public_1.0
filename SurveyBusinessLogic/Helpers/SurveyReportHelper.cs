using Common;
using Common.ViewModels.SurveyViewModels;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using SurveyBusinessLogic.IHelpers;
using SurveyDataAccess;
using SurveyDataAccess.DTOs;

namespace SurveyBusinessLogic.Helpers
{
    public class SurveyReportHelper : ISurveyReportHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly UserInformation _userInformation;
        //private readonly IUserLogHelper _userLogHelper;

        public SurveyReportHelper(IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment)
        //UserInformation userInformation,
        //IUserLogHelper userLogHelper)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            //_userLogHelper = userLogHelper;
            //_userInformation = userInformation;
        }

        public async Task<ClusteredBarChartViewModel> CompareAverageScoreByMonth(int surveyId, DateTime startTime, DateTime finishTime)
        {
            ClusteredBarChartViewModel clusteredBarChartViewModel = new ClusteredBarChartViewModel();

            clusteredBarChartViewModel.Title = "So sánh điểm trung bình theo tháng";

            //startTime = new DateTime(startTime.Year, startTime.Month, 1);
            // Lay danh sach group cua bieu do
            //for (DateTime date = startTime; date <= finishTime; date = date.AddMonths(1))
            //{
            //    clusteredBarChartViewModel.Labels.Add(date.ToString("MM-yyyy"));
            //}
            // Lay danh sach cau hoi cua khao sat
            IEnumerable<SurveyQuestionDTO> selectedQuestions = await _unitOfWork.SurveyQuestionRepository.GetBySurveyFormID(surveyId);

            foreach (var item in selectedQuestions)
            {
                QuestionDTO question = _unitOfWork.QuestionRepository.GetEagerQuestionById(item.QuestionId);
                if (question.QuestionTypeId == (int)EQuestionType.Option)
                {
                    clusteredBarChartViewModel.Labels.Add(question.ChartLabel);
                }
            }
            //tao 1 nhom cot
            for (DateTime date = startTime; date <= finishTime; date = date.AddMonths(1))
            {
                GroupColoumnViewModel groupColoumnViewModel = new GroupColoumnViewModel();
                groupColoumnViewModel.Label = date.ToString("MM-yyyy");
                BarChartViewModel temp = await GetAverageScoreByDate(surveyId, date, date.AddMonths(1));
                groupColoumnViewModel.Columns = temp.Columns;
                clusteredBarChartViewModel.Groups.Add(groupColoumnViewModel);
            }

            return clusteredBarChartViewModel;
        }

        public async Task<string> ExportSurveyResult(int surveyId)
        {
            try
            {
                var formSurvey = _unitOfWork.SurveyFormRepository.GetEagerSurveyFormByID(surveyId);
                var customerSurveys = await _unitOfWork.ParticipantRepository.GetAllAsync(filter: s => s.SurveyFormId == surveyId);
                List<QuestionDTO> questions = new List<QuestionDTO>();

                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("SurveyReport");

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
                    // Add headers for the remaining columns...

                    int index = 1;
                    foreach (var customer in customerSurveys)
                    {
                        var data = _unitOfWork.ParticipantRepository.GetEagerParticipantById(customer.Id);


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

                    var stream = new MemoryStream();
                    package.SaveAs(stream);

                    var content = stream.ToArray();
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "exportData");

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
                    string fileName = "Report" + ".xlsx";
                    path = Path.Combine(path, fileName);
                    //save file
                    File.WriteAllBytes(path, content);

                    //thuc hien ghi log
                    //await _userLogHelper.WriteLog(new UserLogDTO
                    //{
                    //    Action = (int)EAction.ExportExcel,
                    //    Controller = (int)EController.SurveyReportController,
                    //    UserName = _userInformation.GetUserName()
                    //});

                    return fileName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BarChartViewModel> GetAverageScoreByDate(int surveyId, DateTime startTime, DateTime finishTime)
        {
            BarChartViewModel barChartViewModel = new BarChartViewModel();
            barChartViewModel.Title = "Điểm trung bình theo ngày";
            barChartViewModel.Unit = "Điểm";
            List<AverageScoreDTO> data = await _unitOfWork.AnswerRepository.GetAverageScoreByDateAsync(surveyId, startTime, finishTime.AddDays(1), (int)EQuestionType.Option);

            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    ColumnViewModel coloumnViewModel = new ColumnViewModel();
                    QuestionDTO question = _unitOfWork.QuestionRepository.GetById(item.QuestionId);
                    coloumnViewModel.Label = question.ChartLabel;
                    coloumnViewModel.Value = item.AverageScore;
                    barChartViewModel.Columns.Add(coloumnViewModel);
                }
            }
            else
            {
                ColumnViewModel coloumnViewModel = new ColumnViewModel();
                coloumnViewModel.Label = "null";
                coloumnViewModel.Value = 0;
                barChartViewModel.Columns.Add(coloumnViewModel);
            }
            //// Lay sanh sach khach hang da tham gia khao sat
            //IEnumerable<CustomerSurveyDTO> customerSurveys = await _unitOfWork.CustomerSurveyRepository.GetAllAsync(s => s.SurveyFormId == surveyId && s.CreatedOn.Date >= startTime.Date && s.CreatedOn.Date < finishTime.Date);
            //// Lay danh sach cau hoi cua khao sat
            ////SurveyFormDTO surveyForm = _unitOfWork.SurveyFormRepository.GetEagerSurveyFormByID(surveyId);
            //IEnumerable<SelectedQuestionDTO> selectedQuestions = await _unitOfWork.SelectQuestionRepository.GetBySurveyFormID(surveyId);
            //// duyet qua tung cau hoi
            //foreach (var item in selectedQuestions)
            //{
            //    // lay thong tin cau hoi
            //    QuestionDTO question = _unitOfWork.QuestionRepository.GetEagerQuestionByID(item.QuestionId);
            //    if (question.QuestionTypeId == (int)EQuestionType.Option)
            //    {
            //        int total = 0;
            //        int count = 0;
            //        // duyet qua tung khach hang tren tung cau hoi
            //        foreach (var customerSurvey in customerSurveys)
            //        {
            //            // lay ket qua khao sat cua khach hang
            //            var surveyResult = _unitOfWork.CustomerSurveyRepository.GetEagerCustomerSurveyByID(customerSurvey.Id);
            //            if (surveyResult != null)
            //            {
            //                var result = surveyResult.SurveyResults.Where(s => s.QuestionId == question.Id).First();
            //                    total += question.Answers.Where(s => s.Id == result.AnswerId).First().Point;
            //                    count++;                           
            //            }
            //        }
            //        // them cot vao bieu do
            //        if (count > 0)
            //        {
            //            ColoumnViewModel coloumnViewModel = new ColoumnViewModel();
            //            coloumnViewModel.Label = question.ChartLabel;
            //            coloumnViewModel.Value = Math.Round(total / (count * 1.0), 2); 
            //            barChartViewModel.Coloumns.Add(coloumnViewModel);
            //        }
            //        else {
            //            ColoumnViewModel coloumnViewModel = new ColoumnViewModel();
            //            coloumnViewModel.Label = question.ChartLabel;
            //            coloumnViewModel.Value = 0;
            //            barChartViewModel.Coloumns.Add(coloumnViewModel);
            //        }
            //    }
            //}
            return barChartViewModel;
        }

        public BarChartViewModel GetNumberOfSurverys(int surveyId, DateTime startTime, DateTime finishTime)
        {
            startTime = startTime.Date;
            finishTime = finishTime.AddDays(1).Date;
            IEnumerable<ParticipantDTO> customerSurveys = _unitOfWork.ParticipantRepository.GetAll(filter: s => s.SurveyFormId == surveyId && s.CreatedOn.Date >= startTime.Date && s.CreatedOn.Date < finishTime.Date);
            BarChartViewModel barChartViewModel = new BarChartViewModel();
            barChartViewModel.Title = "Số lượng khảo sát";
            barChartViewModel.Unit = "Khảo sát";
            List<ColumnViewModel> coloumnViewModels = new List<ColumnViewModel>();
            for (DateTime date = startTime; date < finishTime; date = date.AddDays(1))
            {
                int count = customerSurveys.Where(s => s.CreatedOn.Date == date.Date).Count();
                ColumnViewModel coloumnViewModel = new ColumnViewModel();
                coloumnViewModel.Label = date.ToString("dd-MM-yyyy");
                coloumnViewModel.Value = count;
                coloumnViewModels.Add(coloumnViewModel);
            }
            barChartViewModel.Columns = coloumnViewModels;
            return barChartViewModel;
        }
    }
}
