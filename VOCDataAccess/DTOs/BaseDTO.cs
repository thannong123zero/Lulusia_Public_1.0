namespace VOCDataAccess.DTOs
{
    public abstract class BaseDTO
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public BaseDTO()
        {
            IsDeleted = false;
            IsActive = true;
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
    }
}
