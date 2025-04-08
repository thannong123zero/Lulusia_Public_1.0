using System.Threading.Tasks;
using Common;
using Common.Models;
using Common.ViewModels.LipstickViewModels;
using LipstickBusinessLogic.ILipstickHelpers;
using LulusiaAdmin.Server.Controllers.BaseApiControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LulusiaAdmin.Server.Controllers.LipstickControllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PageContentController : BaseApiController
    {
        private readonly IPageContentHelper _pageContentHelper;
        public readonly IStringLocalizer<SharedResource> _localizer;
        public PageContentController(IPageContentHelper pageContentHelper, IStringLocalizer<SharedResource> localizer)
        {
            _pageContentHelper = pageContentHelper;
            _localizer = localizer;
        }
        //[HttpGet]
        //[Route("getAllPageType")]
        //public IActionResult GetAllPageType()
        //{
        //    var data = Enum.GetValues(typeof(EPageType)).Cast<EPageType>().Select(x => new PageTypeViewModel { Id = (int)x, Name = _stringLocalizer[EnumHelper.GetDisplayName(x)] }).ToList();

        //    return Ok(data);
        //}

        [HttpGet]
        [Route("getAll/{pageIndex}/{pageSize}/{pageTypeId}")]
        public async Task<IActionResult> GetAll(int pageIndex,int pageSize,int pageTypeId)
        {
            if (pageIndex < 1)
                return Failed(EStatusCodes.BadRequest, _localizer["invalidPageIndex"]);
            Pagination<PageContentViewModel> data = await _pageContentHelper.GetAllAsync(pageIndex,pageSize);
            return Succeeded<Pagination<PageContentViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getByPageTypeId/{pageTyeId}")]
        public IActionResult GetByPageTypeId(int pageTyeId = -1)
        {
            IEnumerable<PageContentViewModel> data = _pageContentHelper.GetByPageTypeId(pageTyeId);
            return Succeeded<IEnumerable<PageContentViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }

        [HttpGet]
        [Route("getById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var data = _pageContentHelper.GetById(Id);
            if (data == null)
            {
                return Failed(EStatusCodes.NotFound, _localizer["dataNotFound"]);
            }
            return Succeeded<PageContentViewModel>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] PageContentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _pageContentHelper.Create(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataCreationFailed"]);
            return Succeeded(_localizer["dataCreatedSuccessfully"]);
        }
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] PageContentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _pageContentHelper.Update(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataUpdateFailed"]);

            return Succeeded(_localizer["dataUpdatedSuccessfully"]);
        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int Id)
        {
            //if (Id == 0)
            //{
            //    return BadRequest();
            //}
            var result = _pageContentHelper.Delete(Id);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataDeletionFailed"]);
            return Succeeded(_localizer["dataDeletedSuccessfully"]);
        }
    }
}
