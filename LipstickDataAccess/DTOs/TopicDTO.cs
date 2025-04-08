namespace LipstickDataAccess.DTOs
{
    public class TopicDTO : BaseDTO
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameVN { get; set; }
        public string? Note { get; set; }
        public int Priority { get; set; }
        public string? Avatar { get; set; }
        public bool InHomePage { get; set; }
        //public ICollection<ArticleDTO> Articles { get; set; }
        //public TopicDTO()
        //{
        //    Articles = new List<ArticleDTO>();
        //}
    }
}
