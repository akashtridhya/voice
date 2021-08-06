using voice.middleware.Helpers;
using voice.middleware.Models.Account.Response;
using voice.middleware.Models.Consts;
using voice.middleware.Models.Dashboard.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using voice.middleware.Models.Dashboard.Response;

namespace voice.middleware.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        #region Index
        [Route("dashboard", Name = "Dashboard")]
        public async Task<IActionResult> Index()
        {
            object response = null;
            var responseGet = await DashboardHelpers.GetDashboardData();
            if (responseGet.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            else
                response = JsonConvert.DeserializeObject<DashboardResponse>(responseGet.data);
            return View(response);
        }
        #endregion Index

        #region Edit Profile
        [Route("edit-profile", Name = "EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            var responseGet = await DashboardHelpers.GetProfileAPI();
            var response = JsonConvert.DeserializeObject<LoginResponse>(responseGet.data);
            if (responseGet.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return View(response);
        }

        [HttpPost]
        public async Task<ActionResult> EditProfile([FromBody] EditProfileRequest request)
        {
            if (!ModelState.IsValid)
               return JsonErrorResponse(ModelState);

            var response = await DashboardHelpers.EditProfilAPI(request.Username, request.FirstName, request.LastName, request.Email);

            if (response.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            else if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
            {
                JsonSuccessResponse(response.meta.message);
                if (request.File != null)
                {
                    var responseImageUpload = await DashboardHelpers.ProfileImageUpload(request.File);

                    if (response.meta.statusCode == StatusCodeConsts.UnAuthorized)
                        return SessionExpired();
                    else if (responseImageUpload.meta.statusCode != StatusCodeConsts.Success)
                        return JsonErrorResponseList(response.meta.message);
                    else
                    {
                        var Image = await DashboardHelpers.GetProfileAPI();
                        object responseImage = JsonConvert.DeserializeObject<LoginResponse>(Image.data);
                        HttpContext.Session.SetObject("ProfileData", responseImage);
                    }
                }
            }
            return JsonSuccessResponse(response.meta.message);
        }

        [HttpPost]
        public async Task<ActionResult> EditImage(ProfileUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);

            if (request.File != null)
            {
                var response = await DashboardHelpers.ProfileImageUpload(request.File);

                if (response.meta.statusCode == StatusCodeConsts.UnAuthorized)
                    return SessionExpired();
                else if (response.meta.statusCode != StatusCodeConsts.Success)
                    return JsonErrorResponseList(response.meta.message);
                else
                {
                    var Image = await DashboardHelpers.GetProfileAPI();
                    object responseImage = JsonConvert.DeserializeObject<LoginResponse>(Image.data);
                    HttpContext.Session.SetObject("ProfileData", responseImage);
                }
                return JsonSuccessResponse(response.meta.message);
            }
            return JsonSuccessResponse();
        }

        [HttpGet]
        public async Task<ActionResult> RemoveImage()
        {
            var response = await DashboardHelpers.RemoveProfileImage();

            if (response.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return JsonSuccessResponse(response.meta.message);
        }
        #endregion Edit Profile

        #region Change Passowrd
        [Route("change-password", Name = "ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
            var response = await DashboardHelpers.ChangePasswordAPI(request.OldPassword, request.NewPassword, request.ConfirmPassword);

            if (response.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            else if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            return JsonSuccessResponse(response.meta.message);
        }
        #endregion Change Password

        #region Logout
        [Route("logout", Name = "Logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            await DashboardHelpers.Logout();
            return RedirectToAction("Login", "Auth");
        }
        #endregion

        #region Get Profile Response
        public async Task<dynamic> GetProfileAPIResponse()
        {
            var profileResponse = await DashboardHelpers.GetProfileAPI();
            if (profileResponse.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return RedirectToAction("HandleError", "Auth", new { sessionExpired = false, isForbidden = true });
            return JsonConvert.DeserializeObject<LoginResponse>(profileResponse.data);
        }
        #endregion Get Profile Response
    }
}