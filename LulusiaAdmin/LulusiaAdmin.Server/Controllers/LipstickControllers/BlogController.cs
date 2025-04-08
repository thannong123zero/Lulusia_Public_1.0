using Common.ViewModels.LipstickViewModels;
using LipstickBusinessLogic.ILipstickHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using LulusiaAdmin.Server.Controllers.BaseApiControllers;
using Common.Models;
using Common;

namespace LulusiaAdmin.Server.Controllers.LipstickControllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BlogController : BaseApiController
    {
        private readonly IBlogHelper _blogHelper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public BlogController(IBlogHelper blogHelper, IStringLocalizer<SharedResource> localizer)
        {
            _blogHelper = blogHelper;
            _localizer = localizer;
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll(int pageIndex = 1)
        {
            if (pageIndex < 1)
                return Failed(EStatusCodes.BadRequest, _localizer["invalidPageIndex"]);
            Pagination<BlogViewModel> data = await _blogHelper.GetAllAsync(pageIndex);
            return Succeeded<Pagination<BlogViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getAllActive")]
        public IActionResult GetAllActive()
        {
            IEnumerable<BlogViewModel> data = _blogHelper.GetAllActive();
            return Succeeded<IEnumerable<BlogViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getByTopicId/{topicId}")]
        public IActionResult GetByTopicId(int topicId)
        {
            IEnumerable<BlogViewModel> data = _blogHelper.GetByTopicId(topicId);
            if (data == null)
            {
                return Failed(EStatusCodes.NotFound, _localizer["dataNotFound"]);
            }
            return Succeeded<IEnumerable<BlogViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }

        [HttpGet]
        [Route("getById/{Id}")]
        public IActionResult GetById(int Id)
        {
            BlogViewModel data = _blogHelper.GetById(Id);
            if (data == null)
            {
                return Failed(EStatusCodes.NotFound, _localizer["dataNotFound"]);
            }
            return Succeeded<BlogViewModel>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromForm] BlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _blogHelper.Create(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataCreationFailed"]);
            return Succeeded(_localizer["dataCreatedSuccessfully"]);
        }
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromForm] BlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _blogHelper.Update(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataUpdateFailed"]);
            return Succeeded(_localizer["dataUpdatedSuccessfully"]);
        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int Id)
        {
            var result = _blogHelper.Delete(Id);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataDeletionFailed"]);
            return Succeeded(_localizer["dataDeletedSuccessfully"]);
        }
    }
}
