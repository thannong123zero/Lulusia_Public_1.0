using BusinessLogic.Helpers.FeatureHelpers;
using BusinessLogic.Helpers.SystemHelpers;
using BusinessLogic.IHelpers.IFeatureHelper;
using BusinessLogic.IHelpers.ISystemHelpers;
using Common.Services.ActionLoggingServices;
using Common.Services.EMailServices;
using Common.Services.FileStorageServices;

//using Common.Services.ImageStorageServices;
using Common.Services.JwtServices;
using Common.Services.QRCodeServices;
using LipstickBusinessLogic.ILipstickClientHelpers;
using LipstickBusinessLogic.ILipstickHelpers;
using LipstickBusinessLogic.LipstickClientHelpers;
using LipstickBusinessLogic.LipstickHelpers;
using SurveyBusinessLogic.Helpers;
using SurveyBusinessLogic.IHelpers;

namespace LulusiaAdmin.Server
{
    public static class SignUpService
    {
        public static IServiceCollection SignUp(this IServiceCollection services)
        {
            #region Lipstick
            services.AddScoped<LipstickDataAccess.IUnitOfWork, LipstickDataAccess.UnitOfWork>();
            //lipstick
            //services.AddTransient<IUnitHelper, UnitHelper>();
            services.AddTransient<ICategoryHelper, CategoryHelper>();
            services.AddTransient<ISubCategoryHelper, SubCategoryHelper>();
            services.AddTransient<IBrandHelper, BrandHelper>();
            services.AddTransient<ITopicHelper, TopicHelper>();
            services.AddTransient<IBlogHelper, BlogHelper>();
            services.AddTransient<IProductHelper, ProductHelper>();
            services.AddTransient<IEmailHelper, EmailHelper>();
            services.AddTransient<IQRCodeHelper, QRCodeHelper>();
            services.AddTransient<IPageContentHelper, PageContentHelper>();
            services.AddTransient<IHomeBannerHelper, HomeBannerHelper>();
            services.AddTransient<IPageTypeHelper, PageTypeHelper>();
            services.AddTransient<ISizeHelper, SizeHelper>();
            services.AddTransient<IColorHelper, ColorHelper>();
            //lipstick client
            services.AddTransient<IInforPageClientHelper, InforPageClientHelper>();
            services.AddTransient<IBrandClientHelper, BrandClientHelper>();
            services.AddTransient<ICategoryClientHelper, CategoryClientHelper>();
            services.AddTransient<ITopicClientHelper, TopicClientHelper>();
            services.AddTransient<IBlogClientHelper, BlogClientHelper>();
            services.AddTransient<IHomeBannerClientHelper, HomeBannerClientHelper>();
            //services.AddScoped<IOrderHelper>
            #endregion
            #region Survey
            services.AddScoped<SurveyDataAccess.IUnitOfWork, SurveyDataAccess.UnitOfWork>();
            services.AddScoped<IParticipantHelper, ParticipantHelper>();
            services.AddScoped<IQuestionGroupHelper, QuestionGroupHelper>();
            services.AddScoped<IQuestionHelper, QuestionHelper>();
            services.AddScoped<ISurveyFormHelper, SurveyFormHelper>();
            services.AddScoped<ISurveyReportHelper, SurveyReportHelper>();
            services.AddScoped<IQuestionTypeHelper, QuestionTypeHelper>();
            #endregion
            #region System Database
            services.AddScoped<DataAccess.IUnitOfWork, DataAccess.UnitOfWork>();
            //system
            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IModuleHelper, ModuleHelper>();
            services.AddScoped<IRoleHelper, RoleHelper>();
            services.AddScoped<IMyAccountHelper, MyAccountHelper>();
            services.AddScoped<IActionHelper, ActionHelper>();
            //services.AddScoped<IRoleClaimGroupHelper, RoleClaimGroupHelper>();
            //services.AddScoped<IRoleClaimHelper, RoleClaimHelper>();
            services.AddScoped<ISettingHelper, SettingHelper>();
            #endregion
            

            #region Service
            //service
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IQRCodeService, QRCodeService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IActionloggingService, ActionLoggingService>();
            #endregion
            return services;
        }
    }
}
