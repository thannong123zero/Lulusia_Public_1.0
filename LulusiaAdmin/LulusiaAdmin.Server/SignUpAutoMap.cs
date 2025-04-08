using AutoMapper;
using Common.ViewModels.LipstickViewModels;
using Common.ViewModels.SurveyViewModels;
using Common.ViewModels.SystemViewModels;
using DataAccess.DTOs;
using LipstickDataAccess.DTOs;
using SurveyDataAccess.DTOs;

namespace LulusiaAdmin.Server
{
    public class SignUpAutoMap : Profile
    {
        public SignUpAutoMap()
        {
            #region Lipstick
            //Lipstick
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<BrandDTO, BrandViewModel>().ReverseMap();
            //CreateMap<UnitDTO, UnitViewModel>().ReverseMap();
            CreateMap<TopicDTO, TopicViewModel>().ReverseMap();
            CreateMap<SubCategoryDTO, SubCategoryViewModel>().ReverseMap();
            CreateMap<ProductDTO, ProductViewModel>().ReverseMap();
            CreateMap<PageContentDTO, PageContentViewModel>().ReverseMap();
            CreateMap<HomeBannerDTO, HomeBannerViewModel>().ReverseMap();
            CreateMap<BlogDTO, BlogViewModel>().ReverseMap();
            CreateMap<PageTypeViewModel, PageTypeViewModel>().ReverseMap();
            CreateMap<SizeDTO, SizeViewModel>().ReverseMap();
            CreateMap<ColorDTO, ColorViewModel>().ReverseMap();
            CreateMap<PageTypeDTO, PageTypeViewModel>().ReverseMap();
            #endregion
            #region Survey
            CreateMap<SurveyFormDTO, SurveyFormViewModel>().ReverseMap();
            CreateMap<QuestionGroupDTO, QuestionGroupViewModel>().ReverseMap();
            CreateMap<QuestionDTO, QuestionViewModel>().ReverseMap();
            CreateMap<PredefinedAnswerDTO, PredefinedAnswerViewModel>().ReverseMap();
            CreateMap<SurveyQuestionDTO, SelectedQuestionViewModel>().ReverseMap();
            CreateMap<ParticipantDTO, ParticipantViewModel>().ReverseMap();
            CreateMap<SurveyFormDTO, SurveyFormViewModel>().ReverseMap();
            CreateMap<QuestionTypeDTO, QuestionTypeViewModel>().ReverseMap();
            #endregion
            #region System
            CreateMap<RoleDTO, RoleViewModel>().ReverseMap();
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
            CreateMap<ModuleDTO, ModuleViewModel>().ReverseMap();
            CreateMap<ControllerDTO, ControllerViewModel>().ReverseMap();
            CreateMap<ControllerActionDTO, ControllerActionViewModel>().ReverseMap();
            CreateMap<ActionDTO, ActionViewModel>().ReverseMap();
            #endregion
        }
    }
}
