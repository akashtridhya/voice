using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using voice.api.Controllers;
using voice.api.Filters;
using voice.dapper;
using voice.models;
using voice.models.Games.Request.Account;
using voice.models.Requests.Account;
using voice.notify;
using voice.security;

namespace voice.api.Helpers
{
    public class AccountHelpers : IDisposable
    {
        #region Object Declarations And Constructor

        private ICrypto Crypto { get; set; }

        private IDapperRepository DapperRepository { get; set; }
        public AccountHelpers(
            IDapperRepository DapperRepository,
            ICrypto Crypto)
        {
            this.DapperRepository = DapperRepository;
            this.Crypto = Crypto;
        }
        #endregion Object Declarations And Constructor

        #region Add Users
        public async Task<dynamic> AddUser(SignUpRequest request, IStringLocalizer<BaseController> localizer,
          IMessages messages,
          BaseUrlConfigs baseUrlConfigs)
        {
            await DapperRepository.FindAsync<dynamic>(SpConsts.Account.CheckUsers, new { request.Username, request.EmailId, request.MobileNumber });

            #region Call Game Register API
            var Birthdate = (request.BirthDate).ToString("MM-dd-yyyy");
            var requestRegisterGame = new RegisterRequest()
            {
                accountSystemTag = models.Constants.MarkorAPIConsts.ImportantConsts.AccountSystemTag,
                countryCode = request.CountryCode,
                currencyCode = request.CurrencyCode,
                email = request.EmailId,
                firstName = request.FirstName,
                lastName = request.LastName,
                dateOfBirth = Birthdate,
                password = request.Password,
                platformTag = models.Constants.MarkorAPIConsts.ImportantConsts.PlatformTag,
                mobilePhoneNumber = request.MobileNumber,
                termsAndCondsVersion = 0,
                countryCallingCode = "",
                privacyPolicyVersion = 0,
                contactableByCall = true,
                contactableByOthers = true,
                contactableByPost = true,
                contactableBySms = true,
                contactableByUs = true
            };
            var url = $"{models.Constants.MarkorAPIConsts.Account.RegisterSuperLite}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestRegisterGame), Encoding.UTF8, "application/json");
            var responseMarkor = await Services.Service.PostAPIWithoutToken(url, stringContent);
            #endregion Call Game Register API

            if (responseMarkor.error == null)
            {
                string passwordEncrypted = Crypto.EncryptPassword(request.Password);
                request.Password = passwordEncrypted;
                var user = await DapperRepository.FindAsync<ProfileWithTokenResponse>(
                    SpConsts.Account.InsertUsers,
                    new
                    {
                        request.Username,
                        request.FirstName,
                        request.LastName,
                        request.EmailId,
                        request.Password,
                        request.Address1,
                        request.Address2,
                        request.Address3,
                        request.BirthDate,
                        request.CountryCode,
                        request.CurrencyCode,
                        request.Postcode,
                        request.Title,
                        request.MobileNumber,
                        request.LanguageCode,
                        request.HouseNumber,
                        Role = request.Role.ToString(),
                        request.UserId,
                        request.FbId
                    });
                if (user != null)
                {
                    await new EmailHelpers(localizer, messages, baseUrlConfigs, DapperRepository)
                         .SendAccountEmail(new ProfileResponse { Id = user.Id, Username = user.Username, Email = user.Email }, "", EmailTypeConst.RegisterUser.ToString());
                }
                if (!String.IsNullOrWhiteSpace(user.FbId)) user.IsFbRegistered = true;
                return new CustomResult(user, 200);
            }
            else
                return new CustomResult(responseMarkor, 400);
        }
        #endregion Add Users

        #region Find Users
        public async Task<ProfileResponse> FindUser(LoginRequest request)
        {
            var profileResponse = await DapperRepository.FindAsync<ProfileResponse>(
                SpConsts.Account.SelectProfile,
                new
                {
                    request.Username,
                    request.UserId,
                    request.Role,
                    request.UniqueId,
                    Password = Crypto.EncryptPassword(request.Password)
                });

            if (profileResponse != null)
                if (!String.IsNullOrWhiteSpace(profileResponse.FbId)) profileResponse.IsFbRegistered = true;
            return profileResponse;
        }

