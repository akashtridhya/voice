using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using voice.api.Helpers;
using voice.dapper;
using voice.models;
using voice.models.Requests.Level;
using voice.models.Requests.UserManagement;
using voice.security;
namespace voice.api.Controllers
{
    public class LevelController : BaseController
    {
        #region 1. Object Declarations And Constructor
        private BaseUrlConfigs BaseUrlConfigs { get; set; }
        public LevelController(
          IStringLocalizer<BaseController> Localizer,
          ICrypto Crypto,
          IDapperRepository DapperRepository,
          IOptions<BaseUrlConfigs> BaseUrlConfigsOptions)
          : base(DapperRepository, Localizer, Crypto)
        {
            BaseUrlConfigs = BaseUrlConfigsOptions.Value;
        }
        #endregion 1. Object Declarations And Constructor

        #region 2. Level Persist
        [Authorize, HttpPost(ActionsConsts.LevelManagement.PersistLevel)]
        public async Task<IActionResult> PersistLevelAsync([FromBody] LevelPersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            await ValidateUserAsync();
            var user = UserEntity;

            request.UniqueId = GetUniqueId(User);
            request.UserId = user.Id;
            request.Role = user.Role;

            using var helper = new LevelHelpers(DapperRepository);
            await helper.LevelPersist(request);
            return OkResponse();
        }
        #endregion 2. Level Persist

        #region 3. Get Level
        [HttpGet(ActionsConsts.LevelManagement.RetriveLevel)]
        public async Task<IActionResult> GetLevelAsyncWithout()
        {
            await ValidateUserAsync();
            var user = UserEntity;
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.LevelManagement.LevelSelect, new { Role = user.Role, UniqueId = GetUniqueId(User), UserId = user.Id });
            return OkResponse(response);
        }
        #endregion 3. Get Level

        #region 4. Get Level Mobile
        [HttpGet(ActionsConsts.LevelManagement.RetriveMobileLevel)]
        public async Task<IActionResult> GetLevelAsyncWithoutAuth()
        {
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.LevelManagement.LevelSelect);
            return OkResponse(response);
        }
        #endregion 4. Get Level Mobile

        #region 5. Level Change Status or Delete
        [HttpPost(ActionsConsts.LevelManagement.StatusOrDeleteLevel)]
        public async Task<IActionResult> LevelChangeStatus([FromBody] DeleteRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);
            await ValidateUserAsync();

            using var helper = new LevelHelpers(DapperRepository);
            await helper.LevelStatusUpdateOrDelete(request);
            return OkResponse();
        }
        #endregion 5. Level Change Status or Delete

    }
}