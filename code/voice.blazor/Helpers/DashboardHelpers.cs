using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using voice.blazor.Models;
using voice.blazor.Models.Account;
using voice.blazor.Models.Consts;
using voice.blazor.Models.Dashboard.Request;
using voice.blazor.Models.Dashboard.Response;

namespace voice.blazor.Helpers
{
    public class DashboardHelpers : IDisposable
    {

        #region Edit Profile API
        internal static async Task<dynamic> EditProfilAPI(string Token,EditProfileRequest request)
        {
            var APIRequest = new EditProfileRequest
            {
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };
            var url = $"{ApiEndPointConsts.Account.EditProfile}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(APIRequest), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent, Token);

            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion

        #region Profile Image Upload
        public static async Task<dynamic> ProfileImageUpload(IFormFile image)
        {
            var url = $"{ApiEndPointConsts.Account.ImageProfile}";
            var response = await Services.Service.PostUploadFile(url, "", image);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion Profile Image Upload

        #region Image Remove
        public static async Task<dynamic> RemoveProfileImage()
        {
            var url = $"{ApiEndPointConsts.Account.RemoveImageProfile}";
            var response = await Services.Service.GetAPIWithToken(url);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion Image Remove

        #region Change Password API
        internal static async Task<dynamic> ChangePasswordAPI(string Token, ChangePasswordRequest request)
        {
            var APIRequest = new ChangePasswordRequest
            {
                OldPassword = request.OldPassword,
                NewPassword = request.NewPassword,
                ConfirmPassword = request.ConfirmPassword
            };

            var url = $"{ApiEndPointConsts.Account.ChangePassword}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(APIRequest), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent, Token);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion Change Password API

        #region Get Profile API
        public static async Task<dynamic> GetProfileAPI(string Token)
        {
            var url = $"{ApiEndPointConsts.Account.RetriveProfile}";
            var response = await Services.Service.GetAPIWithToken(url,Token);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion Get Profile API

        #region Dashboard Data
        public static async Task<dynamic> GetDashboardData(string Token)
        {
            var url = $"{ApiEndPointConsts.Dashboard.GetDashBoardData}";
            var response = await Services.Service.GetAPIWithToken(url, Token);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion Dashboard Data

        #region Dashboard Data
        public static async Task<dynamic> Logout(string Token = "")
        {
            var url = $"{ApiEndPointConsts.Account.Logout}";
            var response = await Services.Service.GetAPIWithToken(url, Token);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion Dashboard Data

        #region House Keeping
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool dispose)
        {
            if (dispose)
            {
            }
        }
        #endregion
    }
}