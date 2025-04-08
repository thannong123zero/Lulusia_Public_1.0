using Common;
using Common.ViewModels.LipstickClientViewModels;
using Newtonsoft.Json;

namespace Lulusia.Services
{
    public class BlogService
    {
        private readonly ClientAppConfig _appConfig;
        public BlogService(ClientAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task<IEnumerable<BlogClientViewModel>?> GetAllActive()
        {
            string baseUrl = _appConfig.GetBaseAPIURL();
            string url = _appConfig.GetAllActiveBlogUrl;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseData))
                    {
                        IEnumerable<BlogClientViewModel> data = JsonConvert.DeserializeObject<IEnumerable<BlogClientViewModel>>(responseData);
                        return data;
                    }
                }
            }
            return null;
        }
        public async Task<IEnumerable<BlogClientViewModel>?> getByTopicId(int topicId)
        {
            string baseUrl = _appConfig.GetBaseAPIURL();
            string url = _appConfig.GetBlogsByTopicIdUrl + topicId;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseData))
                    {
                        IEnumerable<BlogClientViewModel> data = JsonConvert.DeserializeObject<IEnumerable<BlogClientViewModel>>(responseData);
                        return data;
                    }
                }
            }
            return null;
        }
        public async Task<IEnumerable<BlogClientViewModel>?> GetLatestBlogs()
        {
            string baseUrl = _appConfig.GetBaseAPIURL();
            string url = _appConfig.GetLatestBlogsUrl;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseData))
                    {
                        IEnumerable<BlogClientViewModel> data = JsonConvert.DeserializeObject<IEnumerable<BlogClientViewModel>>(responseData);
                        return data;
                    }
                }
            }
            return null;
        }
        public async Task<BlogClientViewModel?> GetById(int id)
        {
            string baseUrl = _appConfig.GetBaseAPIURL();
            string url = _appConfig.GetBlogByIdUrl + id;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseData))
                    {
                        BlogClientViewModel data = JsonConvert.DeserializeObject<BlogClientViewModel>(responseData);
                        return data;
                    }
                }
            }
            return null;
        }
    }
}
