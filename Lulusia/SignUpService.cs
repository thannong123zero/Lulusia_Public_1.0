using Lulusia.Helpers;
using Lulusia.Services;

namespace Lulusia
{
    public static class SignUpService
    {
        public static IServiceCollection SignUp(this IServiceCollection services)
        {
            services.AddScoped<InformationPageService>();
            services.AddScoped<BrandService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<BlogService>();
            services.AddScoped<TopicService>();
            services.AddScoped<BannerService>();
            //helper
            services.AddScoped<LayoutHelper>();
            services.AddScoped<HomePageHelper>();

            return services;
        }
    }
}
