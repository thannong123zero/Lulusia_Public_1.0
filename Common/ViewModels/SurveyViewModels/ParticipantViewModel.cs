namespace Common.ViewModels.SurveyViewModels
{
    public class ParticipantViewModel
    {
        public int Id { get; set; }
        public int SurveyFromId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? Note { get; set; }
    }
}
