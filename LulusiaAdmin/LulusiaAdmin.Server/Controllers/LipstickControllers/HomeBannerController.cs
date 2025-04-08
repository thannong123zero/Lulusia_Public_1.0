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
    public class HomeBannerController : BaseApiController
    {
        private readonly IHomeBannerHelper _homeBannerHelper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public HomeBannerController(IHomeBannerHelper homeBannerHelper, IStringLocalizer<SharedResource> localizer)
        {
            _homeBannerHelper = homeBannerHelper;
            _localizer = localizer;
        }
        [HttpGet]
        [Route("getAll/{pageIndex}/{pageSize}/{bannerTypeId}")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize,int bannerTypeId)
        {
            if (pageIndex < 1)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidPageIndex"]);
            }
            Pagination<HomeBannerViewModel> data = await _homeBannerHelper.GetAllAsync(pageIndex, pageSize, bannerTypeId);
            return Succeeded<Pagination<HomeBannerViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getByBannerTypeId/{bannerTypeId}")]
        public IActionResult GetByBannerTypeId(int bannerTypeId = -1)
        {
            IEnumerable<HomeBannerViewModel> data = _homeBannerHelper.GetByBannerTypeId(bannerTypeId);
            return Succeeded<IEnumerable<HomeBannerViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getAllActive")]
        public IActionResult GetAllActive()
        {
            IEnumerable<HomeBannerViewModel> data = _homeBannerHelper.GetAllActive();
            return Succeeded<IEnumerable<HomeBannerViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var data = _homeBannerHelper.GetById(Id);
            if (data == null)
            {
                return Failed(EStatusCodes.NotFound, _localizer["dataNotFound"]);
            }
            return Succeeded<HomeBannerViewModel>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromForm] HomeBannerViewModel model)
        {
            if (!ModelState.IsValid || model.ImageFile == null)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _homeBannerHelper.Create(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataCreationFailed"]);
            return Succeeded(_localizer["dataCreatedSuccessfully"]);
        }
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromForm] HomeBannerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result =_homeBannerHelper.Update(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataUpdateFailed"]);

            return Succeeded(_localizer["dataUpdatedSuccessfully"]);
        }
        [HttpDelete]
        [Route("delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            var result = _homeBannerHelper.Delete(Id);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataDeletionFailed"]);
            return Succeeded(_localizer["dataDeletedSuccessfully"]);
        }
    }
}
