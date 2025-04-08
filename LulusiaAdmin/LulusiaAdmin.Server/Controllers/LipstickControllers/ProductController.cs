using Common;
using Common.Models;
using Common.ViewModels.LipstickViewModels;
using LipstickBusinessLogic.ILipstickHelpers;
using LulusiaAdmin.Server.Controllers.BaseApiControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LulusiaAdmin.Server.Controllers.LipstickControllers
{
    [Route("api/Product")]
    [ApiController]
    //[Authorize]
    public class ProductController : BaseApiController
    {
        private readonly IProductHelper _productHelper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public ProductController(IProductHelper productHelper, IStringLocalizer<SharedResource> localizer)
        {
            _productHelper = productHelper;
            _localizer = localizer;
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll(int pageIndex = 1)
        {
            if (pageIndex < 1)
                return Failed(EStatusCodes.BadRequest, _localizer["invalidPageIndex"]);
            Pagination<ProductViewModel> data = await _productHelper.GetAllAsync(pageIndex);
            return Succeeded<Pagination<ProductViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            ProductViewModel data = await _productHelper.GetByIdAsync(Id);
            if (data == null)
            {
                return Failed(EStatusCodes.NotFound, _localizer["dataNotFound"]);
            }
            return Succeeded<ProductViewModel>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = await _productHelper.CreateAsync(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataCreationFailed"]);
            return Succeeded(_localizer["dataCreatedSuccessfully"]);
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromForm] ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = await _productHelper.UpdateAsync(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataUpdateFailed"]);
            return Succeeded(_localizer["dataUpdatedSuccessfully"]);
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteProductByID(int Id)
        {
            var result = await _productHelper.DeleteAsync(Id);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataDeletionFailed"]);
            return Succeeded(_localizer["dataDeletedSuccessfully"]);
        }
        //[HttpGet]
        //[Route("checkPermissionToDelete")]
        //public async Task<IActionResult> CheckPermissionToDelete(string ID)
        //{
        //    DatabaseOjectResult databaseOjectResult = new DatabaseOjectResult();
        //    databaseOjectResult.OK = await _productHelper.CheckPermissionToDelete(ID);
        //    return Ok(databaseOjectResult);
        //}
        //[HttpDelete]
        //[Route("softDeleteProduct")]
        //public async Task<IActionResult> SoftDeleteProductByID(string ID)
        //{
        //    if (ID == null)
        //    {
        //        return BadRequest();
        //    }
        //    await _productHelper.SoftDeleteProductByID(ID);

        //    return Ok();
        //}
        //[HttpPatch]
        //[Route("restoreProduct")]
        //public async Task<IActionResult> RestoreProductByID(string ID)
        //{
        //    if (ID == null)
        //    {
        //        return BadRequest();
        //    }
        //    await _productHelper.RestoreProductByID(ID);

        //    return Ok();
        //}
    }
}
