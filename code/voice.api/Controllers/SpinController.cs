using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using voice.api.Helpers;
using voice.dapper;
using voice.models;
using voice.models.Requests.Spin;
using voice.models.Requests.UserManagement;
using voice.security;

namespace voice.api.Controllers
{      
    [Authorize]
    public class SpinController : BaseController
    {
        #region 1. Object Declarations And Constructor
        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        public SpinController(
          IStringLocalizer<BaseController> Localizer,
          ICrypto Crypto,
          IDapperRepository DapperRepository,
          IOptions<BaseUrlConfigs> BaseUrlConfigsOptions)
          : base(DapperRepository, Localizer, Crypto)
        {
            BaseUrlConfigs = BaseUrlConfigsOptions.Value;
        }
        #endregion 1. Object Declarations And Constructor

        #region 2. Spin Persist
        [Authorize, HttpPost(ActionsConsts.SpinManagement.PersistSpin)]
        public async Task<IActionResult> PersistSpinAsync([FromBody] SpinPersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            using var helper = new SpinHelpers(DapperRepository);
            await helper.SpinPersist(request);
            return OkResponse();
        }
        #endregion 2. Spin Persist

        #region 3. Get Spin
        [HttpGet(ActionsConsts.SpinManagement.RetriveSpin)]
        public async Task<IActionResult> GetSpinAsync()
        {
            await ValidateUserAsync();
            var user = UserEntity;
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.SpinManagement.SpinSelect, new { Role = user.Role, UniqueId = GetUniqueId(User), UserId = user.Id });
            return OkResponse(response);
        }
        #endregion 3. Get Spin

        #region 4. Spin Change Status or Delete
        [HttpPost(ActionsConsts.SpinManagement.StatusOrDeleteSpin)]
        public async Task<IActionResult> SpinChangeStatus([FromBody] DeleteRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);
            await ValidateUserAsync();

            using var helper = new SpinHelpers(DapperRepository);
            await helper.SpinStatusUpdateOrDelete(request);
            return OkResponse();
        }   
        #endregion 4. Spin Change Status or Delete

        #region 5. Spin Functionality 
        [HttpGet(ActionsConsts.SpinManagement.SpinValue)]
        public async Task<IActionResult> SpinAsync()
        {
            await ValidateUserAsync();
            var user = UserEntity;
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.SpinManagement.SpinValue, new { Role = user.Role, UniqueId = GetUniqueId(User), UserId = user.Id });
            return OkResponse(response);
        }
        #endregion 5. Spin Functionality

        #region 6. Check SpinValid
        [HttpGet(ActionsConsts.SpinManagement.SpinCheckValid)]
        public async Task<IActionResult> CheckValidSpinAsync()
        {
            await ValidateUserAsync();
            var user = UserEntity;
            var response = await DapperRepository.FindAsync<dynamic>(SpConsts.SpinManagement.SpinCheckValid, new { Role = user.Role, UniqueId = GetUniqueId(User), UserId = user.Id });
            return OkResponse(response);
        }
        #endregion 6. Check SpinValid
    }
}