using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using voice.middleware.Models;
using voice.middleware.Models.Account;
using voice.middleware.Models.Consts;
using voice.middleware.Models.Dashboard.Request;
using voice.middleware.Models.Dashboard.Response;

namespace voice.middleware.Helpers
{
    public class DashboardHelpers : IDisposable
    {

        #region Edit Profile API
        internal static async Task<dynamic> EditProfilAPI(string Username = null, string firstName = null, string lastName = null, string email = null)
        {
            var APIRequest = new EditProfileRequest
            {
                Username = Username,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
            };
            var url = $"{ApiEndPointConsts.Account.EditProfile}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(APIRequest), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent);

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
        internal static async Task<dynamic> ChangePasswordAPI(string oldPassword = null, string newPassword = null, string confirmPassword = null)
        {
            var APIRequest = new ChangePasswordRequest
            {
                OldPassword = oldPassword,
                NewPassword = newPassword,
                ConfirmPassword = confirmPassword
            };

            var url = $"{ApiEndPointConsts.Account.ChangePassword}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(APIRequest), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent);
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
        public static async Task<dynamic> GetProfileAPI()
        {
            var url = $"{ApiEndPointConsts.Account.RetriveProfile}";
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
        #endregion Get Profile API

        #region Dashboard Data
        public static async Task<dynamic> GetDashboardData()
        {
            var url = $"{ApiEndPointConsts.Dashboard.GetDashBoardData}";
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
        #endregion Dashboard Data

        #region Logout
        public static async Task<dynamic> Logout()
        {
            var url = $"{ApiEndPointConsts.Account.Logout}";
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
        #endregion Logout

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