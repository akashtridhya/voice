using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using voice.blazor.Models;
using voice.blazor.Models.Account;
using voice.blazor.Models.Consts;

namespace voice.blazor.Helpers
{
    public class AccountHelpers : IDisposable
    {
        #region Login API
        internal static async Task<dynamic> LoginAPI(LoginRequest loginRequest)
        {
            var ApiRequest = new LoginRequest
            {
                Username = loginRequest.Username,
                Password = loginRequest.Password,
                RememberMe = loginRequest.RememberMe,
            };

            var url = $"{ApiEndPointConsts.Account.Login}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(ApiRequest), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithoutToken(url, stringContent);
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
        #endregion Login API

        #region Forget Password API
        internal static async Task<dynamic> ForgetPasswordAPI(string email = null)
        {
            var ApiRequest = new ForgetPasswordRequest
            {
                Email = email
            };

            var url = $"{ApiEndPointConsts.Account.ForgetPassword}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(ApiRequest), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithoutToken(url, stringContent);
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
        #endregion Forget Password API

        //#region Reset Password API
        //internal static async Task<ResetPasswordResponse> ForgetPasswordAPI(ResetPasswordRequest request)
        //{
        //    var url = $"{ApiEndPointConsts.Account.ResetPassword}";
        //    var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        //    var response = await Services.Service.PostAPIWithoutToken(url, stringContent);
        //    var JsonResponse = JsonConvert.DeserializeObject<ForgetPasswordResponse>(response.Response);
        //    return new ResetPasswordResponse
        //    {
        //        StatusCode = response.StatusCode,
        //        Message = JsonResponse.Message
        //    };

        //}
        //#endregion Reset Password API

        //#region Password Flag
        //internal static async Task<PasswordFlagResponse> PasswordFlag()
        //{
        //    var url = $"{ApiEndPointConsts.Account.PasswordFlag}";
        //    var response = await Services.Service.GetAPIWithToken(url);
        //    var JsonResponse = JsonConvert.DeserializeObject<ForgetPasswordResponse>(response.Response);
        //    return new PasswordFlagResponse
        //    {
        //        StatusCode = response.StatusCode,
        //        Message = JsonResponse.Message
        //    };
        //}
        //#endregion Password Flag

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