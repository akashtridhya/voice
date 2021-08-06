using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using voice.api.Helpers;
using voice.dapper;
using voice.models;
using voice.models.Requests.UserManagement;
using voice.models.Requests.XpPoints;
using voice.security;
namespace voice.api.Controllers
{
    public class XpPointsController : BaseController
    {
        #region 1. Object Declarations And Constructor
        private BaseUrlConfigs BaseUrlConfigs { get; set; }
        public XpPointsController(
          IStringLocalizer<BaseController> Localizer,
          ICrypto Crypto,
          IDapperRepository DapperRepository,
          IOptions<BaseUrlConfigs> BaseUrlConfigsOptions)
          : base(DapperRepository, Localizer, Crypto)
        {
            BaseUrlConfigs = BaseUrlConfigsOptions.Value;
        }
        #endregion 1. Object Declarations And Constructor

        #region 2. XpPoints Persist
        [Authorize, HttpPost(ActionsConsts.XpPointsManagement.PersistXpPoints)]
        public async Task<IActionResult> PersistXpPointsAsync([FromBody] XpPointsPersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            await ValidateUserAsync();
            var user = UserEntity;

            request.UniqueId = GetUniqueId(User);
            request.UserId = user.Id;
            request.Role = user.Role;

            using var helper = new XpPointsHelpers(DapperRepository);
            await helper.XpPointsPersist(request);
            return OkResponse();
        }
        #endregion 2. XpPoints Persist

        #region 3. Get XpPoints
        [HttpGet(ActionsConsts.XpPointsManagement.RetriveXpPoints)]
        public async Task<IActionResult> GetXpPointsAsyncWithout()
        {
            await ValidateUserAsync();
            var user = UserEntity;
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.XpPointsManagement.XpPointsSelect, new { Role = user.Role, UniqueId = GetUniqueId(User), UserId = user.Id });
            return OkResponse(response);
        }
        #endregion 3. Get XpPoints

        #region 4. Get XpPoints Mobile
        [HttpGet(ActionsConsts.XpPointsManagement.RetriveMobileXpPoints)]
        public async Task<IActionResult> GetXpPointsAsyncWithoutAuth()
        {
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.XpPointsManagement.XpPointsSelect);
            return OkResponse(response);
        }
        #endregion 4. Get XpPoints Mobile

        #region 5. XpPoints Change Status or Delete
        [HttpPost(ActionsConsts.XpPointsManagement.StatusOrDeleteXpPoints)]
        public async Task<IActionResult> XpPointsChangeStatus([FromBody] DeleteRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);
            await ValidateUserAsync();

            using var helper = new XpPointsHelpers(DapperRepository);
            await helper.XpPointsStatusUpdateOrDelete(request);
            return OkResponse();
        }
        #endregion 5. XpPoints Change Status or Delete

    }
}