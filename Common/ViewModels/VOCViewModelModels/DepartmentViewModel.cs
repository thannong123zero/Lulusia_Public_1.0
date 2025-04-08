namespace Common.ViewModels.VOCViewModelModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Emails { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public DepartmentViewModel()
        {
            Priority = 1;
            IsActive = true;
        }
    }
}
