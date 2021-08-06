using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using voice.api.Helpers;
using voice.dapper;
using voice.models;
using voice.models.Requests.Game;
using voice.models.Requests.UserManagement;
using voice.security;
using static voice.models.Constants.MarkorAPIConsts;

namespace voice.api.Controllers
{
    public class GamesController : BaseController
    {
        #region 1. Object Declarations And Constructor
        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        public GamesController(
          IStringLocalizer<BaseController> Localizer,
          ICrypto Crypto,
          IDapperRepository DapperRepository,
          IOptions<BaseUrlConfigs> BaseUrlConfigsOptions)
          : base(DapperRepository, Localizer, Crypto)
        {
            BaseUrlConfigs = BaseUrlConfigsOptions.Value;
        }
        #endregion 1. Object Declarations And Constructor

        #region 2. Get Game
        [Authorize, HttpPost(ActionsConsts.GameManagement.RetriveGames)]
        public async Task<IActionResult> GetGameAsync([FromBody] GetGameListRequest request, [FromServices] IOptions<BaseUrlConfigs> baseUrlConfigs)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            await ValidateUserAsync();
            var user = UserEntity;
            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);
            request.Role = user.Role;
            dynamic response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.GameManagement.GameSelect,
              new
              {
                  GameCategory = request.GameCategory,
                  Role = request.Role,
                  UniqueId = request.UniqueId,
                  UserId = request.UserId
              });
            for (int i = 0; i < response.Count; i++)
            {
                response[i].GameImageURL = new GenericHelpers(baseUrlConfigs.Value).BindGameAttachmentUrl(response[i].GameImageURL);
                response[i].GameLaunchURL = GetGames.GetGamesURL +  ImportantConsts.AccountSystemTag + "/game/" + response[i].GameTag + "?lang=en&launchType=MobileApp&homeURL=lobby";
            }
            return OkResponse(response);
        }
        #endregion 2. Get Game

        #region 3. Persist Games
        [Authorize, HttpPost(ActionsConsts.GameManagement.PersistGames)]
        public async Task<IActionResult> AddGameAsync([FromBody] PersistGameRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            await ValidateUserAsync();
            var user = UserEntity;

            request.UniqueId = GetUniqueId(User);
            request.UserId = user.Id;
            request.Role = user.Role;

            using var helper = new GamesHelpers(DapperRepository);
            await helper.GamePersist(request);
            return OkResponse();
        }
        #endregion 3. Persist Games

        #region 4. Games Change Status or Delete
        [HttpPost(ActionsConsts.GameManagement.StatusOrDeleteGames)]
        public async Task<IActionResult> GamesChangeStatus([FromBody] DeleteRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);
            await ValidateUserAsync();

            using var helper = new GamesHelpers(DapperRepository);
            await helper.GameStatus(request);
            return OkResponse();
        }
        #endregion 4. Games Change Status or Delete
    }
}