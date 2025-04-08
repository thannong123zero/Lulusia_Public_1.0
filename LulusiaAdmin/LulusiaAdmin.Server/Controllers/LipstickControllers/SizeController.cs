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
    public class SizeController : BaseApiController
    {
        private readonly ISizeHelper _sizeHelper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public SizeController(ISizeHelper sizeHelper, IStringLocalizer<SharedResource> localizer)
        {
            _sizeHelper = sizeHelper;
            _localizer = localizer;
        }
        [HttpGet]
        [Route("getAll/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                return Failed(EStatusCodes.BadRequest, _localizer["invalidPageIndex"]);
            Pagination<SizeViewModel> data = await _sizeHelper.GetAllAsync(pageIndex,pageSize);
            return Succeeded<Pagination<SizeViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }

        [HttpGet]
        [Route("getAllActive")]
        public IActionResult GetAllActive()
        {
            IEnumerable<SizeViewModel> data = _sizeHelper.GetAllActive();
            return Succeeded<IEnumerable<SizeViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getById/{Id}")]
        public IActionResult GetById(int Id)
        {
            SizeViewModel data = _sizeHelper.GetById(Id);
            if (data == null)
            {
                return Failed(EStatusCodes.NotFound, _localizer["dataNotFound"]);
            }
            return Succeeded<SizeViewModel>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] SizeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _sizeHelper.Create(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataCreationFailed"]);
            return Succeeded(_localizer["dataCreatedSuccessfully"]);
        }
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] SizeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _sizeHelper.Update(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataUpdateFailed"]);
            return Succeeded(_localizer["dataUpdatedSuccessfully"]);
        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int Id)
        {
            var result = _sizeHelper.SoftDelete(Id);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataDeletionFailed"]);
            return Succeeded(_localizer["dataDeletedSuccessfully"]);
        }
    }
}
