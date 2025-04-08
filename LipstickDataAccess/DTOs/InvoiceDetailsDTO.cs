namespace LipstickDataAccess.DTOs
{
    public class InvoiceDetailsDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int InvoiceId { get; set; }
        public double Quantity { get; set; }
        //public InvoiceDTO Invoice { get; set; }
    }
}
