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
    public class TopicController : BaseApiController
    {
        private readonly ITopicHelper _topicHelper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public TopicController(ITopicHelper topicHelper, IStringLocalizer<SharedResource> localizer)
        {
            _topicHelper = topicHelper;
            _localizer = localizer;
        }
        [HttpGet]
        [Route("getAll/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                return Failed(EStatusCodes.BadRequest, _localizer["invalidPageIndex"]);
            Pagination<TopicViewModel> data = await _topicHelper.GetAllAsync(pageIndex,pageSize);
            return Succeeded<Pagination<TopicViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }

        [HttpGet]
        [Route("getAllActive")]
        public IActionResult GetAllActive()
        {
            IEnumerable<TopicViewModel> data = _topicHelper.GetAllActive();
            return Succeeded<IEnumerable<TopicViewModel>>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpGet]
        [Route("getById/{Id}")]
        public IActionResult GetById(int Id)
        {
            TopicViewModel data = _topicHelper.GetById(Id);
            if (data == null)
            {
                return Failed(EStatusCodes.NotFound, _localizer["dataNotFound"]);
            }
            return Succeeded<TopicViewModel>(data, _localizer["dataFetchedSuccessfully"]);
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromForm] TopicViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _topicHelper.Create(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataCreationFailed"]);
            return Succeeded(_localizer["dataCreatedSuccessfully"]);
        }
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromForm] TopicViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _localizer["invalidData"]);
            }
            var result = _topicHelper.Update(model);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataUpdateFailed"]);
            return Succeeded(_localizer["dataUpdatedSuccessfully"]);
        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int Id)
        {
            var result = _topicHelper.Delete(Id);
            if (!result)
                return Failed(EStatusCodes.BadRequest, _localizer["dataDeletionFailed"]);
            return Succeeded(_localizer["dataDeletedSuccessfully"]);
        }
    }
}
