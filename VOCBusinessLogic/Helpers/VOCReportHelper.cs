//using Microsoft.AspNetCore.Hosting;
//using OfficeOpenXml;
//using VOCBusinessLogic.IHelpers;


//namespace VOCBusinessLogic.Helpers
//{
//    public class VOCReportHelper : IVOCReportHelper
//    {
//        private readonly IVOCUnitOfWork _unitOfWork;
//        private readonly IMasterUnitOfWork _masterUnitOfWork;
//        private readonly IWebHostEnvironment _webHostEnvironment;

//        public VOCReportHelper(IVOCUnitOfWork unitOfWork,
//            IWebHostEnvironment webHostEnvironment,
//            IMasterUnitOfWork masterUnitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//            _webHostEnvironment = webHostEnvironment;
//            _masterUnitOfWork = masterUnitOfWork;
//        }

//        public async Task<VOCReportViewModel> VOCReportViewModel()
//        {
//            VOCReportViewModel vOCReportViewModel = new VOCReportViewModel();
//            var data = await _unitOfWork.FeedbackRepository.GetAllAsync();

//            vOCReportViewModel.NumberOfPending = data.Where(x => x.StatusId == (int)EStatus.New).Count();
//            vOCReportViewModel.NumberOfProcessing = data.Where(x => x.StatusId == (int)EStatus.Processing).Count();
//            vOCReportViewModel.NumberOfCompleted = data.Where(x => x.StatusId == (int)EStatus.Completed).Count();
//            vOCReportViewModel.NumberOfRejections = data.Where(x => x.StatusId == (int)EStatus.ReOpen).Count();

//            return vOCReportViewModel;
//        }

//        //public async Task<FeedbackReportViewModel> GetFeedbackReportAsync(DateTime? startTime, DateTime? finishTime, int mallId = -1)
//        //{
//        //    // we need to use a stored procedure to get the report
//        //    FeedbackReportViewModel feedbackReportViewModel = new FeedbackReportViewModel();
//        //    IEnumerable<FeedbackDTO> data = new List<FeedbackDTO>();
//        //    finishTime = finishTime.Value.AddDays(1);
//        //    if (mallId != -1 && startTime.HasValue && finishTime.HasValue)
//        //    {
//        //        data = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s => s.MallId == mallId && s.CreatedOn.Date >= startTime.Value.Date && s.CreatedOn.Date < finishTime.Value.Date);
//        //    }
//        //    else if (mallId == -1 && startTime.HasValue && finishTime.HasValue)
//        //    {
//        //        data = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s => s.CreatedOn.Date >= startTime.Value.Date && s.CreatedOn.Date < finishTime.Value.Date);
//        //    }
//        //    else if (mallId != -1 && (!startTime.HasValue || !finishTime.HasValue))
//        //    {
//        //        data = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s => s.MallId == mallId);
//        //    }
//        //    else
//        //    {
//        //        data = await _unitOfWork.FeedbackRepository.GetAllAsync();
//        //    }
//        //    feedbackReportViewModel.TotalFeedback = data.Count();
//        //    feedbackReportViewModel.TotalNewFeedback = data.Count(s => s.StatusId == (int)EStatus.New);
//        //    feedbackReportViewModel.TotalProcessingFeedback = data.Count(s => s.StatusId == (int)EStatus.Processing);
//        //    feedbackReportViewModel.TotalResponsedFeedback = data.Count(s => s.StatusId == (int)EStatus.Responsed);
//        //    feedbackReportViewModel.TotalCompletedFeedback = data.Count(s => s.StatusId == (int)EStatus.Completed);
//        //    feedbackReportViewModel.TotalClosedFeedback = data.Count(s => s.StatusId == (int)EStatus.Closed);
//        //    feedbackReportViewModel.TotalReopenFeedback = data.Count(s => s.StatusId == (int)EStatus.ReOpen);
//        //    feedbackReportViewModel.TotalForwardedFeedback = data.Count(s => s.IsForwarded == true);
//        //    feedbackReportViewModel.TotalLowFeedback = data.Count(s => s.PriorityId == (int)EPriority.Low);
//        //    feedbackReportViewModel.TotalMediumFeedback = data.Count(s => s.PriorityId == (int)EPriority.Medium);
//        //    feedbackReportViewModel.TotalUrgentFeedback = data.Count(s => s.PriorityId == (int)EPriority.Urgent);

//        //    return feedbackReportViewModel;
//        //}

