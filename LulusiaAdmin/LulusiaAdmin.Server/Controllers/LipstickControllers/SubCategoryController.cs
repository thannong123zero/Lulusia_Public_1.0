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
    public class SubCategoryController : BaseApiController
    {
        private readonly ISubCategoryHelper _subCategoryHelper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public SubCategoryController(ISubCategoryHelper subCategoryHelper, IStringLocalizer<SharedResource> localizer)
        {
            _subCategoryHelper = subCategoryHelper;
            _localizer = localizer;
        }
        [HttpGet]
        [Route("getAll/{pageIndex}/{pageSize}/{categoryId}")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize, int categoryId)
        {
            if (pageIndex < 1)
                return Failed(EStatusCodes.BadRequest, _localizer["invalidPageIndex"]);
            Pagination<SubCategoryViewModel> data = await _subCategoryHelper.GetAllAsync(pageIndex,pageSize,categoryId);
            return Succeeded<Pagination<SubCategoryViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getAllActive")]
        public IActionResult GetAllActive()
        {
            IEnumerable<SubCategoryViewModel> data = _subCategoryHelper.GetAllActive();
            return Succeeded<IEnumerable<SubCategoryViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }

        [HttpGet]
        [Route("getById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var data = _subCategoryHelper.GetById(Id);
            if (data == null)
            {
                return Failed(EStatusCodes.NotFound, _localizer["dataNotFound"]);
            }
            return Succeeded<SubCategoryViewModel>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] SubCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _subCategoryHelper.Create(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataCreationFailed"]);
            return Succeeded(_localizer["dataCreatedSuccessfully"]);
        }
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] SubCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _subCategoryHelper.Update(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataUpdateFailed"]);
            return Succeeded(_localizer["dataUpdatedSuccessfully"]);
        }
        [HttpDelete]
        [Route("deleteBrand")]
        public IActionResult DeleteBrandByID(int Id)
        {
            var result = _subCategoryHelper.Delete(Id);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataDeletionFailed"]);
            return Succeeded(_localizer["dataDeletedSuccessfully"]);
        }
    }
}
