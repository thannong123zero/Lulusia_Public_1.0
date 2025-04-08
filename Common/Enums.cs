using System.ComponentModel.DataAnnotations;

namespace Common
{
    public enum EUserAction
    {
        View,
        ViewDetails,
        Create,
        Update,
        SoftDalete,
        Restore,
        Delete,
        Export,
        Import,
        LogIn,
        LogOut
    }
    public enum EUserActionStatus
    {
        Successful,
        Failed
    }
    public enum EPriority
    {
        Low = 1,
        Medium = 2,
        Urgent = 3
    }
    public enum EStatus
    {
        New = 1,
        Processing = 2,
        Responsed = 3,
        Completed = 4,
        Closed = 5,
        ReOpen = 6
    }
    public enum EVOCType
    {
        Mall,
        Office
    }
    public enum EQuestionType
    {
        Option = 1,
        Open = 2,
        OptionOpen = 3,
        Rating = 4
    }
    public enum EControllers
    {

    }
    public enum EModules
    {
        Lipstick,
        SlideShow,
        Feature,
    }
    public enum EFolderNames
    {
        Blogs,
        HomeBanners,
        Brands,
        Products,
        Categories,
        Sliders,
        Users,
        Orders,
        Settings,
        Pages,
        System,
        Topics,
        Images,
        Videos,
        ReportFiles,
        QRCodes,
    }
    public enum EActions
    {
        View,
        Create,
        Update,
        SoftDelete,
        Restore,
        Delete,
        Export,
        Import
    }

    public enum EStatusCodes
    {
        Success = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        Locked = 423,
        InternalServerError = 500
    }
    public enum EBanners
    {
        MainBanner,
        SubBanner
    }
    public enum ELanguages
    {
        EN,
        VN
    }
    public enum EPageTypes
    {
        //[Display(Name = "aboutUs")]
        AboutUs = 1,
        //[Display(Name = "exchangeNReturnPolicy")]
        ExchangeNReturnPolicy = 2,
        //[Display(Name = "termsandConditions")]
        TermsAndConditions = 3,
        //[Display(Name = "privacyPolicy")]
        PrivacyPolicy = 4,
        //[Display(Name = "paymentPolicy")]
        PaymentPolicy = 5
    }
    public enum ESlideTypes
    {
        Image,
        Video
    }    
    public enum EQRCodeType
    {
        Number,
        Character,
        CharacterAndNumber,
    }
}
