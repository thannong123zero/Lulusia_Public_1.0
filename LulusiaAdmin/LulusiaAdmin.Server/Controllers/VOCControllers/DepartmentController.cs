//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace LulusiaAdmin.Server.Controllers.VOCControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DepartmentController : ControllerBase
//    {
//        private readonly IDepartmentHelper _departmentHelper;
//        private readonly IActionloggingService _actionLog;
//        public DepartmentController(IDepartmentHelper departmentHelper, IActionloggingService actionLog)
//        {
//            _departmentHelper = departmentHelper;
//            _actionLog = actionLog;
//        }
//        [HttpGet("GetAll")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Department, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetAll()
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.View.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//            };
//            _actionLog.CreateAsync(userAction, token);
//            var data = await _departmentHelper.GetAllAsync();
//            return Ok(data);
//        }

//        [HttpGet("GetAllActive")]
//        [AllowAnonymous]
//        //[AuthorizeEnumPolicy(ERoleClaimGroup.Department, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetAllActive()
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.View.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//            };
//            _actionLog.CreateAsync(userAction, token);
//            var data = await _departmentHelper.GetAllActiveAsync();
//            return Ok(data);
//        }

//        [HttpGet("GetById/{id}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Department, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var data = await _departmentHelper.GetByIdAsync(id);
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.ViewDetails.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//                Details = JsonConvert.SerializeObject(data)
//            };
//            _actionLog.CreateAsync(userAction, token);
//            return Ok(data);
//        }

//        [HttpPost("Create")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Department, ERoleClaim.Create, EModule.VOC)]
//        public async Task<IActionResult> Create([FromBody] DepartmentViewModel model)
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.Create.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//                Details = JsonConvert.SerializeObject(model)
//            };
//            if (!ModelState.IsValid)
//            {
//                userAction.Status = EUserActionStatus.Failed.ToString();
//                _actionLog.CreateAsync(userAction, token);
//                return BadRequest(ModelState);
//            }
//            _actionLog.CreateAsync(userAction, token);
//            await _departmentHelper.CreateAsync(model);
//            return Ok();
//        }

//        [HttpPut("Update")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Department, ERoleClaim.Update, EModule.VOC)]
//        public async Task<IActionResult> Update([FromBody] DepartmentViewModel model)
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.Update.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//                Details = JsonConvert.SerializeObject(model)
//            };
//            if (!ModelState.IsValid)
//            {
//                userAction.Status = EUserActionStatus.Failed.ToString();
//                _actionLog.CreateAsync(userAction, token);
//                return BadRequest(ModelState);
//            }
//            _actionLog.CreateAsync(userAction, token);
//            await _departmentHelper.UpdateAsync(model);
//            return Ok();
//        }

//        [HttpPatch("SoftDelete/{id}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Department, ERoleClaim.SoftDelete, EModule.VOC)]
//        public async Task<IActionResult> SoftDelete(int id)
//        {
//            await _departmentHelper.SoftDeleteAsync(id);
//            return Ok();
//        }
//        [HttpPatch("Restore/{id}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Department, ERoleClaim.Restore, EModule.VOC)]
//        public async Task<IActionResult> Restore(int id)
//        {
//            await _departmentHelper.RestoreAsync(id);
//            return Ok();
//        }
//        [HttpDelete("Delete/{id}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Department, ERoleClaim.Delete, EModule.VOC)]
//        public async Task<IActionResult> Delete(int id)
//        {
//            await _departmentHelper.DeleteAsync(id);
//            return Ok();
//        }
//    }
//}
