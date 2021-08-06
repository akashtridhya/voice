using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using voice.api.Filters;
using voice.api.Helpers;
using voice.api.Shared;
using voice.dapper;
using voice.files;
using voice.logs;
using voice.models;
using voice.models.Requests.Account;
using voice.models.Responses.UserManagement;
using voice.notify;
using voice.security;

namespace voice.api.Controllers
{
    public class AccountController : BaseController
    {
        #region 1. Object Declarations And Constructor
        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        public AccountController(
          IStringLocalizer<BaseController> Localizer,
          ICrypto Crypto,
          IDapperRepository DapperRepository,
          IOptions<BaseUrlConfigs> BaseUrlConfigsOptions)
          : base(DapperRepository, Localizer, Crypto)
        {
            BaseUrlConfigs = BaseUrlConfigsOptions.Value;
        }
        #endregion 1. Object Declarations And Constructor

        #region 2. Login
        /// <summary>
        /// Used for Login to all Users.
        /// </summary>
        /// <param name="request">Provide Email and Password for Login</param>
        /// <returns></returns>
        [HttpPost(ActionsConsts.Account.Login)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, [FromServices] IOptions<BaseUrlConfigs> baseUrlConfigs,
            [FromServices] IOptions<AuthConfigs> authConfigOptions)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            if (String.IsNullOrWhiteSpace(request.FbId))
            {
                if (String.IsNullOrWhiteSpace(request.Username)) return ErrorResponse("bad_response_username_required");
                if (String.IsNullOrWhiteSpace(request.Password)) return ErrorResponse("bad_response_password_required");
            }

            using var helper = new AccountHelpers(DapperRepository, Crypto);

            var uniqueId = helper.FindAdminUniqueId(request.Username);
            var flag = false;

            if (uniqueId != "")
                flag = helper.GetAdminFlag(request.Username);

            if (flag == true && uniqueId != "")
                request.UniqueId = uniqueId.ToString();
            else
                request.UniqueId = Guid.NewGuid().ToString();

            var user = await helper.FindUserForLogin(request);

            if (user == null) throw new ApiException("error_invalid_login");

            if (user.Role == "user" || user.Role == "admin" && flag == false)
                user.Token = new TokenHelpers(DapperRepository, Crypto).GetAccessToken(authConfigOptions.Value, user, request.UniqueId);

            if (user.Role == "admin" && flag == true)
            {
                var Token = helper.FindAdminToken(request.Username);
                user.Token = Token;
            }

            if (user.Role == "admin" && flag == false)
            {
                AdminTokenInsertRequest adminTokenInsertRequest = new AdminTokenInsertRequest();

                adminTokenInsertRequest.UniqueId = uniqueId;
                adminTokenInsertRequest.UserId = user.Id;
                adminTokenInsertRequest.Token = user.Token;
                adminTokenInsertRequest.Flag = true;

                await helper.InsertAdminToken(adminTokenInsertRequest);
            }

            await ValidateUserAsync(user, user.Role);

            if (user != null)
                user.ProfileURL = new GenericHelpers(baseUrlConfigs.Value).BindProfileAttachmentUrl(user.ProfileURL);
            return OkResponse(user, "ok_response_login");
        }
        #endregion 2. Login

