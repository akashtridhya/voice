using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using voice.middleware.Helpers;
using voice.middleware.Models.Account;
using voice.middleware.Models.Account.Response;
using voice.middleware.Models.Consts;

namespace voice.middleware.Controllers
{
    public class AuthController : BaseController
    {
        #region Object Declarations And Constructor
        public AuthController()
        {
            ViewBag.Success = false;
        }
        #endregion Object Declarations And Constructor

        #region Login
        [Route("", Name = "Login")]
        public IActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> LogIn([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
            var response = await AccountHelpers.LoginAPI(request.Username, request.Password);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
            {
                var responseSuccess = JsonConvert.DeserializeObject<LoginResponse>(response.data);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, request.Username),
                    new Claim(ClaimTypes.Email, request.Username),
                    new Claim(ClaimTypes.Authentication, responseSuccess.token),
                };
                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var claimsPrincipal = new ClaimsPrincipal(identity);
                // Set current principal
                Thread.CurrentPrincipal = claimsPrincipal;
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = request.RememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddMonths(6)
                }); 
                return JsonSuccessResponse(responseSuccess);
            }
        }
        #endregion Login

        #region Forget Password
        [Route("forget-password", Name = "ForgotPassword")]
        public IActionResult ForgetPassword()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");
            else
                return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);

            var response = await AccountHelpers.ForgetPasswordAPI(request.Email);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
        #endregion Forget Password

        //#region Reset Password
        //[Route("reset-password", Name = "Reset Password")]
        //public IActionResult ResetPassword()
        //{
        //    if (User.Identity.IsAuthenticated)
        //        return RedirectToAction("Index", "Dashboard");
        //    else
        //        return View();
        //}

        //[HttpPost]
        //public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        //{
        //    if (!ModelState.IsValid)
        //        return JsonErrorResponse(ModelState);

        //    var response = await AccountHelpers.ForgetPasswordAPI(request);
        //    if (response.StatusCode != StatusCodeConsts.Success)
        //        return JsonErrorResponse(response.Message);
        //    else
        //        return JsonSuccessResponse("success", Url.Action("Login", "Auth", null, protocol: Request.Scheme));
        //}
        //#endregion

        #region Handle Error
        [Route("unauthorized", Name = "UnAuthorized")]
        public IActionResult HandleError(bool sessionExpired = false, bool isForbidden = false)
        {
            if (sessionExpired)
                ViewBag.SessionExpired = sessionExpired;
            if (isForbidden)
                ViewBag.Forbidden = isForbidden;

            return View("~/Views/Shared/HandleError.cshtml");
        }
        #endregion Handle Error
    }
}