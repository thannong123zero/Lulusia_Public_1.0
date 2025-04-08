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
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryHelper _categoryHelper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public CategoryController(ICategoryHelper categoryHelper, IStringLocalizer<SharedResource> localizer)
        {
            _categoryHelper = categoryHelper;
            _localizer = localizer;
        }
        [HttpGet]
        [Route("getAll/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetAll(int pageIndex = 1, int pageSize = 0)
        {
            if (pageIndex < 1)
                return Failed(EStatusCodes.BadRequest, _localizer["invalidPageIndex"]);
            Pagination<CategoryViewModel> data = await _categoryHelper.GetAllAsync(pageIndex,pageSize);
            return Succeeded<Pagination<CategoryViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getAllActive")]
        public IActionResult GetAllActive()
        {
            IEnumerable<CategoryViewModel> data = _categoryHelper.GetAllActive();
            return Succeeded<IEnumerable<CategoryViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var data = _categoryHelper.GetById(Id);
            if (data == null)
            {
                return Failed(EStatusCodes.NotFound, _localizer["dataNotFound"]);
            }
            return Succeeded<CategoryViewModel>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _categoryHelper.Create(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataCreationFailed"]);
            return Succeeded(_localizer["dataCreatedSuccessfully"]);
        }
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _categoryHelper.Update(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataUpdateFailed"]);
            return Succeeded(_localizer["dataUpdatedSuccessfully"]);
        }
        [HttpDelete]
        [Route("softDelete")]
        public IActionResult softDalete(int Id)
        {
            var result = _categoryHelper.SoftDelete(Id);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataDeletionFailed"]);
            return Succeeded(_localizer["dataDeletedSuccessfully"]);
        }
    }
}
