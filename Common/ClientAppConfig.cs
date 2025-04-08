namespace Common
{
    public class ClientAppConfig
    {
        public required string ProductMode { get; set; }
        public required string ProdBaseAPIUrl { get; set; }
        public required string DevBaseAPIUrl { get; set; }
        //InforPage
        public required string GetByPageTypeIdInforPageUrl { get; set; }
        public required string GetAllActiveBrandUrl { get; set; }
        public required string GetNavigationBarUrl { get; set; }
        public required string GetMenuUrl { get; set; }
        //Blog
        public required string GetAllActiveBlogUrl { get; set; }
        public required string GetLatestBlogsUrl { get; set; }
        public required string GetBlogByIdUrl { get; set; }
        public required string GetBlogsByTopicIdUrl { get; set; }
        //Topic
        public required string GetAllActiveTopicUrl { get; set; }
        public required string GetTopicsInHomePageUrl { get; set; }
        //HomeBanner
        public required string GetAllActiveHomeBannerUrl { get; set; }

        public string GetBaseAPIURL()
        {
            if (string.IsNullOrEmpty(DevBaseAPIUrl) || string.IsNullOrEmpty(ProdBaseAPIUrl) || string.IsNullOrEmpty(ProductMode))
            {
                throw new Exception("Base API URL is not configured in appsettings.json");
            }
            string url = DevBaseAPIUrl;
            if (ProductMode.ToUpper() == "PROD")
            {
                url = ProdBaseAPIUrl;
            }
            return url;
        }
    }
}
