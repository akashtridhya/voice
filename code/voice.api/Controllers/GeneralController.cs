using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using voice.api.Helpers;
using voice.dapper;
using voice.models;
using voice.models.Requests.General;
using voice.models.Requests.UserManagement;
using voice.models.Responses.General.Country;
using voice.security;
namespace voice.api.Controllers
{
    public class GeneralController : BaseController
    {
        #region 1. Object Declarations And Constructor
        private BaseUrlConfigs BaseUrlConfigs { get; set; }
        public GeneralController(
          IStringLocalizer<BaseController> Localizer,
          ICrypto Crypto,
          IDapperRepository DapperRepository,
          IOptions<BaseUrlConfigs> BaseUrlConfigsOptions)
          : base(DapperRepository, Localizer, Crypto)
        {
            BaseUrlConfigs = BaseUrlConfigsOptions.Value;
        }
        #endregion 1. Object Declarations And Constructor

        #region 2. Terms and Conditions
        #region 2.1 Get Terms and Conditions
        [Authorize, HttpGet(ActionsConsts.GeneralManagement.RetriveTermsAndConditions)]
        public async Task<IActionResult> GetTermsAndConditionAsync()
        {
            await ValidateUserAsync();
            BaseValidateRequest baseValidateRequest = new BaseValidateRequest
            {
                UniqueId = GetUniqueId(User),
                UserId = GetUserId(User)
            };
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.GeneralManagement.TermsAndConditionsSelect, baseValidateRequest);
            return OkResponse(response);
        }
        #endregion 2.1 Get Terms and Conditions

