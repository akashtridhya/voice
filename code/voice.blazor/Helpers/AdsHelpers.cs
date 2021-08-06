using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using voice.blazor.Models;
using voice.blazor.Models.Account;
using voice.blazor.Models.Consts;
using voice.blazor.Models.General.Country.Request;

namespace voice.blazor.Helpers
{
    public class AdsHelpers : IDisposable
    {
        #region Get Ads
        public static async Task<dynamic> Ads()
        {
            var url = $"{ApiEndPointConsts.AdsManagement.AdsList}";
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
        #endregion Get Ads

        #region Status Ad
        internal static async Task<dynamic> StatusAd(StatusGeneralRequest request)
        {
            var url = $"{ApiEndPointConsts.AdsManagement.AdsStatus}";
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
        #endregion Status Ad

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