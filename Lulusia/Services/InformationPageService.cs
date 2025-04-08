using Common;
using Common.ViewModels.LipstickClientViewModels;
using Newtonsoft.Json;

namespace Lulusia.Services
{
    public class InformationPageService
    {
        private readonly ClientAppConfig _appConfig;

        public InformationPageService(ClientAppConfig appConfig)
        {
            _appConfig = appConfig;
        }
        public async Task<InforPageClientViewModel?> GetInforPageByPageTypeId(int pageTypeId)
        {
            string baseUrl = _appConfig.GetBaseAPIURL();
            string url = _appConfig.GetByPageTypeIdInforPageUrl + pageTypeId;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseData))
                    {
                        InforPageClientViewModel data = JsonConvert.DeserializeObject<InforPageClientViewModel>(responseData);
                        return data;
                    }
                }
            }
            return null;
        }
    }
}
