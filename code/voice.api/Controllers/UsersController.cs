using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using voice.api.Helpers;
using voice.dapper;
using voice.models;
using voice.models.Requests.UserManagement;
using voice.models.Responses.UserManagement;
using voice.notify;
using voice.security;

namespace voice.api.Controllers
{
    public class UsersController : BaseController
    {
        #region 1. Object Declarations And Constructor 
        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        private IMessages Messages { get; set; }

        public UsersController(
          IStringLocalizer<BaseController> Localizer,
          ICrypto Crypto,
          IMessages Messages,
          IDapperRepository DapperRepository,
          IOptions<BaseUrlConfigs> BaseUrlConfigsOptions)
          : base(DapperRepository, Localizer, Crypto)
        {
            BaseUrlConfigs = BaseUrlConfigsOptions.Value;
            this.Messages = Messages;
        }
        #endregion 1. Object Declarations And Constructor

        #region 2. Users List
        [Authorize, HttpGet(ActionsConsts.UserManagement.RetrieveUsers)]
        public async Task<IActionResult> UsersListAsync()
        {
            BaseValidateRequest baseValidateRequest = new BaseValidateRequest
            {
                UniqueId = GetUniqueId(User),
                UserId = GetUserId(User)
            };
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.UserManagement.UsersList, baseValidateRequest);
            return OkResponse(response);
        }
        #endregion 2. Users List

        #region 3. Add Admin
        [HttpPost(ActionsConsts.UserManagement.AdminInsert)]
        public async Task<IActionResult> AddAdminAsync([FromBody] AdminInsertRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            request.Role = RoleEnums.admin;
            request.Password = "Abc@12345";
            request.UserName = request.EmailId;

            using var helper = new UsersHelpers(DapperRepository);
            await helper.AdminInsert(request);

            if (!string.IsNullOrEmpty(request.EmailId))
            {
                new EmailHelpers(Localizer, Messages, BaseUrlConfigs, DapperRepository)
                                    .SendCustomerRegistrationEmail(request.EmailId, request.UserName);
            }
            return OkResponse();
        }
        #endregion 3. Add Admin


        #region 3. Users Change Status or Delete
        [HttpPost(ActionsConsts.UserManagement.StatusOrDeleteUser)]
        public async Task<IActionResult> ProfileChangeStatus([FromBody] DeleteRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);
            await ValidateUserAsync();

            using var helper = new UsersHelpers(DapperRepository);
            await helper.ProfileStatusUpdateOrDelete(request);
            return OkResponse();
        }
        #endregion 3. Users Change Status or Delete

        #region 4. Users Active Time Insert
        [Authorize, HttpPost(ActionsConsts.UserManagement.InsertUserActiveTime)]
        public async Task<IActionResult> InsertUserActiveTimeAsync([FromBody] UserActiveTimeInsertRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            await ValidateUserAsync();
            var users = UserEntity;

            request.Role = users.Role;
            request.UniqueId = GetUniqueId(User);
            request.UserId = users.Id;

            using var helper = new UsersHelpers(DapperRepository);
            await helper.UserActiveTimeInsert(request);
            return OkResponse();
        }
        #endregion 4. Users Active Time Insert

        #region 5. Get Users Active Time
        [Authorize, HttpPost(ActionsConsts.UserManagement.GetUserActiveTime)]
        public async Task<IActionResult> GetUserActiveTimeAsync([FromBody] UsersSelectActiveTimeRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            await ValidateUserAsync();
            var users = UserEntity;

            request.Role = users.Role;
            request.UniqueId = GetUniqueId(User);
            request.UserId = users.Id;

            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.UserManagement.UserActiveTime, request);

            return OkResponse(response);
        }
        #endregion 5. Get Users Active Time

        #region 3. Users Persist Balance
        [HttpPost(ActionsConsts.UserManagement.PersistUserBalance)]
        public async Task<IActionResult> PersistUserAsync([FromBody] UserBalancePersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);
            await ValidateUserAsync();

            var users = UserEntity;
            request.UserId = users.Id;

            using var helper = new UsersHelpers(DapperRepository);
            await helper.UserBalancePersist(request);
            return OkResponse();
        }
        #endregion 3. Users Persist Balance

        #region 7. User Leader Board
        [Authorize, HttpPost(ActionsConsts.UserManagement.UserLeaderBoard)]
        public async Task<IActionResult> UserLeaderBoardAsync([FromBody] UserLeaderBoardRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            await ValidateUserAsync();
            var users = UserEntity;

            request.UniqueId = GetUniqueId(User);
            request.UserId = users.Id;

            if (request.Location == "Country")
                request.Location = users.CountryCode;
            if (request.Location == "" || request.Location == null)
                request.Location = "All";

            using var helper = new UsersHelpers(DapperRepository);
            IEnumerable<UserLeaderBoardResponse> response = await helper.UserLeaderBoard(request);
            UserLeaderBoardResponse user = response.SingleOrDefault(item => item.UserName == users.Username);
            var responseFinal = new demo()
            {
                details = response,
                user = new User()
                {
                    Position = user.Position,
                    UserName = user.UserName,
                    XpPoints = user.XpPoints
                }
            };
            return OkResponse(responseFinal);
        }

        #endregion 7. User Leader Board

        #region 8. Users Persist Time
        [Authorize, HttpPost(ActionsConsts.UserManagement.PersistUserXpTime)]
        public async Task<IActionResult> PersistUserTimeAsync([FromBody] UserXpTimePersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            using var helper = new UsersHelpers(DapperRepository);
            await helper.UserXpTimePersist(request);
            return OkResponse();
        }
        #endregion 8. Users Persist XpPoints
    }
}