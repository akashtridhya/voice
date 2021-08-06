using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using voice.api.Helpers;
using voice.dapper;
using voice.models;
using voice.models.Requests.Token.Request;
using voice.models.Requests.UserManagement;
using voice.security;

namespace voice.api.Controllers
{
    [Authorize]
    public class TokenController : BaseController
    {
        #region 1. Object Declarations And Constructor
        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        public TokenController(
          IStringLocalizer<BaseController> Localizer,
          ICrypto Crypto,
          IDapperRepository DapperRepository,
          IOptions<BaseUrlConfigs> BaseUrlConfigsOptions)
          : base(DapperRepository, Localizer, Crypto)
        {
            BaseUrlConfigs = BaseUrlConfigsOptions.Value;
        }
        #endregion 1. Object Declarations And Constructor

        #region 2.  Persist Token
        [Authorize, HttpPost(ActionsConsts.Token.PersistToken)]
        public async Task<IActionResult> PersistTokenAsync([FromBody] TokenPersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            using var helper = new BonusTokenHelpers(DapperRepository);
            await helper.TokenPersist(request);
            return OkResponse();
        }
        #endregion 2.  Persist Token

        #region 3. Retrieve Token
        [Authorize, HttpGet(ActionsConsts.Token.RetrieveToken)]
        public async Task<IActionResult> RetrieveTokenAsync()
        {
            await ValidateUserAsync();
            var user = UserEntity;
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.TokenManagement.TokenList, new {Role = user.Role, UniqueId = GetUniqueId(User), UserId = user.Id } );
            return OkResponse(response);
        }
        #endregion 3. Retrieve Token

        #region 4. Status Token
        [HttpPost(ActionsConsts.Token.StatusToken)]
        public async Task<IActionResult> TokenStatus([FromBody] DeleteRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);
            await ValidateUserAsync();

            using var helper = new BonusTokenHelpers(DapperRepository);
            await helper.TokenStatus(request);
            return OkResponse();
        }
        #endregion 4. Status Token
    }
}