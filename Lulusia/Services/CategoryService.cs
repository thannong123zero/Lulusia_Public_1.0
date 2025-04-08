using Common;
using Common.ViewModels.LipstickClientViewModels;
using Newtonsoft.Json;

namespace Lulusia.Services
{
    public class CategoryService
    {
        private readonly ClientAppConfig _appConfig;

        public CategoryService(ClientAppConfig appConfig)
        {
            _appConfig = appConfig;
        }
        public async Task<IEnumerable<CategoryClientViewModel>?> GetNavigationBar()
        {
            string baseUrl = _appConfig.GetBaseAPIURL();
            string url = _appConfig.GetNavigationBarUrl;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseData))
                    {
                        List<CategoryClientViewModel> data = JsonConvert.DeserializeObject<List<CategoryClientViewModel>>(responseData);
                        return data;
                    }
                }
            }
            return null;
        }
        public async Task<IEnumerable<CategoryClientViewModel>?> GetMenu()
        {
            string baseUrl = _appConfig.GetBaseAPIURL();
            string url = _appConfig.GetMenuUrl;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseData))
                    {
                        List<CategoryClientViewModel> data = JsonConvert.DeserializeObject<List<CategoryClientViewModel>>(responseData);
                        return data;
                    }
                }
            }
            return null;
        }
    }
}
