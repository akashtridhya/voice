using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using voice.middleware.Helpers;
using voice.middleware.Models.Consts;
using voice.middleware.Models.General.Country.Request;
using voice.middleware.Models.XpPoints.Request;
using voice.middleware.Models.XpPoints.Response;

namespace voice.middleware.Controllers
{
    [Authorize]
    public class XpPointsController : BaseController
    {
        #region XpPoints
        [Route("XpPoints", Name = "XpPoints")]
        public async Task<IActionResult> XpPoints()
        {
            var XpPoints = await XpPointsHelpers.XpPoints();
            if (XpPoints.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return View();
        }

        public async Task<List<xpPointsDetail>> GetXpPoints()
        {
            var XpPointsList = await XpPointsHelpers.XpPoints();
            var response = JsonConvert.DeserializeObject<XpPointsListResponse>(XpPointsList.data);
            for (int i = 0; response.details.Count > i; i++)
            {
                response.details[i].number = i + 1;
            }
            return response.details;
        }

        [HttpPost]
        public async Task<ActionResult> XpPointsPersist([FromBody] XpPointsPersistRequest request)
        {
            if (request.id == "" || request.id == null)
                request.id = null;
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
            var response = await XpPointsHelpers.PersistXpPoints(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }

        [HttpPost]
        public async Task<ActionResult> XpPointsStatusOrDelete([FromBody] StatusGeneralRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);

            var response = await XpPointsHelpers.StatusXpPoints(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
        #endregion XpPoints
    }
}