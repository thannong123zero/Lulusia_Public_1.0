﻿using BusinessLogic.IHelpers.ISystemHelpers;
using Common;
using Common.ViewModels.SystemViewModels;
using DataAccess.DTOs;
using LulusiaAdmin.Server.Controllers.BaseApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LulusiaAdmin.Server.Controllers.SystemControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MyAccountController : BaseApiController
    {
        private readonly SignInManager<UserDTO> _signInManager;
        private readonly IMyAccountHelper _myAccountHelper;
        public readonly IStringLocalizer<MyAccountController> _localizer;
        public readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public MyAccountController(SignInManager<UserDTO> signInManager,
            IStringLocalizer<MyAccountController> localizer,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMyAccountHelper myAccountHelper)
        {
            _signInManager = signInManager;
            _myAccountHelper = myAccountHelper;
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _sharedLocalizer["invalidData"]);
            }
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                JwtViewModel jwt = await _myAccountHelper.LoginAsync(model);
                return Succeeded<JwtViewModel>(jwt, _localizer["loginSuccess"]);
            }
            else if (result.IsLockedOut)
            {
                return Failed(EStatusCodes.Locked, _localizer["accountLocked"]);
            }
            else
            {
                return Failed(EStatusCodes.BadRequest, _localizer["usernameOrPasswordIncorrect"]);
            }
        }

        [HttpPost("RecoverPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _sharedLocalizer["invalidData"]);
            }
            bool result = await _myAccountHelper.RecoverPassword(model);
            if (result)
            {
                return Succeeded(_localizer["recoverPasswordSuccess"]);
            }
            else
            {
                return Failed(EStatusCodes.BadRequest, _localizer["recoverPasswordFailed"]);
            }
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _sharedLocalizer["invalidData"]);
            }
            var result = await _myAccountHelper.ResetPasswordAsync(model);
            if (result)
            {
                return Succeeded(_localizer["resetPasswordSuccess"]);
            }
            else
            {
                return BadRequest(_localizer["resetPasswordFailed"]);
            }
        }

        [HttpGet("ValidateRefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateRefreshToken([FromHeader] string refreshToken)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _sharedLocalizer["invalidData"]);
            }
            bool result = await _myAccountHelper.ValidateRefreshTokenAsync(refreshToken);
            if (result)
            {
                return Succeeded(_localizer["refreshTokenValid"]);
            }
            else
            {
                return Failed(EStatusCodes.BadRequest, _localizer["refreshTokenInvalid"]);
            }
        }

        [HttpGet("ReNewToken")]
        public async Task<IActionResult> ReNewToken([FromHeader] string refreshToken, [FromHeader] string token)
        {
            JwtViewModel jwt = await _myAccountHelper.ReNewTokenAsync(refreshToken, token);
            if (jwt != null)
            {
                return Succeeded<JwtViewModel>(jwt, _localizer["renewTokenSuccess"]);
            }
            else
            {
                return Failed(EStatusCodes.BadRequest, _localizer["renewTokenFailed"]);
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failed(EStatusCodes.BadRequest, _sharedLocalizer["invalidData"]);
            }
            bool result = await _myAccountHelper.ChangePasswordAsync(model);
            if (result)
            {
                return Succeeded(_localizer["changePasswordSuccess"]);
            }
            else
            {
                return Failed(EStatusCodes.BadRequest, _localizer["changePasswordFailed"]);
            }
        }
    }
}
