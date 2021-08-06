using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using voice.api.Filters;
using voice.dapper;
using voice.models;
using voice.security;

namespace voice.api.Helpers
{
    public class TokenHelpers : IDisposable
    {
        #region Object Declarations And Constructor

        private IDapperRepository DapperRepository { get; set; }

        private ICrypto Crypto { get; set; }

        public TokenHelpers(
            IDapperRepository DapperRepository,
            ICrypto Crypto)
        {
            this.DapperRepository = DapperRepository;
            this.Crypto = Crypto;
        }

        #endregion Object Declarations And Constructor

        #region Do background process for the Saving, Validating and Updating

        public string GenerateToken(
            string userId,
            string purpose)
        {
            var token = DapperRepository.Find<dynamic>(
                SpConsts.Token.CreateAndValidate,
                new
                {
                    UserId = userId,
                    Purpose = purpose,
                    UniqueId = Guid.NewGuid().ToString()
                });

            return Crypto.Encrypt(JsonConvert.SerializeObject(token));
        }

        public async System.Threading.Tasks.Task<dynamic> ValidateTokenAsync(
            string token,
            string purpose)
        {
            var token_data = JsonConvert.DeserializeObject<dynamic>(Crypto.Decrypt(token));
            var token_obj = await DapperRepository.FindAsync<dynamic>(
                SpConsts.Token.CreateAndValidate,
                new
                {
                    UniqueId = token_data.uniqueId.ToString(),
                    UserId = token_data.userId.ToString(),
                    Purpose = purpose
                });

            if (token_obj != null
                && token_obj.purpose == purpose)
                return token_obj;

            throw new ApiException("error_token_expired", 400);
        }

        public void ExpireToken(
            string UniqueId,
            string UserId)
        {
            DapperRepository.AddOrUpdate(
                SpConsts.Token.Expire,
                new
                {
                    UniqueId,
                    UserId
                });
        }

        #endregion Do background process for the Saving, Validating and Updating

        #region Generate Access Token

        public string GetAccessToken(
            AuthConfigs AuthConfig,
            ProfileResponse profile,
            string UniqueId)
        {
            Claim[] claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sid, Crypto.Encrypt(profile.Id.ToString())),
                new Claim(JwtRegisteredClaimNames.Email, Crypto.Encrypt(profile.Email==null?"":profile.Email)),
                new Claim(JwtRegisteredClaimNames.GivenName, Crypto.Encrypt($"{(profile.Username==null?"":profile.Username)}" ?? "")),
                new Claim(JwtRegisteredClaimNames.Jti, Crypto.Encrypt(UniqueId)),
                new Claim(ClaimTypes.Role, Crypto.Encrypt(profile.Role.ToString()))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConfig.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(AuthConfig.Issuer, AuthConfig.Audiance, expires: DateTime.Now.AddDays(AuthConfig.AccessExpireDays), signingCredentials: creds, claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion Generate Access Token

        #region Generate token for each task

        public string GenerateResetPasswordToken(string userId) => GenerateToken(userId, EmailTypeConst.ResetPassword);

        public string GenerateChangeEmailToken(string userId) => GenerateToken(userId, EmailTypeConst.ChangeEmail);

        public string GenerateConfirmEmailToken(string userId) => GenerateToken(userId, EmailTypeConst.ConfirmEmail);

        public string GenerateAdminInviteToken(string userId) => GenerateToken(userId, EmailTypeConst.AdminInvite);

        #endregion Generate token for each task

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}