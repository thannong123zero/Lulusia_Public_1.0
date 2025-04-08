using Common.Models;
using Common.ViewModels.LipstickViewModels;
using Common;
using LipstickBusinessLogic.ILipstickHelpers;
using LipstickBusinessLogic.LipstickHelpers;
using LulusiaAdmin.Server.Controllers.BaseApiControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LulusiaAdmin.Server.Controllers.LipstickControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageTypeController : BaseApiController
    {
        private readonly IPageTypeHelper _pageTypeHelper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public PageTypeController(IPageTypeHelper pageTypeHelper, IStringLocalizer<SharedResource> localizer)
        {
            _pageTypeHelper = pageTypeHelper;
            _localizer = localizer;
        }
        [HttpGet]
        [Route("getAll/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                return Failed(EStatusCodes.BadRequest, _localizer["invalidPageIndex"]);
            Pagination<PageTypeViewModel> data = await _pageTypeHelper.GetAllAsync(pageIndex,pageSize);
            return Succeeded<Pagination<PageTypeViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getAllActive")]
        public IActionResult GetAllActive()
        {
            IEnumerable<PageTypeViewModel> data = _pageTypeHelper.GetAllActive();
            return Succeeded<IEnumerable<PageTypeViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getEPageType")]
        public async Task<IActionResult> GetEPageType()
        {
           List<string> data = await _pageTypeHelper.GetEPageTypesAsync();
            return Succeeded<List<string>>(data, _localizer["dataFetchedSuccessfully"]);
        }

        [HttpGet]
        [Route("getById/{Id}")]
        public IActionResult GetById(int Id)
        {
            PageTypeViewModel data = _pageTypeHelper.GetById(Id);
            if (data == null)
            {
                return Failed(EStatusCodes.NotFound, _localizer["dataNotFound"]);
            }
            return Succeeded<PageTypeViewModel>(data, _localizer["dataFetchedSuccessfully"]);
        }
        
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] PageTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _pageTypeHelper.Create(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataCreationFailed"]);
            return Succeeded(_localizer["dataCreatedSuccessfully"]);
        }
        
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] PageTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _pageTypeHelper.Update(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataUpdateFailed"]);
            return Succeeded(_localizer["dataUpdatedSuccessfully"]);
        }
       
    }
}
