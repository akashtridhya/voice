using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using voice.blazor.Models;
using voice.blazor.Models.Account;
using voice.blazor.Models.Consts;
using voice.blazor.Models.Game.Request;
using voice.blazor.Models.General.Country.Request;

namespace voice.blazor.Helpers
{
    public class GameHelpers : IDisposable
    {
        #region Get Ads
        public static async Task<dynamic> Games(GetGamesListRequest request)
        {
            var url = $"{ApiEndPointConsts.GameManagement.GameList}";
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
        #endregion Get Ads

        #region Status Update Bonus
        [HttpPost]
        internal static async Task<dynamic> StatusGame(StatusGeneralRequest request)
        {
            var url = $"{ApiEndPointConsts.GameManagement.GameStatus}";
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
        #endregion Status Update Bonus

        #region Perist Game
        [HttpPost]
        internal static async Task<dynamic> PersistGame(PersistGameRequest request)
        {
            var url = $"{ApiEndPointConsts.GameManagement.GamePersist}";
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
        #endregion Perist Game

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
        #endregion House Keeping
    }
}