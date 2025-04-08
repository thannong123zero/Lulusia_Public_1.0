using Common;
using Common.ViewModels.LipstickClientViewModels;
using Newtonsoft.Json;

namespace Lulusia.Services
{
    public class BrandService
    {
        private readonly ClientAppConfig _appConfig;
        public BrandService(ClientAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task<IEnumerable<BrandClientViewModel>?> GetAllActive()
        {
            string baseUrl = _appConfig.GetBaseAPIURL();
            string url = _appConfig.GetAllActiveBrandUrl;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseData))
                    {
                        IEnumerable<BrandClientViewModel> data = JsonConvert.DeserializeObject<IEnumerable<BrandClientViewModel>>(responseData);
                        return data;
                    }
                }
            }
            return null;
        }

    }
}
