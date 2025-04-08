using System.Text.Json.Serialization;

namespace Common.ViewModels.VOCViewModelModels
{
    public class VOCUIConfigurationViewModel
    {
        [JsonPropertyName("Mall")]
        public List<ContentUIViewModel> Mall { get; set; }

        [JsonPropertyName("Office")]
        public List<ContentUIViewModel> Office { get; set; }
        public VOCUIConfigurationViewModel()
        {
            Mall = new List<ContentUIViewModel> { };
            Office = new List<ContentUIViewModel> { };
        }
    }
    public class ContentUIViewModel
    {
        [JsonPropertyName("Key")]
        public string Key { get; set; }

        [JsonPropertyName("EN")]
        public string EN { get; set; }

        [JsonPropertyName("VN")]
        public string VN { get; set; }
    }
}
