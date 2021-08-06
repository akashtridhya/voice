using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using voice.middleware.Helpers;
using voice.middleware.Models.Consts;
using voice.middleware.Models.Game.Request;
using voice.middleware.Models.Game.Response;
using voice.middleware.Models.General.Country.Request;
using voice.middleware.Models.Generic.Request;

namespace voice.middleware.Controllers
{
    public class GameController : BaseController
    {
        #region Game List
        [Route("game-slots", Name = "SlotGame")]
        public async Task<IActionResult> SlotGame(GetGamesListRequest request)
        {
            request.gameCategory = "slot";
            var Games = await GameHelpers.Games(request);
            if (Games.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return View();
        }

        [Route("game-tables", Name = "TableGame")]
        public async Task<IActionResult> TableGame(GetGamesListRequest request)
        {
            request.gameCategory = "table";
            var Games = await GameHelpers.Games(request);
            if (Games.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return View();
        }

        public async Task<List<GameDetail>> GetSlotGames(GetGamesListRequest request)
        {
            request.gameCategory = "slot";
            var GameList = await GameHelpers.Games(request);
            var response = JsonConvert.DeserializeObject<GameListResponse>(GameList.data);
            for (int i = 0; response.details.Count > i; i++)
            {
                response.details[i].number = i + 1;
            }
            return response.details;
        }

        public async Task<List<GameDetail>> GetTableGames(GetGamesListRequest request)
        {
            request.gameCategory = "table";
            var GameList = await GameHelpers.Games(request);
            var response = JsonConvert.DeserializeObject<GameListResponse>(GameList.data);
            for (int i = 0; response.details.Count > i; i++)
            {
                response.details[i].number = i + 1;
            }
            return response.details;
        }
        #endregion Game List

        [HttpPost]
        public async Task<ActionResult> GameStatusOrDelete([FromBody] StatusGeneralRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);

            var response = await GameHelpers.StatusGame(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }

        [HttpPost]
        public async Task<ActionResult> GamePersist([FromBody] PersistGameRequest request)
        {
            if (request.id == "")
                request.id = null;
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
            var response = await GameHelpers.PersistGame(request);

            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }

        [HttpPost]
        public async Task<ActionResult> GameImage(IFormFile file)
        {
            var fileModel = new ImageUploadRequest()
            {
                File = file,
                Type = "games"
            };

            var response = await GenericHelpers.ImageUpload(fileModel);
            if (response.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponse(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
    }
}