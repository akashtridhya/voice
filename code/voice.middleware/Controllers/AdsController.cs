using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using voice.middleware.Helpers;
using voice.middleware.Models.Ads.Request;
using voice.middleware.Models.Ads.Response;
using voice.middleware.Models.Consts;
using voice.middleware.Models.General.Country.Request;
using voice.middleware.Models.Generic.Request;

namespace voice.middleware.Controllers
{
    public class AdsController : BaseController
    {
        #region Ads List
        [Route("ads", Name = "Ads")]
        public async Task<IActionResult> Ads()
        {
            var Ads = await AdsHelpers.Ads();
            if (Ads.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return View();
        }

        public async Task<List<AdDetail>> GetAds()
        {
            var AdsList = await AdsHelpers.Ads();
            var response = JsonConvert.DeserializeObject<AdListResponse>(AdsList.data);
            for (int i = 0; response.details.Count > i; i++)
            {
                response.details[i].number = i + 1;
            }
            return response.details;
        }
        #endregion Ads List

        #region Ads Status
        [HttpPost]
        public async Task<ActionResult> AdsStatusOrDelete([FromBody] StatusGeneralRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
                   
            var response = await AdsHelpers.StatusAd(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
        #endregion Ads Status

        #region Persist Ads
        [HttpPost]
        public async Task<ActionResult> AdsPersist([FromBody]PersistAdsRequest request)
        {
            if (request.id == "")
                request.id = null;
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
            var response = await AdsHelpers.PersistAds(request);

            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
        #endregion Persist Ads

        #region Ads Image
        [HttpPost]
        public async Task<ActionResult> AdsImage(IFormFile file)
        {
            var fileModel = new ImageUploadRequest()
            {
                File = file,
                Type = "adImage"
            };

            var response = await GenericHelpers.ImageUpload(fileModel);
            if (response.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponse(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
        #endregion Ads Image
    }
}