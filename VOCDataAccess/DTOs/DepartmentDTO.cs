namespace VOCDataAccess.DTOs
{
    public class DepartmentDTO : BaseDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Emails { get; set; }
        public int Priority { get; set; }
    }
}