//        public async Task<BarChartViewModel> GetNumberOfFeedbacksByMonthAsync(DateTime time, int mallId = -1)
//        {
//            try
//            {
//                BarChartViewModel barChart = new BarChartViewModel();
//                barChart.Unit = "Feedback Times";
//                //time 
//                // data
//                var data = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: x => (mallId == -1 ? true : x.MallId == mallId) && x.CreatedOn.Date >= time.Date && x.CreatedOn.Date <= time.AddMonths(1).Date);

//                int daysInMonth = DateTime.DaysInMonth(time.Year, time.Month);
//                for (int i = 1; i <= daysInMonth; i++)
//                {
//                    DateTime dateTime = new DateTime(time.Year, time.Month, i);
//                    int numberOfAccessmentTimes = data.Where(s => s.CreatedOn.Date == dateTime.Date).ToList().Count();

//                    barChart.Columns.Add(new ColumnViewModel { Label = dateTime.ToString("dd-MM-yyyy"), Value = numberOfAccessmentTimes });
//                }
//                return barChart;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<StackBarViewModel> CompareNumberOfFeedbackTypesByMonthAsync(DateTime firstMonth, DateTime secondMonth, int applyTo = 0, int mallId = -1, int officeId = -1)
//        {
//            //time 
//            StackBarViewModel stackBarViewModel = new StackBarViewModel();


//            var feedbackTypes = await _unitOfWork.FeedbackTypeRepository.GetAllAsync(filter: s =>
//                !s.IsDeleted && s.IsActive &&
//                s.ApplyTo == applyTo,
//                orderBy: p => p.OrderByDescending(s => s.Priority));
//            for (DateTime date = firstMonth; date <= secondMonth; date = date.AddMonths(1))
//            {
//                stackBarViewModel.Labels.Add(date.ToString("MM-yyyy"));
//            }

//            foreach (var item in feedbackTypes.ToList())
//            {
//                StackColoumnViewModel stackColoumnViewModel = new StackColoumnViewModel();
//                stackColoumnViewModel.Label = item.NameVN;
//                for (DateTime date = firstMonth; date <= secondMonth; date = date.AddMonths(1))
//                {
//                    var feedbacks = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s =>
//                    s.ApplyTo == applyTo &&
//                    (mallId == -1 ? true : s.MallId == mallId) &&
//                    (officeId == -1 ? true : s.OfficeId == officeId) &&
//                    s.CreatedOn.Date >= date.Date &&
//                    s.CreatedOn.Date < date.AddMonths(1).Date &&
//                    !s.IsDeleted);
//                    int temp = feedbacks.Where(s => s.FeedbackTypeId == item.Id).Count();
//                    stackColoumnViewModel.Columns.Add(new StackViewModel { Label = date.ToString("MM-yyyy"), Value = temp });

//                }
//                stackBarViewModel.Stacks.Add(stackColoumnViewModel);

//            }
//            return stackBarViewModel;
//        }
//        //Test pass
//        public async Task<StackBarViewModel> CompareNumberOfTicketsByMonthAsync(DateTime firstMonth, DateTime secondMonth, int applyTo = 0, int mallId = -1, int officeId = -1)
//        {
//            var statusList = _unitOfWork.FeedbackStatusRepository.GetAll();
//            StackBarViewModel stackBarViewModel = new StackBarViewModel();
//            stackBarViewModel.Labels.Add("Mới");
//            stackBarViewModel.Labels.Add("Đang xử lý");
//            stackBarViewModel.Labels.Add("Đã hoàn thành");
//            stackBarViewModel.Labels.Add("Đã phản hồi");
//            stackBarViewModel.Labels.Add("Mở lại");
//            stackBarViewModel.Labels.Add("Đóng");


//            for (DateTime date = firstMonth; date <= secondMonth; date = date.AddMonths(1))
//            {
//                var feedbacks = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s =>
//                    s.ApplyTo == applyTo &&
//                    (mallId == -1 ? true : s.MallId == mallId) &&
//                    (officeId == -1 ? true : s.OfficeId == officeId) &&
//                    s.CreatedOn.Date >= date.Date &&
//                    s.CreatedOn.Date < date.AddMonths(1).Date);

//                StackColoumnViewModel stack = new StackColoumnViewModel();
//                stack.Label = date.ToString("MM-yyyy");
//                foreach (var status in statusList)
//                {
//                    stack.Columns.Add(new StackViewModel { Label = status.Name, Value = feedbacks.Count(s => s.StatusId == status.Id) });
//                }
//                stackBarViewModel.Stacks.Add(stack);

//            }

//            return stackBarViewModel;
//        }
//        //Test pass
//        public async Task<TreeMapViewModel> GetNumberOfFeedbackTypesByMonthAsync(DateTime time, int applyTo = 0, int mallId = -1, int officeId = -1)
//        {
//            TreeMapViewModel treeMap = new TreeMapViewModel();
//            var feedbackTypes = await _unitOfWork.FeedbackTypeRepository.GetAllAsync(filter: s =>
//            !s.IsDeleted && s.IsActive &&
//            s.ApplyTo == applyTo,
//            orderBy: p => p.OrderByDescending(s => s.Priority));