        #region 2.2 Get Terms and Conditions
        [HttpGet(ActionsConsts.GeneralManagement.RetriveMobileTermsAndConditions)]
        public async Task<IActionResult> GetTermsAndConditionAsyncWithoutAuth()
        {
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.GeneralManagement.TermsAndConditionsSelect);
            return OkResponse(response);
        }
        #endregion 2.2 Get Terms and Conditions

        #region 2.3 Persist Terms and Conditions
        [Authorize, HttpPost(ActionsConsts.GeneralManagement.PersistTermsAndConditions)]
        public async Task<IActionResult> PersistTermsAndConditonAsync([FromBody] TermsAndConditionPersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            using var helper = new GeneralHelpers(DapperRepository);
            await helper.TermsAndConditionsPersist(request);
            return OkResponse();
        }
        #endregion 2.3 Persist Terms and Conditions
        #endregion 2. Terms and Conditions

        #region 3. Privacy Policy
        #region 3.1 Get Privacy Policy
        [Authorize, HttpGet(ActionsConsts.GeneralManagement.RetrivePrivacyPolicy)]
        public async Task<IActionResult> GetPrivacyPolicyAsync()
        {
            await ValidateUserAsync();
            BaseValidateRequest baseValidateRequest = new BaseValidateRequest
            {
                UniqueId = GetUniqueId(User),
                UserId = GetUserId(User)
            };
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.GeneralManagement.PrivacyPolicySelect, baseValidateRequest);
            return OkResponse(response);
        }
        #endregion 3.1 Get Privacy Policy

        #region 3.2 Get Privacy Policy
        [HttpGet(ActionsConsts.GeneralManagement.RetriveMobilePrivacyPolicy)]
        public async Task<IActionResult> GetPrivacyPolicyAsyncWithoutAuth()
        {
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.GeneralManagement.PrivacyPolicySelect);
            return OkResponse(response);
        }
        #endregion 3.2 Get Privacy Policy

        #region 3.3 Persist Privacy Policy
        [Authorize, HttpPost(ActionsConsts.GeneralManagement.PersistPrivacyPolicy)]
        public async Task<IActionResult> PersistPrivacyPolicyAsync([FromBody] PrivacyPolicyPersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            using var helper = new GeneralHelpers(DapperRepository);
            await helper.PrivacyPolicyPersist(request);
            return OkResponse();
        }
        #endregion 3.3 Persist Privacy Policy
        #endregion 3. Privacy Policy

        #region 4. How To Play
        #region 4.1 Get How To Play
        [Authorize, HttpGet(ActionsConsts.GeneralManagement.RetriveHowToPlay)]
        public async Task<IActionResult> GetHowToPlayAsync()
        {
            await ValidateUserAsync();
            BaseValidateRequest baseValidateRequest = new BaseValidateRequest
            {
                UniqueId = GetUniqueId(User),
                UserId = GetUserId(User)
            };
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.GeneralManagement.HowToPlaySelect, baseValidateRequest);
            return OkResponse(response);
        }
        #endregion 4.1 Get How To Play

        #region 4.2 Get How To Play
        [HttpGet(ActionsConsts.GeneralManagement.RetriveMobileHowToPlay)]
        public async Task<IActionResult> GetHowToPlayAsyncWithoutAuth()
        {
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.GeneralManagement.HowToPlaySelect);
            return OkResponse(response);
        }
        #endregion 4.2 Get How To Play

        #region 4.3 Persist How To Play
        [Authorize, HttpPost(ActionsConsts.GeneralManagement.PersistHowToPlay)]
        public async Task<IActionResult> PersistHowToPlayAsync([FromBody] HowToPlayPersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            using var helper = new GeneralHelpers(DapperRepository);
            await helper.HowToPlayPersist(request);
            return OkResponse();
        }
        #endregion 4.3 Persist How To Play
        #endregion 4. How To Play

        #region 5. DropDowns
        #region 5.1 Country
        #region 5.1.1 Country Persist
        [Authorize, HttpPost(ActionsConsts.GeneralManagement.PersistCountry)]
        public async Task<IActionResult> PersistCountryAsync([FromBody] CountryPersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            using var helper = new GeneralHelpers(DapperRepository);
            await helper.CountryPersist(request);
            return OkResponse();
        }
        #endregion 5.1.1 Country Persist

        #region 5.1.2 Get Country
        [HttpGet(ActionsConsts.GeneralManagement.RetriveCountry)]
        public async Task<IActionResult> GetCountryAsync()
        {
            await ValidateUserAsync();
            var user = UserEntity;
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.GeneralManagement.CountrySelect, new { Role = user.Role, UniqueId = GetUniqueId(User), UserId = user.Id });
            return OkResponse(response);
        }
        #endregion 5.1.2 Get Country

        #region 5.1.3 Get Country Mobile
        [HttpGet(ActionsConsts.GeneralManagement.RetriveMobileCountry)]
        public async Task<IActionResult> GetCountryAsyncWithoutAuth()
        {
            var response = await DapperRepository.GetTableAsync<CountryResponse>(SpConsts.GeneralManagement.CountrySelect);
            foreach (var res in response)
            {
                if (res.CurrencyJson != null)
                {
                    List<CurrencyDetail> cur = JsonConvert.DeserializeObject<List<CurrencyDetail>>(res.CurrencyJson);
                    res.CurrencyNameCsv = cur;
                }
                if (res.LanguageJson != null)
                {
                    List<LanguageDetail> lan = JsonConvert.DeserializeObject<List<LanguageDetail>>(res.LanguageJson);
                    res.LanguageNameCsv = lan;
                }
            }
            return OkResponse(response);
        }
        #endregion 5.1.3 Get Country Mobile

        #region 5.1.4 Country Change Status or Delete
        [HttpPost(ActionsConsts.GeneralManagement.StatusOrDeleteCountry)]
        public async Task<IActionResult> CountryChangeStatus([FromBody] DeleteRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);
            await ValidateUserAsync();

            using var helper = new GeneralHelpers(DapperRepository);
            await helper.CountryStatusUpdateOrDelete(request);
            return OkResponse();
        }
        #endregion 5.1.4 Country Change Status or Delete
        #endregion 5.1 Country

        #region 5.2 Language
        #region 5.2.1 Language Persist
        [Authorize, HttpPost(ActionsConsts.GeneralManagement.PersistLanguage)]
        public async Task<IActionResult> PersistLanguageAsync([FromBody] LanguagePersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            using var helper = new GeneralHelpers(DapperRepository);
            await helper.LanguagePersist(request);
            return OkResponse();
        }
        #endregion 5.2.1 Language Persist

        #region 5.2.2 Get Language
        [HttpGet(ActionsConsts.GeneralManagement.RetriveLanguage)]
        public async Task<IActionResult> GetLanguageAsync()
        {
            await ValidateUserAsync();
            var user = UserEntity;
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.GeneralManagement.LanguageSelect, new { Role = user.Role, UniqueId = GetUniqueId(User), UserId = user.Id });
            return OkResponse(response);
        }
        #endregion 5.2.2 Get Language

        #region 5.2.3 Get Language Mobile
        [HttpGet(ActionsConsts.GeneralManagement.RetriveMobileLanguage)]
        public async Task<IActionResult> GetLanguageAsyncWithoutAuth()
        {
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.GeneralManagement.LanguageSelect);
            return OkResponse(response);
        }
        #endregion 5.2.3 Get Language Mobile

        #region 5.2.4 Language Change Status or Delete
        [HttpPost(ActionsConsts.GeneralManagement.StatusOrDeleteLanguage)]
        public async Task<IActionResult> LanguageChangeStatus([FromBody] DeleteRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);
            await ValidateUserAsync();

            using var helper = new GeneralHelpers(DapperRepository);
            await helper.LanguageStatusUpdateOrDelete(request);
            return OkResponse();
        }
        #endregion 5.2.4 Language Change Status or Delete
        #endregion 5.2 Language

        #region 5.3 Currency
        #region 5.3.1 Currency Persist
        [Authorize, HttpPost(ActionsConsts.GeneralManagement.PersistCurrency)]
        public async Task<IActionResult> PersistCurrencyAsync([FromBody] CurrencyPersistRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            request.UniqueId = GetUniqueId(User);
            request.UserId = GetUserId(User);

            using var helper = new GeneralHelpers(DapperRepository);
            await helper.CurrencyPersist(request);
            return OkResponse();
        }
        #endregion 5.3.1 Currency Persist

        #region 5.3.2 Get Currency
        [HttpGet(ActionsConsts.GeneralManagement.RetriveCurrency)]
        public async Task<IActionResult> GetCurrencyAsyncWithout()
        {
            await ValidateUserAsync();
            var user = UserEntity;
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.GeneralManagement.CurrencySelect, new { Role = user.Role, UniqueId = GetUniqueId(User), UserId = user.Id });
            return OkResponse(response);
        }
        #endregion 5.3.2 Get Currency

        #region 5.3.3 Get Currency Mobile
        [HttpGet(ActionsConsts.GeneralManagement.RetriveMobileCurrency)]
        public async Task<IActionResult> GetCurrencyAsyncWithoutAuth()
        {
            var response = await DapperRepository.GetTableAsync<dynamic>(SpConsts.GeneralManagement.CurrencySelect);
            return OkResponse(response);
        }
        #endregion 5.3.3 Get Currency Mobile

        #region 5.3.4 Currency Change Status or Delete
        [HttpPost(ActionsConsts.GeneralManagement.StatusOrDeleteCurrency)]
        public async Task<IActionResult> CurrencyChangeStatus([FromBody] DeleteRequest request)
        {
            if (request == null) return ErrorResponse("error_empty_request");
            if (!ModelState.IsValid) return ErrorResponse(ModelState);
            await ValidateUserAsync();

            using var helper = new GeneralHelpers(DapperRepository);
            await helper.CurrencyStatusUpdateOrDelete(request);
            return OkResponse();
        }
        #endregion 5.3.4 Currency Change Status or Delete
        #endregion 5.3 Currency

        #endregion 5. DropDowns
    }
}