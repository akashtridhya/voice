using Newtonsoft.Json;
using System.Threading.Tasks;
using voice.middleware.Models;
using voice.middleware.Models.Account;
using voice.middleware.Models.Consts;
using voice.middleware.Models.Generic.Request;
using voice.middleware.Models.Generic.Response;

namespace voice.middleware.Helpers
{
    public class GenericHelpers
    {
        #region Image Upload
        public static async Task<dynamic> ImageUpload(ImageUploadRequest request)
        {
            var url = $"{ApiEndPointConsts.Generic.ImageUpload}";
            var response = await Services.Service.PostUploadFile(url, request.Type, request.File);
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
        #endregion Image Upload

        //#region CheckPermission

        //public static async Task<CheckPermissionResponse> CheckPermission(string pageName, string permission)
        //{
        //    var loginResponse = await DashboardHelpers.GetProfileAPI();

        //    if (loginResponse != null && loginResponse.data != null)
        //    {

        //    }
        //    return new CheckPermissionResponse
        //    {
        //    };
        //}

        //#endregion CheckPermission

        //#region Get Source URL

        //public static async Task<dynamic> GetSourceURL(string shortURL)
        //{
        //    var url = $"{ApiEndPointConsts.Generic.GetSourceURL}{shortURL}";
        //    CallAPI responsea = await Services.Service.GetAPIWithoutToken(url);
        //    return responsea.Response;
        //}

        //#endregion Get Source URL
    }
}