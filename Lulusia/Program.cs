using Common;
using Lulusia;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var applicationconfig = builder.Configuration.GetSection("AppConfig");
ClientAppConfig appConfig = applicationconfig.Get<ClientAppConfig>();


builder.Services.AddSingleton(appConfig);
// Add services to the container.
builder.Services.AddControllersWithViews();
#region AddLocalization
builder.Services.AddSingleton<WebAppLanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
            return factory.Create("ErrorMessage", assemblyName.Name);
        };
    });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
            new CultureInfo("en-US"),
            new CultureInfo("vi-VN")
        };

    options.DefaultRequestCulture = new RequestCulture("vi-VN");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    var cookieProvider = options.RequestCultureProviders
            .OfType<CookieRequestCultureProvider>()
            .First();
    //cookieProvider.Options.DefaultRequestCulture = new RequestCulture("vi-VN");
    options.RequestCultureProviders.Clear();
    options.RequestCultureProviders.Add(cookieProvider);
    //options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});
builder.Services.AddSingleton<WebAppLanguageService>();
#endregion
builder.Services.SignUp();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
#region Add Localization
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
#endregion
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
