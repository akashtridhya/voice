using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using voice.middleware.Models;
using voice.middleware.Models.Account;
using voice.middleware.Models.Consts;
using voice.middleware.Models.Users.Request;

namespace voice.middleware.Helpers
{
    public class UsersHelpers : IDisposable
    {
        #region Users List
        public static async Task<dynamic> UserList()
        {
            var url = $"{ApiEndPointConsts.UsersManagement.UsersList}";
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
        #endregion Users Lists

        #region Status Update User
        internal static async Task<dynamic> StatusUser(UserStatusRequest request)
        {
            var url = $"{ApiEndPointConsts.UsersManagement.UserStatus}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
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
        #endregion Status Update User

        #region Update User
        internal static async Task<dynamic> UpdateUser(UserUpdateRequest request)
        {
            var url = $"{ApiEndPointConsts.UsersManagement.UserUpdate}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
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
        #endregion Update User

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