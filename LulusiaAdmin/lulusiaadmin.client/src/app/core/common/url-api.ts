export enum EUrl {

    //#region Feature
    //QRCode
    generateAQRCode = "/api/qrcode/generateaqrcode",
    generateListQRCode = "/api/qrcode/generatelistqrcode",
    //#endregion

    //#region LipstickShop
    //Report
    getAllUrlReport = "/api/report/getall",
    //Order
    getAllUrlOrder = "/api/order/getall",
    getByIdUrlOrder = "/api/order/getbyid",
    createUrlOrder = "/api/order/create",
    updateUrlOrder = "/api/order/update",
    //Product
    getAllUrlProduct = "/api/product/getall",
    getByIdUrlProduct = "/api/product/getbyid",
    createUrlProduct = "/api/product/create",
    updateUrlProduct = "/api/product/update",
    softDeleteUrlProduct = "/api/product/softdelete",
    //Blog
    getAllUrlBlog = "/api/blog/getall",
    getAllActiveUrlBlog = "/api/blog/getallactive",
    getByIdUrlBlog = "/api/blog/getbyid",
    getByTopicIdUrlBlog = "/api/blog/getbytopicid",
    createUrlBlog = "/api/blog/create",
    updateUrlBlog = "/api/blog/update",
    softDeleteUrlBlog = "/api/blog/softdelete",
    //Brand
    getAllUrlBrand = "/api/brand/getall",
    getByIdUrlBrand = "/api/brand/getbyid",
    createUrlBrand = "/api/brand/create",
    updateUrlBrand = "/api/brand/update",
    getAllActiveUrlBrand = "/api/brand/getallactive",
    softDeleteUrlBrand = "/api/brand/softdelete",
    //Category
    getAllUrlCategory = "/api/category/getall",
    getAllActiveUrlCategory = "/api/category/getallactive",
    getByIdUrlCategory = "/api/category/getbyid",
    createUrlCategory = "/api/category/create",
    updateUrlCategory = "/api/category/update",
    softDeleteUrlCategory = "/api/category/softdelete",
    //SubCategory
    getAllUrlSubCategory = "/api/subcategory/getall",
    getAllActiveUrlSubCategory = "/api/subcategory/getallactive",
    getByIdUrlSubCategory = "/api/subcategory/getbyid",
    createUrlSubCategory = "/api/subcategory/create",
    updateUrlSubCategory = "/api/subcategory/update",
    softDeleteUrlSubCategory = "/api/subcategory/softdelete",
    getByCategoryIdUrlSubCategory = "/api/subcategory/getbycategoryid",
    //Color
    getAllUrlColor = "/api/color/getall",
    getAllActiveUrlColor = "/api/color/getallactive",
    getByIdUrlColor = "/api/color/getbyid",
    createUrlColor = "/api/color/create",
    updateUrlColor = "/api/color/update",
    softDeleteUrlColor = "/api/color/softdelete",
    //Size
    getAllUrlSize = "/api/size/getall",
    getAllActiveUrlSize = "/api/size/getallactive",
    getByIdUrlSize = "/api/size/getbyid",
    createUrlSize = "/api/size/create",
    updateUrlSize = "/api/size/update",
    softDeleteUrlSize = "/api/size/softdelete",
    //PageType
    getAllUrlPageType = "/api/pagetype/getall",
    getAllActiveUrlPageType = "/api/pagetype/getallactive",
    getByIdUrlPageType = "/api/pagetype/getbyid",
    createUrlPageType = "/api/pagetype/create",
    updateUrlPageType = "/api/pagetype/update",
    softDeleteUrlPageType = "/api/pagetype/softdelete",
    getEPageTypeUrlPageType = "/api/pagetype/getepagetype",
    //PageContent
    getAllUrlPageContent = "/api/pagecontent/getall",
    getAllActiveUrlPageContent = "/api/pagecontent/getallactive",
    getByIdUrlPageContent = "/api/pagecontent/getbyid",
    createUrlPageContent = "/api/pagecontent/create",
    updateUrlPageContent = "/api/pagecontent/update",
    softDeleteUrlPageContent = "/api/pagecontent/softdelete",
    //PageIntroduction
    getAllUrlPageIntroduction = "/api/pageintro/getall",
    getAllActiveUrlPageIntroduction = "/api/pageintro/getallactive",
    getByIdUrlPageIntroduction = "/api/pageintro/getbyid",
    createUrlPageIntroduction = "/api/pageintro/create",
    updateUrlPageIntroduction = "/api/pageintro/update",
    softDeleteUrlPageIntroduction = "/api/pageintro/softdelete",
    //Topic
    getAllUrlTopic = "/api/topic/getall",
    getAllActiveUrlTopic = "/api/topic/getallactive",
    getByIdUrlTopic = "/api/topic/getbyid",
    createUrlTopic = "/api/topic/create",
    updateUrlTopic = "/api/topic/update",
    softDeleteUrlTopic = "/api/topic/softdelete",
    //InforPage
    getAllPageTypeUrlInforPage = "/api/inforpage/getallpagetype",
    getAllUrlInforPage = "/api/inforpage/getall",
    getAllActiveUrlInforPage = "/api/inforpage/getallactive",
    getByIdUrlInforPage = "/api/inforpage/getbyid",
    createUrlInforPage = "/api/inforpage/create",
    updateUrlInforPage = "/api/inforpage/update",
    softDeleteUrlInforPage = "/api/inforpage/softdelete",
    getByPageTypeId = "/api/inforpage/getbypagetypeid",
    //HomeBanner
    getAllUrlHomeBanner = "/api/homebanner/getall",
    getAllActiveUrlHomeBanner = "/api/homebanner/getallactive",
    getByIdUrlHomeBanner = "/api/homebanner/getbyid",
    createUrlHomeBanner = "/api/homebanner/create",
    updateUrlHomeBanner = "/api/homebanner/update",
    softDeleteUrlHomeBanner = "/api/homebanner/softdelete",
    getByBannerTypeId = "/api/homebanner/getbybannertypeid",

    //#endregion



    //#region System

    //My Account
    changePasswordUrlMyAccount = "/api/myaccount/changepassword",
    loginUrl = "/api/myaccount/login",
    recoverPasswordUrl = "/api/myaccount/recoverpassword",
    resetPasswordUrl = "/api/myaccount/resetpassword",
    refreshTokenUrl = "/api/myaccount/refreshtoken",
    validateRefreshTokenUrl = "/api/myaccount/validaterefreshtoken",
    reNewToken = "/api/myaccount/renewtoken",
    //Account
    getAllUrlAccount = "/api/usersystem/getall",
    getByIdUrlAccount = "/api/usersystem/getbyid/",
    createUrlAccount = "/api/usersystem/create",
    updateUrlAccount = "/api/usersystem/update",
    //Role
    getAllUrlRole = "/api/role/getall",
    getByIdUrlRole = "/api/role/getbyid",
    createUrlRole = "/api/role/create",
    updateUrlRole = "/api/role/update",
    //Module
    getAllUrlModule = "/api/module/getall",
    getByIdUrlModule = "/api/module/getbyid",
    createUrlModule = "/api/module/create",
    updateUrlModule = "/api/module/update",
    //Action
    getAllUrlAction = "/api/action/getall",
    getEActionUrlAction = "/api/action/geteaction",
    getByIdUrlAction = "/api/action/getbyid",
    createUrlAction = "/api/action/create",
    updateUrlAction = "/api/action/update",
    //Setting
    getAllUrlSetting = "/api/setting/getall",
    getByKeyUrlSetting = "/api/setting/getbykey",
    updateUrlSetting = "/api/setting/update",
    //#endregion
}