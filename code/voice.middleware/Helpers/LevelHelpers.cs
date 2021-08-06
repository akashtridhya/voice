using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using voice.middleware.Models;
using voice.middleware.Models.Account;
using voice.middleware.Models.Consts;
using voice.middleware.Models.General.Country.Request;
using voice.middleware.Models.Level.Request;

namespace voice.middleware.Helpers
{
    public class LevelHelpers : IDisposable
    {
        #region Level
        public static async Task<dynamic> Level()
        {
            var url = $"{ApiEndPointConsts.LevelManagement.LevelRetrive}";
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

        internal static async Task<dynamic> PersistLevel(LevelPersistRequest request)
        {
            var url = $"{ApiEndPointConsts.LevelManagement.LevelPersist}";
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

        internal static async Task<dynamic> StatusLevel(StatusGeneralRequest request)
        {
            var url = $"{ApiEndPointConsts.LevelManagement.LevelStatusOrDelete}";
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
        #endregion Level

        #region Dispose
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