//            var Feedbacks = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s =>
//                s.ApplyTo == applyTo &&
//                (mallId == -1 ? true : s.MallId == mallId) &&
//                (officeId == -1 ? true : s.OfficeId == officeId) &&
//                s.CreatedOn.Date >= time.Date &&
//                s.CreatedOn.Date < time.AddMonths(1).Date &&
//                !s.IsDeleted);

//            treeMap.Maps = new List<MapViewModel>();
//            feedbackTypes.ToList().ForEach(item =>
//            {
//                treeMap.Maps.Add(new MapViewModel { Name = item.NameVN, Value = Feedbacks.Count(s => s.FeedbackTypeId == item.Id) });
//            });

//            return treeMap;
//        }
//        //Test pass
//        public async Task<BarChartViewModel> GetNumberOfTicketsAsync(DateTime firstTime, DateTime secondTime, int applyTo = 0, int mallId = -1, int officeId = -1)
//        {
//            var statusList = _unitOfWork.FeedbackStatusRepository.GetAll();
//            BarChartViewModel barChart = new BarChartViewModel();

//            barChart.Unit = "Times";
//            var data = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s =>
//                s.ApplyTo == applyTo &&
//                (mallId == -1 ? true : s.MallId == mallId) &&
//                (officeId == -1 ? true : s.OfficeId == officeId) &&
//                s.CreatedOn.Date >= firstTime.Date &&
//                s.CreatedOn.Date < secondTime.AddDays(1).Date);
//            barChart.Columns.Add(new ColumnViewModel { Label = "Tổng", Value = data.Count() });
//            foreach (var status in statusList)
//            {
//                barChart.Columns.Add(new ColumnViewModel { Label = status.Name, Value = data.Count(s => s.StatusId == status.Id) });
//            }
//            return barChart;
//        }

//        public async Task<string> ExportFeedback(DateTime? startTime, DateTime? finishTime, int? feedbackTypeId, int? statusId, int? priorityId, int? applyTo, int? mallId, int? officeId)
//        {
//            try
//            {
//                IEnumerable<FeedbackDTO> feedbacks = await _unitOfWork.FeedbackRepository.GetAllAsync(filter: s =>
//                    (startTime == null ? true : s.CreatedOn.Date >= startTime.Value.Date) &&
//                    (finishTime == null ? true : s.CreatedOn.Date <= finishTime.Value.Date) &&
//                    (applyTo == -1 ? true : s.ApplyTo == applyTo) &&
//                    (mallId == -1 ? true : s.MallId == mallId) &&
//                    (officeId == -1 ? true : s.OfficeId == officeId) &&
//                    (feedbackTypeId == -1 ? true : s.FeedbackTypeId == feedbackTypeId) &&
//                    (statusId == -1 ? true : s.StatusId == statusId) &&
//                    (priorityId == -1 ? true : s.PriorityId == priorityId) &&
//                    !s.IsDeleted,
//                    orderBy: p => p.OrderByDescending(s => s.IsOverdue).ThenByDescending(s => s.CreatedOn));
//                IEnumerable<FeedbackTypeDTO> feedbackTypes = await _unitOfWork.FeedbackTypeRepository.GetAllAsync(filter: s => !s.IsDeleted);
//                IEnumerable<MallDTO> malls = await _masterUnitOfWork.MallRepository.GetAllAsync(filter: s => !s.IsDeleted);
//                IEnumerable<OfficeDTO> offices = await _masterUnitOfWork.OfficeRepository.GetAllAsync(filter: s => !s.IsDeleted);
//                IEnumerable<FeedbackStatusDTO> statusList = await _unitOfWork.FeedbackStatusRepository.GetAllAsync();
//                IEnumerable<FeedbackPriorityDTO> priorityList = await _unitOfWork.FeedbackPriorityRepository.GetAllAsync();
//                IEnumerable<DepartmentDTO> departments = await _unitOfWork.DepartmentRepository.GetAllAsync();

//                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//                using (var package = new ExcelPackage())
//                {
//                    var worksheet = package.Workbook.Worksheets.Add("SurveyReport");

