using BusinessLogic.IHelpers.IFeatureHelper;
using Common;
using Common.ViewModels.QRViewModels;
using LulusiaAdmin.Server.Controllers.BaseApiControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LulusiaAdmin.Server.Controllers.FeatureControllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class QRCodeController : BaseApiController
    {
        private readonly IQRCodeHelper _qrCodeHelper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public QRCodeController(IQRCodeHelper qrCodeHelper, IStringLocalizer<SharedResource> localizer)
        {
            _qrCodeHelper = qrCodeHelper;
            _localizer = localizer;
        }
        

        [HttpPost]
        [Route("GenerateAQRCode")]
        public async Task<IActionResult> GenerateAQRCode([FromForm] QRCodeViewModel model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return Failed(EStatusCodes.BadRequest,_localizer["invalidData"]);
                }
                // Generate the QR code as a base64 string
                byte[] fileBytes = await _qrCodeHelper.GenerateQRCodeAsync(model);
                // Return the byte array as a PNG image
                return File(fileBytes, "image/png");
            }
            catch (Exception ex)
            {
                return Failed(EStatusCodes.InternalServerError, ex.Message);
            }
        }
        
        [HttpPost]
        [Route("GenerateListQRCode")]
        public async Task<IActionResult> GenerateListQRCode([FromForm] QRCodeListViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
                }
                string filePath = await _qrCodeHelper.GenerateListQRCodeAsync(model);
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                var fileName = Path.GetFileName(filePath);
                return File(fileBytes, "application/zip", fileName);
            }
            catch (Exception ex)
            {
                return Failed(EStatusCodes.InternalServerError, ex.Message);
            }
        }

        //[HttpPost]
        //[Route("MergeImage")]
        //public async Task<IActionResult> MergeImage([FromForm] MergeImageViewModel model)
        //{
        //    if (model == null)
        //    {
        //        return BadRequest();
        //    }
        //    string filePath = await _qrCodeHelper.MergeImage(model);
        //    if (string.IsNullOrEmpty(filePath))
        //    {
        //        return BadRequest();
        //    }
        //    var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        //    var fileName = Path.GetFileName(filePath);
        //    return File(fileBytes, "application/zip", fileName);
        //}
    }
}
