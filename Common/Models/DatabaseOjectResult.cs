namespace Common.Models
{
    public class DatabaseOjectResult
    {
        public bool OK { get; set; }
        public string ErrorMessage { get; set; }
        public string Content { get; set; }
        public DatabaseOjectResult()
        {
            OK = false;
        }
    }
}
