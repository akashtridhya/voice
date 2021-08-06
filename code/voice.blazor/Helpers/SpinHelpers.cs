using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using voice.blazor.Models;
using voice.blazor.Models.Account;
using voice.blazor.Models.Consts;
using voice.blazor.Models.General.Country.Request;
using voice.blazor.Models.Spin.Request;

namespace voice.blazor.Helpers
{
    public class SpinHelpers : IDisposable
    {
        #region Get Spin
        public static async Task<dynamic> SpinList(string Token = "")
        {
            var url = $"{ApiEndPointConsts.SpinManagement.SpinList}";
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
        #endregion Get Spin

        #region Status Spin
        internal static async Task<dynamic> StatusSpin(StatusGeneralRequest request, string Token = "")
        {
            var url = $"{ApiEndPointConsts.SpinManagement.SpinStatus}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
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
        #endregion Status Spin

        #region Perist Bonus
        internal static async Task<dynamic> PersistSpin(PersistSpinRequest request, string Token = "")
        {
            var url = $"{ApiEndPointConsts.SpinManagement.SpinPersist}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
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
        #endregion Perist Bonus

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
