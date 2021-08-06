using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using voice.middleware.Helpers;
using voice.middleware.Models.Consts;
using voice.middleware.Models.General.Country.Request;
using voice.middleware.Models.Level.Request;
using voice.middleware.Models.Level.Response;
using voice.middleware.Models.XpPoints.Request;
using voice.middleware.Models.XpPoints.Response;

namespace voice.middleware.Controllers
{
    [Authorize]
    public class LevelController : BaseController
    {
        #region Level
        [Route("Level", Name = "Level")]
        public async Task<IActionResult> Level()
        {
            var Level = await LevelHelpers.Level();
            if (Level.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return View();
        }

        public async Task<List<LevelDetail>> GetLevel()
        {
            var LevelList = await LevelHelpers.Level();
            var response = JsonConvert.DeserializeObject<LevelListResponse>(LevelList.data);
            for (int i = 0; response.details.Count > i; i++)
            {
                response.details[i].number = i + 1;
            }
            return response.details;
        }

        [HttpPost]
        public async Task<ActionResult> LevelPersist([FromBody] LevelPersistRequest request)
        {
            if (request.id == "" || request.id == null)
                request.id = null;
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
            var response = await LevelHelpers.PersistLevel(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }

        [HttpPost]
        public async Task<ActionResult> LevelStatusOrDelete([FromBody] StatusGeneralRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);

            var response = await LevelHelpers.StatusLevel(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
        #endregion Level
    }
}