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
    public class BrandController : BaseApiController
    {
        private readonly IBrandHelper _brandHelper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public BrandController(IBrandHelper brandHelper, IStringLocalizer<SharedResource> localizer)
        {
            _brandHelper = brandHelper;
            _localizer = localizer;
        }

        [HttpGet]
        [Route("getAll/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetAll(int pageIndex = 1, int pageSize = 0)
        {
            if (pageIndex < 1)
                return Failed(EStatusCodes.BadRequest, _localizer["invalidPageIndex"]);
            Pagination<BrandViewModel> data = await _brandHelper.GetAllAsync(pageIndex,pageSize);
            if (data == null)
            {
                return Failed(EStatusCodes.NotFound, _localizer["dataNotFound"]);
            }
            return Succeeded<Pagination<BrandViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }

        [HttpGet]
        [Route("getById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var data = _brandHelper.GetById(Id);
            if (data == null)
            {
                return Failed(EStatusCodes.NotFound, _localizer["dataNotFound"]);
            }
            return Succeeded<BrandViewModel>(data, _localizer["dataFetchedSuccessfully"]);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromForm] BrandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _brandHelper.Create(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataCreationFailed"]);
            return Succeeded(_localizer["dataCreatedSuccessfully"]);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromForm] BrandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _brandHelper.Update(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataUpdateFailed"]);

            return Succeeded(_localizer["dataUpdatedSuccessfully"]);
        }

        [HttpDelete]
        [Route("deleteBrand")]
        public IActionResult DeleteBrandByID(int Id)
        {
            var result = _brandHelper.Delete(Id);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataDeletionFailed"]);
            return Succeeded(_localizer["dataDeletedSuccessfully"]);
        }
        //[HttpGet]
        //[Route("checkPermissionToDelete")]
        //public async Task<IActionResult> CheckPermissionToDelete(string ID)
        //{
        //    DatabaseOjectResult databaseOjectResult = new DatabaseOjectResult();
        //    databaseOjectResult.OK = await _brandHelper.CheckPermissionToDelete(ID);
        //    return Ok(databaseOjectResult);
        //}
        //[HttpDelete]
        //[Route("softDeleteBrand")]
        //public async Task<IActionResult> SoftDeleteBrandByID(string ID)
        //{
        //    if (ID == null)
        //    {
        //        return BadRequest();
        //    }
        //    await _brandHelper.SoftDeleteBrandByID(ID);

        //    return Ok();
        //}
        //[HttpPatch]
        //[Route("restoreBrand")]
        //public async Task<IActionResult> RestoreBrandByID(string ID)
        //{
        //    if (ID == null)
        //    {
        //        return BadRequest();
        //    }
        //    await _brandHelper.RestoreBrandByID(ID);

        //    return Ok();
        //}
    }
}
