using Newtonsoft.Json;

namespace Lulusia
{    // language code
    public enum ELanguage
    {
        VN,
        EN
    }
    public enum EWebContentKey
    {
        HostName,
        FacebookLink,
        InstagramLink,
        YoutubeLink,
        GithubLink,
        PhoneNumber,
        Email,
        Address,
        CompanyName
    }
    public static class Global
    {
        private const string webContentFilePath = @".\LocalData\WebContent.json";
        private static List<WebContent> webContents = new List<WebContent>();

        static Global()
        {
            using StreamReader reader = new(webContentFilePath);
            string json = reader.ReadToEnd();
            webContents = JsonConvert.DeserializeObject<List<WebContent>>(json);
        }
        public static string GetLanguageCode(HttpRequest request)
        {
            string? languageCookie = request.Cookies[".AspNetCore.Culture"];
            if (!string.IsNullOrEmpty(languageCookie) && languageCookie.Contains("en-US"))
            {
                return ELanguage.EN.ToString();
            }
            else
            {
                return ELanguage.VN.ToString();
            }
        }

        public static string GetWebContentValueByKey(Enum key, string language)
        {
            WebContent data = webContents.FirstOrDefault(s => string.Equals(s.Key, key.ToString()));
            if (data == null)
            {
                return "";
            }
            return language == ELanguage.EN.ToString() ? data.EN : data.VN;
        }
        public static void UpdateUIVOCPageContent(WebContent model)
        {
            WebContent data = webContents.FirstOrDefault(s => string.Equals(s.Key, model.Key));
            if (data == null)
            {
                return;
            }
            data.EN = data.EN;
            data.VN = data.VN;
            string json = JsonConvert.SerializeObject(webContents);
            File.WriteAllText(webContentFilePath, json);
        }
    }
    public class WebContent
    {
        public required string Key { get; set; }
        public required string EN { get; set; }
        public required string VN { get; set; }
    }
    public class Province
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<District> Districts { get; set; }
        public Province()
        {
            Districts = new List<District>();
        }
    }
    public class District
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