//                    worksheet.Cells[1, 1].Value = "STT";
//                    worksheet.Cells[1, 2].Value = "SĐT";
//                    worksheet.Cells[1, 3].Value = "Họ và tên";
//                    worksheet.Cells[1, 4].Value = "Email";
//                    if (applyTo == (int)EVOCType.Mall)
//                        worksheet.Cells[1, 5].Value = "TTTM";
//                    else if (applyTo == (int)EVOCType.Office)
//                        worksheet.Cells[1, 5].Value = "Văn phòng";
//                    worksheet.Cells[1, 6].Value = "Loại phản hồi";
//                    worksheet.Cells[1, 7].Value = "Nội dung";
//                    worksheet.Cells[1, 8].Value = "Trạng thái";
//                    worksheet.Cells[1, 9].Value = "Ưu tiên";
//                    worksheet.Cells[1, 10].Value = "Ngày tạo";
//                    worksheet.Cells[1, 11].Value = "Tạo bởi";
//                    worksheet.Cells[1, 12].Value = "Ngày cập nhật";
//                    worksheet.Cells[1, 13].Value = "Cập nhật bởi";
//                    worksheet.Cells[1, 14].Value = "PB. CLQ";
//                    worksheet.Cells[1, 15].Value = "Ghi chú của phòng ban";
//                    worksheet.Cells[1, 16].Value = "Giải pháp";
//                    // Add headers for the remaining columns...

//                    int index = 1;
//                    int order = 1;
//                    foreach (var item in feedbacks)
//                    {
//                        var mall = malls.FirstOrDefault(s => s.Id == item.MallId);
//                        var office = offices.FirstOrDefault(s => s.Id == item.OfficeId);
//                        var feedbackType = feedbackTypes.FirstOrDefault(s => s.Id == item.FeedbackTypeId);
//                        var forwardFeedbacks = await _unitOfWork.ForwardFeedbackRepository.GetAllAsync(filter: s => s.FeedbackId == item.Id);
//                        int rowInc = 0;
//                        worksheet.Cells[index + 1, 1].Value = order;
//                        worksheet.Cells[index + 1, 2].Value = item.PhoneNumber;
//                        worksheet.Cells[index + 1, 3].Value = item.FullName;
//                        worksheet.Cells[index + 1, 4].Value = item.Email;
//                        if (applyTo == (int)EVOCType.Mall)
//                            worksheet.Cells[index + 1, 5].Value = mall == null ? "" : mall.NameVN;
//                        else if (applyTo == (int)EVOCType.Office)
//                            worksheet.Cells[index + 1, 5].Value = office == null ? "" : office.NameVN;
//                        worksheet.Cells[index + 1, 6].Value = feedbackType == null ? "" : feedbackType.NameVN;
//                        worksheet.Cells[index + 1, 7].Value = item.Content;
//                        worksheet.Cells[index + 1, 8].Value = statusList.FirstOrDefault(s => s.Id == item.StatusId)?.Name;
//                        worksheet.Cells[index + 1, 9].Value = priorityList.FirstOrDefault(s => s.Id == item.PriorityId)?.Name;
//                        worksheet.Cells[index + 1, 10].Value = item.CreatedOn.ToString("dd-MM-yyyy HH:mm");
//                        worksheet.Cells[index + 1, 11].Value = item.CreatedBy;
//                        worksheet.Cells[index + 1, 12].Value = item.ModifiedOn.ToString("dd-MM-yyyy HH:mm");
//                        worksheet.Cells[index + 1, 13].Value = item.ModifiedBy;
//                        if (forwardFeedbacks.Count() > 0)
//                        {
//                            foreach (var forward in forwardFeedbacks)
//                            {
//                                rowInc++;
//                                worksheet.Cells[index + rowInc, 14].Value = departments.FirstOrDefault(s => s.Id == forward.DepartmentId)?.Name;
//                                worksheet.Cells[index + rowInc, 15].Value = forward.Response;
//                            }
//                        }
//                        worksheet.Cells[index + 1, 16].Value = item.Solution;

//                        if (rowInc > 0)
//                            index += rowInc;
//                        else
//                            index++;
//                        order++;
//                        // Add values for the remaining columns...
//                    }

//                    var stream = new MemoryStream();
//                    package.SaveAs(stream);

//                    var content = stream.ToArray();
//                    string path = Path.Combine(_webHostEnvironment.WebRootPath, EFolderName.ReportFiles.ToString());

//                    if (!Directory.Exists(path))
//                    {
//                        Directory.CreateDirectory(path);
//                    }
//                    //How to delete all files in a folder?
//                    DirectoryInfo directory = new DirectoryInfo(path);
//                    foreach (FileInfo file in directory.GetFiles())
//                    {
//                        file.Delete();
//                    }
//                    // create file name
//                    string fileName = "VOCReport" + ".xlsx";
//                    path = Path.Combine(path, fileName);
//                    //save file
//                    File.WriteAllBytes(path, content);

//                    return path;

//                }
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }
//    }
//}