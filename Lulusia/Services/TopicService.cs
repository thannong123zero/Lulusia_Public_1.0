using Common;
using Common.ViewModels.LipstickClientViewModels;
using Newtonsoft.Json;

namespace Lulusia.Services
{
    public class TopicService
    {
        private readonly ClientAppConfig _appConfig;

        public TopicService(ClientAppConfig appConfig)
        {
            _appConfig = appConfig;
        }
        public async Task<IEnumerable<TopicClientViewModel>?> GetTopicsInHomePage()
        {
            string baseUrl = _appConfig.GetBaseAPIURL();
            string url = _appConfig.GetTopicsInHomePageUrl;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseData))
                    {
                        IEnumerable<TopicClientViewModel> data = JsonConvert.DeserializeObject<IEnumerable<TopicClientViewModel>>(responseData);
                        return data;
                    }
                }
            }
            return null;
        }
        public async Task<IEnumerable<TopicClientViewModel>?> GetAllActive()
        {
            string baseUrl = _appConfig.GetBaseAPIURL();
            string url = _appConfig.GetAllActiveTopicUrl;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseData))
                    {
                        IEnumerable<TopicClientViewModel> data = JsonConvert.DeserializeObject<IEnumerable<TopicClientViewModel>>(responseData);
                        return data;
                    }
                }
            }
            return null;
        }
    }
}
