using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using voice.api.Helpers;
using voice.dapper;
using voice.models;
using voice.models.Requests.Ads.Request;
using voice.models.Requests.UserManagement;
using voice.security;

namespace voice.api.Controllers
{
    public class AdsController : BaseController
    {
        #region 1. Object Declarations And Constructor
        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        public AdsController(
          IStringLocalizer<BaseController> Localizer,
          ICrypto Crypto,
          IDapperRepository DapperRepository,
          IOptions<BaseUrlConfigs> BaseUrlConfigsOptions)
          : base(DapperRepository, Localizer, Crypto)
        {
            BaseUrlConfigs = BaseUrlConfigsOptions.Value;
        }
        #endregion 1. Object Declarations And Constructor

        #region 2. Ads Persist
        [Authorize, HttpPost(ActionsConsts.AdsManagement.PersistAds)]
        public async Task<IActionResult> PersistCountryAsync([FromBody] AdsPersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            using var helper = new AdsHelpers(DapperRepository);
            await helper.AdsPersist(request);
            return OkResponse();
        }
        #endregion 2. Ads Persist

        #region 3. Get Ads
        [Authorize]
        [HttpGet(ActionsConsts.AdsManagement.RetriveAds)]
        public async Task<IActionResult> GetCountryAsync([FromServices] IOptions<BaseUrlConfigs> baseUrlConfigs)
        {
            await ValidateUserAsync();
            var user = UserEntity;
            dynamic response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.AdsManagement.AdsSelect, new { Role = user.Role, UniqueId = GetUniqueId(User), UserId = user.Id });
            for (int i = 0; i < response.Count; i++)
            {
                response[i].AdImageURL = new GenericHelpers(baseUrlConfigs.Value).BindAdAttachmentUrl(response[i].AdImageURL);
            }
            return OkResponse(response);
        }
        #endregion 3. Get Ads

        #region 3.1 Get Ads Mobile
        [HttpGet(ActionsConsts.AdsManagement.RetriveMobileAds)]
        public async Task<IActionResult> GetAdsAsyncWithoutAuth([FromServices] IOptions<BaseUrlConfigs> baseUrlConfigs)
        {
            dynamic response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.AdsManagement.AdsSelect);
            for (int i = 0; i < response.Count; i++)
            {
                response[i].AdImageURL = new GenericHelpers(baseUrlConfigs.Value).BindAdAttachmentUrl(response[i].AdImageURL);
            }
            return OkResponse(response);
        }
        #endregion 5.1.3 Get Ads Mobile

        #region 4. Ads Change Status or Delete
        [Authorize]
        [HttpPost(ActionsConsts.AdsManagement.StatusOrDeleteAds)]
        public async Task<IActionResult> CountryChangeStatus([FromBody] DeleteRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);
            await ValidateUserAsync();

            using var helper = new AdsHelpers(DapperRepository);
            await helper.AdsStatusUpdateOrDelete(request);
            return OkResponse();
        }
        #endregion 4. Country Change Status or Delete
    }
}