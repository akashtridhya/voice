using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using voice.dapper;
using voice.models;
using voice.security;

namespace voice.api.Controllers
{
    public class DashboardController : BaseController
    {
        #region 1. Object Declarations And Constructor
        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        public DashboardController(
          IStringLocalizer<BaseController> Localizer,
          ICrypto Crypto,
          IDapperRepository DapperRepository,
          IOptions<BaseUrlConfigs> BaseUrlConfigsOptions)
          : base(DapperRepository, Localizer, Crypto)
        {
            BaseUrlConfigs = BaseUrlConfigsOptions.Value;
        }
        #endregion 1. Object Declarations And Constructor

        #region 2. Retrieve Dashboard Data
        [Authorize, HttpGet(ActionsConsts.Dashboard.GetDashboardData)]
        public async Task<IActionResult> RetrieveDashbaordDataAsync()
        {
            await ValidateUserAsync();
            var user = UserEntity;
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.Dashboard.GetDashboardData, new { Role = user.Role, UniqueId = GetUniqueId(User), UserId = user.Id });
            return OkResponse(response);
        }
        #endregion 2. Retrieve Dashboard Data
    }
}