        #region 3. Signup 
        /// <summary>
        /// Used for Signup.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(ActionsConsts.Account.Register)]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequest request,
            [FromServices] IMessages messages, [FromServices] IOptions<AuthConfigs> authConfigOptions, [FromServices] IMessages Messages, [FromServices] IOptions<BaseUrlConfigs> baseUrlConfigs)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            if (String.IsNullOrWhiteSpace(request.FbId))
            {
                if (String.IsNullOrWhiteSpace(request.EmailId)) return ErrorResponse("bad_response_invalid_email");
                if (String.IsNullOrWhiteSpace(request.Password)) return ErrorResponse("bad_response_password_required");
                if (String.IsNullOrWhiteSpace(request.Username)) return ErrorResponse("bad_response_username_required");
                if (String.IsNullOrWhiteSpace(request.Role.ToString())) return ErrorResponse("bad_response_role_required");
            }
            if (!String.IsNullOrWhiteSpace(request.FbId))
            {
                Random generator = new Random();
                String r = generator.Next(0, 1000).ToString("D3");
                request.Username = request.FirstName + r;

                if (request.Username == "") return ErrorResponse("bad_response_username_required");
                if (String.IsNullOrWhiteSpace(request.Role.ToString())) return ErrorResponse("bad_response_role_required");
            }
            using var helper = new AccountHelpers(DapperRepository, Crypto);
            var user = await helper.AddUser(request, Localizer, Messages, BaseUrlConfigs);

            if (user.Meta.StatusCode == StatusCodeConsts.Success)
            {
                await ValidateUserAsync(user.Data.Data, user.Data.Data.Role);
                var token = new TokenHelpers(DapperRepository, Crypto).GetAccessToken(authConfigOptions.Value, user.Data.Data, request.UniqueId.ToString());
                user.Data.Data.Token = token;

                user.Data.Data.ProfileURL = new GenericHelpers(baseUrlConfigs.Value).BindProfileAttachmentUrl(user.Data.Data.ProfileURL);
                return OkResponse(user.Data.Data, "ok_response_register");
            }
            return ErrorMarkorResponse(user.Data.Data.error);
        }
        #endregion 3. Sign Up

        #region 4. Forgot Password

        /// <summary>
        /// Used For Forgot Password.
        /// </summary>
        /// <param name="request">Provide Email Id for Validate</param>
        /// <returns></returns>
        [HttpPost(ActionsConsts.Account.ForgotPassword)]
        public async Task<IActionResult> ForgotPasswordAsync(
            [FromBody] ForgotPasswordRequest request,
            [FromServices] IMessages Messages)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            using var helper = new AccountHelpers(DapperRepository, Crypto);

            await helper.ForgotPassword(request.EmailId, Localizer, Messages, BaseUrlConfigs);

            return OkResponse(string.Format(Localizer["ok_response_forgot_password"].Value, request.EmailId));
        }
        #endregion 4. Forgot Password

        #region 5. Reset Password
        /// <summary>
        /// Used For Reset Password By The Link Of Forgot Password Mail
        /// </summary>
        /// <param name="request">Provide valid parameters</param>
        /// <returns></returns>
        [HttpPost(ActionsConsts.Account.ResetPassword)]
        public async Task<IActionResult> ResetPasswordAsync(
            [FromBody] ResetPasswordRequest request,
            [FromServices] IMessages messages)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            using var account_help = new AccountHelpers(DapperRepository, Crypto);

            var userId = await account_help.UpdatePasswordToken(request.Token, request.Password);

            #region Sending email in queue

            var user = await account_help.FindUser(new LoginRequest
            {
                UserId = Guid.Parse(userId),
                Password = request.Password
            });

            await new EmailHelpers(Localizer, messages, BaseUrlConfigs, DapperRepository)
                .SendAccountEmail(user, null, EmailTypeConst.ChangePassword);

            #endregion Sending email in queue

            return OkResponse("ok_response_changed_password");
        }

        #endregion 5. Reset Password

        #region 5. Change Password

        /// <summary>
        /// Used For Change Password.
        /// </summary>
        /// <param name="request">Provide Valid Request Parameters</param>
        /// <returns></returns>
        [Authorize, HttpPost(ActionsConsts.Account.ChangePassword)]
        public async Task<IActionResult> ChangePasswordAsync(
            [FromBody] ChangePasswordRequest request,
            [FromServices] IMessages messages,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            using var account_help = new AccountHelpers(DapperRepository, Crypto);

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            await account_help.ChangePassword(
                request.UserId.ToString(),
                request.Password,
                request.CurrentPassword,
                EmailTypeConst.ChangePassword,
                request.UniqueId
                );

            #region Sending email in queue

            var user = await account_help.FindUser(new LoginRequest
            {
                Username = GetUserName(User),
                Password = request.Password
            });

            new EmailHelpers(Localizer, messages, BaseUrlConfigs, DapperRepository)
                .SendAccountEmail(user, null, EmailTypeConst.ChangePassword);

            #endregion Sending email in queue

            await new NotificationHelpers(Localizer, messages, hubContext).SendWebNotifications(NotificationTypeConsts.ChangePassword, "Your password has been changed successfully", request.UserId.ToString());

            return OkResponse("ok_response_changed_password");
        }

        #endregion 5. Change Password

        #region 6. Get Profile
        [Authorize, HttpGet(ActionsConsts.Account.Profile)]
        public async Task<IActionResult> ProfileAsync(
            [FromServices] IOptions<BaseUrlConfigs> baseUrlConfigs,
            [FromServices] IOptions<AuthConfigs> authConfigOptions)
        {
            await ValidateUserAsync();
            var response = UserEntity;
            if (response != null)
                response.ProfileURL = new GenericHelpers(baseUrlConfigs.Value).BindProfileAttachmentUrl(response.ProfileURL);

            UserLeaderBoardRequest request = new UserLeaderBoardRequest();

            request.UniqueId = GetUniqueId(User);
            request.UserId = response.Id;
            if (request.Location == "Country")
                request.Location = response.CountryCode;
            if (request.Location == "" || request.Location == null)
                request.Location = "All";
            using var helper = new UsersHelpers(DapperRepository);
            IEnumerable<UserLeaderBoardResponse> responseLeaderboard = await helper.UserLeaderBoard(request);
            UserLeaderBoardResponse userPosition = responseLeaderboard.SingleOrDefault(item => item.UserName == response.Username);

            response.UserPosition = userPosition.Position;

            return OkResponse(response, "ok_response_get_profile");
        }
        #endregion 6. Get Profile

        #region 7. Update Profile

        [Authorize, HttpPost(ActionsConsts.Account.UpdateProfile)]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UserUpdateRequest request, [FromServices] IOptions<BaseUrlConfigs> baseUrlConfigs, [FromServices] ILogManager logManager)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);
            if (String.IsNullOrWhiteSpace(request.Id.ToString()))
                request.Id = GetUserId(User);
            using var helper = new AccountHelpers(DapperRepository, Crypto);
            var editResponse = await helper.UpdateProfile(request);

            if (editResponse != null)
            {
                editResponse.ProfileURL = new GenericHelpers(baseUrlConfigs.Value).BindProfileAttachmentUrl(editResponse.ProfileURL);
                if (!String.IsNullOrWhiteSpace(editResponse.FbId)) editResponse.IsFbRegistered = true;
                return OkResponse(editResponse, "ok_response_update_password");
            }
            throw new ApiException("error_bad_update_profile");
        }

        #endregion 7. Update Profile

        #region 8. Upload Profile Picture

        [Authorize, HttpPost(ActionsConsts.Account.UpdateProfilePicture)]
        public async Task<IActionResult> UpdateProfilePictureAsync(
            FileUploadRequest request,
            [FromServices] IUploadManager UploadManager)
        {
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            var profileUrl = await UploadManager.Store(
                request.File,
                Guid.NewGuid().ToString(),
                FolderEnums.profile.ToString());

            if (string.IsNullOrEmpty(profileUrl)) return ErrorResponse();

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            await DapperRepository.AddOrUpdateAsync<dynamic>(
                SpConsts.Account.UpdateProfilePicture,
                new
                {
                    Id = request.UserId,
                    ProfileUrl = profileUrl,
                    UniqueId = request.UniqueId
                });

            return OkResponse();
        }
        #endregion 8. Upload Profile Picture

        #region 9. Remove Profile Picture

        [Authorize, HttpGet(ActionsConsts.Account.DeleteProfilePicture)]
        public async Task<IActionResult> DeleteProfilePictureAsync(
            [FromServices] IUploadManager UploadManager)
        {
            await ValidateUserAsync();

            ////var filename = UserEntity.ProfileUrl.Split("/").LastOrDefault();
            //UploadManager.Delete(filename, FolderEnums.profile.ToString());

            var userId = GetUserId(User);
            var uniqueId = GetUniqueId(User);

            await DapperRepository.AddOrUpdateAsync<dynamic>(
                SpConsts.Account.UpdateProfilePicture,
                new
                {
                    Id = userId,
                    UniqueId = uniqueId
                });

            return OkResponse();
        }

        #endregion 9. Remove Profile Picture

        #region 10. Logout
        [Authorize, HttpGet(ActionsConsts.Account.Logout)]
        public async Task<IActionResult> LogoutAsync()
        {
            BaseValidateRequest baseValidateRequest = new BaseValidateRequest
            {
                UniqueId = GetUniqueId(User),
                UserId = GetUserId(User)
            };

            await DapperRepository.AddOrUpdateAsync<dynamic>(SpConsts.Account.UsersDeviceDelete, baseValidateRequest);

            return OkResponse("ok_response_logout");
        }
        #endregion 10. Logout

        #region 11. Insert User Device
        [Authorize, HttpPost(ActionsConsts.Account.UserDeviceInsert)]
        public async Task<IActionResult> UserDeviceInsertAsync([FromBody] UsersDeviceInsertRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            if (!request.DeviceType.Equals(DeviceTypeEnums.android.ToString())
                && !request.DeviceType.Equals(DeviceTypeEnums.web.ToString())
                && !request.DeviceType.Equals(DeviceTypeEnums.ios.ToString()))
                ErrorResponse("bad_response_invalid_device_type");

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            await DapperRepository.AddOrUpdateAsync<dynamic>(SpConsts.Account.UsersDeviceInsert, request);

            return OkResponse();
        }
        #endregion 11. Insert User Device
    }
}