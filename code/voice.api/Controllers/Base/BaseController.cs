using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using voice.api.Filters;
using voice.api.Helpers;
using voice.dapper;
using voice.models;
using voice.security;

namespace voice.api.Controllers
{
    [ServiceFilter(typeof(ExceptionFilters))]
    [ApiController]
    [Route(ActionsConsts.ApiVersion)]
    public class BaseController : ControllerBase
    {
        #region Constructor and Object Declaration

        public IDapperRepository DapperRepository { get; set; }

        protected IStringLocalizer<BaseController> Localizer { get; set; }

        protected ICrypto Crypto { get; set; }

        protected ProfileResponse UserEntity { get; set; }

        public dynamic Meta { get; set; }

        public BaseController(
            IDapperRepository DapperRepository,
            IStringLocalizer<BaseController> Localizer,
            ICrypto Crypto)
        {
            this.Localizer = Localizer;
            this.Crypto = Crypto;
            this.DapperRepository = DapperRepository;
        }

        #endregion Constructor and Object Declaration

        #region Get current user's data using Token

        protected bool IsLoguedIn(ClaimsPrincipal User) => User.Identity.IsAuthenticated;

        protected Guid GetUserId(ClaimsPrincipal User)
        {
            var user_id = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault().Value;

            return Guid.Parse(Crypto.Decrypt(user_id));
        }

        protected string GetUserEmail(ClaimsPrincipal User)
        {
            var email = User.Claims.Where(x => x.Type == ClaimTypes.Email)?.FirstOrDefault().Value;

            return Crypto.Decrypt(email);
        }

        protected string GetUserName(ClaimsPrincipal User)
        {
            var username = User.Claims.Where(x => x.Type == ClaimTypes.GivenName)?.FirstOrDefault().Value;

            return Crypto.Decrypt(username);
        }

        protected string GetUniqueId(ClaimsPrincipal User)
        {
            var uniqueId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Jti)?.FirstOrDefault().Value;

            return Crypto.Decrypt(uniqueId);
        }

        protected string GetUserRole(ClaimsPrincipal User)
        {
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            return Crypto.Decrypt(role);
        }

        #endregion Get current user's data using Token

        #region OkResponse

        protected IActionResult OkResponse() => Ok(new { Meta = new { message = Localizer["ok_response_request_performed_successfully"].Value, statusCode = 200 } });

        protected IActionResult OkResponse(string message) => Ok(new
        {
            Meta = new { message = Localizer[message].Value, statusCode = 200 }
        });

        protected IActionResult OkResponse(object data, string message) => Ok(new
        {
            Meta = new { message = Localizer[message].Value, statusCode = 200 },
            data
        });

        protected IActionResult OkResponse(object data) => Ok(new
        {
            data
        });

        protected IActionResult OkResponse(
            IEnumerable<dynamic> details)
        {
            var data = new { details };
            return Ok(new
            {
                Meta = new { message = Localizer["ok_response_request_performed_successfully"].Value, statusCode = 200 },
                data
            });
        }
        #endregion OkResponse

        #region Failed Response

        protected IActionResult ErrorResponse() => throw new ApiException("error_something_went_wrong", 400);

        protected IActionResult ErrorResponse(string message) => throw new ApiException(message, 400);

        protected IActionResult ErrorResponse(string message, int statusCode) => throw new ApiException(message, statusCode);

        protected IActionResult ErrorResponse(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ModelState)
        {
            string message = string.Join("; ", ModelState.SelectMany(x => x.Value.Errors).Select(x => Localizer[x.ErrorMessage].Value));
            throw new ApiException(message, 400);
        }

        protected IActionResult ErrorResponse(IEnumerable<dynamic> data, string message)
        {
            throw new ApiException(Localizer[message].Value, 400, data);
        }

        protected IActionResult ErrorMarkorResponse(object message) => BadRequest(new { message });
        #endregion Failed Response

        #region Validate User

        protected async Task ValidateUserAsync(
            ProfileResponse user = null,
            string role = null)
        {
            if (user == null && HttpContext.User.Identity.IsAuthenticated)
            {
                using var account_help = new AccountHelpers(DapperRepository, Crypto);
                user = await account_help.FindUser(new LoginRequest
                {
                    Role = GetUserRole(User),
                    UserId = GetUserId(User),
                    UniqueId = GetUniqueId(User),
                });
            }

            if (user == null) ErrorResponse("error_access_token_expired", 401);
            //if (!user.EmailConfirmed) ErrorResponse("bad_response_account_not_confirmed");

            if (!user.Active) ErrorResponse("bad_response_account_inactive");

            if (user.Deleted) ErrorResponse("bad_response_account_delete");

            if (!string.IsNullOrEmpty(role) && !user.Role.ToLower().Equals(role.ToLower()))
                ErrorResponse("forbid_error_access");

            this.UserEntity = user;
        }

        #endregion Validate User
    }
}