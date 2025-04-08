//using Microsoft.Extensions.Localization;
//using System.Reflection;

//namespace LulusiaAdmin.Server
//{
//    public class WebAppLanguageService
//    {
//        private readonly IStringLocalizer _localizer;
//        public WebAppLanguageService(IStringLocalizerFactory factory)
//        {
//            var type = typeof(SharedResource);
//            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
//            _localizer = factory.Create("SharedResource", assemblyName.Name);
//        }

//        public LocalizedString this[string key] => _localizer[key];

//        public LocalizedString Getkey(string key)
//        {
//            return _localizer[key];
//        }
//    }
//}
