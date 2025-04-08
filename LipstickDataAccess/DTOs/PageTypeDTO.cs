namespace LipstickDataAccess.DTOs
{
    public class PageTypeDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Label {  get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public PageTypeDTO()
        {
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
    }
}