        public async Task<ProfileWithTokenResponse> FindUserForLogin(LoginRequest request)
        {
            var profileResponse = await DapperRepository.FindAsync<ProfileWithTokenResponse>(
                SpConsts.Account.SelectProfile,
                new
                {
                    request.Username,
                    request.UniqueId,
                    request.FbId,
                    Password = Crypto.EncryptPassword(request.Password)
                });

            #region Game Login
            var loginRequest = new GameLoginRequest()
            {
                LoginAccountSystemTag = models.Constants.MarkorAPIConsts.ImportantConsts.AccountSystemTag,
                LoginPassword = request.Password,
                LoginPlatformTag = models.Constants.MarkorAPIConsts.ImportantConsts.PlatformTag,
                LoginPrinciple = profileResponse.Email
            };

            var url = $"{models.Constants.MarkorAPIConsts.Account.Login}";

            var stringContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
            var responseLoginMarkor = await Services.Service.PostAPIWithToken(url, stringContent);
            #endregion Game Login

            profileResponse.SessionCorrelationId = responseLoginMarkor.SessionCorrelationId;
            profileResponse.UserCorrelationId = responseLoginMarkor.UserCorrelationId;

            if (!String.IsNullOrWhiteSpace(profileResponse.FbId)) profileResponse.IsFbRegistered = true;
            return profileResponse;
        }
        #endregion Find Users

        #region Admin Multiple Login
        #region Find Admin UniqueId
        public string FindAdminUniqueId(string request)
        {
            var response = DapperRepository.Find<dynamic>(SpConsts.Account.CheckForAdmin, new { Username = request });
            return response.UniqueId.ToString();
        }
        #endregion Find Admin UniqueId

        #region Find Admin Token
        public string FindAdminToken(string request)
        {
            var response = DapperRepository.Find<dynamic>(SpConsts.Account.CheckForAdmin, new { Username = request });
            return response.Token;
        }
        #endregion Find Admin Token

        #region Find Admin Flag
        public bool GetAdminFlag(string request)
        {
            var response = DapperRepository.Find<dynamic>(SpConsts.Account.CheckForAdmin, new { Username = request });
            return response.Flag;
        }
        #endregion Find Admin Flag 
        #endregion

        #region Update Profile
        public async Task<ProfileResponse> UpdateProfile(UserUpdateRequest request)
        {
            var editResposnse = await DapperRepository.FindAsync<ProfileResponse>(SpConsts.Account.UpdateUser, new
            {
                request.Id,
                request.Username,
                request.FirstName,
                request.LastName,
                request.Email,
                request.BirthDate,
                request.HouseNumber,
                request.Postcode,
                request.Address1,
                request.Address2,
                request.Address3,
                request.UserId,
                request.UniqueId
            });
            return editResposnse;
        }
        #endregion

        #region Forgot Password
        public async Task ForgotPassword(string email,
          IStringLocalizer<BaseController> localizer,
          IMessages messages,
          BaseUrlConfigs baseUrlConfigs)
        {
            var result = await DapperRepository.FindAsync<dynamic>(SpConsts.Account.ForgotPassword, new { EmailId = email });
            if (result != null)
            {
                string token = new TokenHelpers(DapperRepository, Crypto).GenerateResetPasswordToken(result.Id.ToString());
                var Link = string.Format(baseUrlConfigs.ResetPassword, token);

                await new EmailHelpers(localizer, messages, baseUrlConfigs, DapperRepository)
                    .SendAccountEmail(new ProfileResponse { Id = result.Id, Username = result.Username, Email = result.Email }, Link, EmailTypeConst.ResetPassword.ToString());
            }
        }
        #endregion Forgot Password

        #region Update Password

        public async Task<string> UpdatePasswordToken(
           string token,
           string password)
        {
            return await UpdatePassword(token, password, EmailTypeConst.ResetPassword);
        }

        public async Task<string> UpdatePassword(
         string token,
         string password,
         string purpose)
        {
            using var token_help = new TokenHelpers(DapperRepository, Crypto);

            var data = await token_help.ValidateTokenAsync(token, purpose);
            await ChangePassword(data.userId.ToString(), password, password, purpose);
            token_help.ExpireToken(data.uniqueId.ToString(), data.userId.ToString());

            return data.userId.ToString();
        }

        public async Task ChangePassword(
          string userId,
          string password,
          string currentPassword,
          string purpose,
          string UniqueId = null)
        {
            await DapperRepository.AddOrUpdateAsync(
                SpConsts.Account.SetPassword,
                new
                {
                    UserId = userId,
                    Password = Crypto.EncryptPassword(password),
                    CurrentPassword = Crypto.EncryptPassword(currentPassword),
                    Purpose = purpose,
                    UniqueId = UniqueId
                });
        }

        #endregion Update Password

        public async Task InsertAdminToken(AdminTokenInsertRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.Account.AdminTokenInsert, request);
        }

        #region Logout

        public async Task LogoutAsync(
            string UniqueId,
            string UserId)
        {
            await DapperRepository.AddOrUpdateAsync(
                SpConsts.Token.Expire,
                new
                {
                    UniqueId,
                    UserId
                });
        }

        #endregion Logout

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}