namespace LipstickDataAccess.DTOs
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int DistrictID { get; set; }
        public int ProvinceID { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string? Note { get; set; }
        public bool IsApproved { get; set; }
        //public ICollection<InvoiceDetailsDTO> InvoiceDetails { get; set; }
        //public InvoiceDTO()
        //{
        //    InvoiceDetails = new List<InvoiceDetailsDTO>();
        //}

    }
}
