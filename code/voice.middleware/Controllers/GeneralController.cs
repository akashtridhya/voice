using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using voice.middleware.Helpers;
using voice.middleware.Models.Consts;
using voice.middleware.Models.General.Country.Request;
using voice.middleware.Models.General.Country.Response;
using voice.middleware.Models.General.Currency.Request;
using voice.middleware.Models.General.Currency.Response;
using voice.middleware.Models.General.HowToPlay.Request;
using voice.middleware.Models.General.HowToPlay.Response;
using voice.middleware.Models.General.Language.Request;
using voice.middleware.Models.General.Language.Response;
using voice.middleware.Models.General.PrivacyPolicy.Request;
using voice.middleware.Models.General.PrivacyPolicy.Response;
using voice.middleware.Models.General.TermsAndConditions.Request;
using voice.middleware.Models.General.TermsAndConditions.Response;

namespace voice.middleware.Controllers
{
    [Authorize]
    public class GeneralController : BaseController
    {

        #region Terms And Conditions
        [Route("terms-and-conditions", Name = "TermsAndConditions")]
        public async Task<IActionResult> TermsAndConditions()
        {
            object response = null;
            var termsAndConditions = await GeneralHelpers.GetTermsAndConditions();
            if (termsAndConditions.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            if (termsAndConditions.meta.statusCode != StatusCodeConsts.UnAuthorized)
                response = JsonConvert.DeserializeObject<TermsAndConditionsResponse>(termsAndConditions.data);
            return View(response);
        }

        public async Task<ActionResult> EditTermsAndConditions([FromBody] EditTermsAndConditionRequest request)
        {
            var response = await GeneralHelpers.EditTermsAndCondition(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            return JsonSuccessResponse(response.meta.message);
        }
        #endregion Terms And Conditions

        #region Privacy Policy
        [Route("privacy-policy", Name = "PrivacyPolicy")]
        public async Task<IActionResult> PrivacyPolicy()
        {
            object response = null;
            var privacyPolicy = await GeneralHelpers.PrivacyPolicy();
            if (privacyPolicy.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            if (privacyPolicy.meta.statusCode != StatusCodeConsts.UnAuthorized)
                response = JsonConvert.DeserializeObject<PrivacyPolicyResponse>(privacyPolicy.data);
            return View(response);
        }

        public async Task<ActionResult> EditPrivacyPolicy([FromBody] EditPrivacyPolicyRequest request)
        {
            var response = await GeneralHelpers.EditPrivacyPolicy(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            return JsonSuccessResponse(response.meta.message);
        }
        #endregion Privacy Policy

        #region How To Play
        [Route("how-to-play", Name = "HowToPlay")]
        public async Task<IActionResult> HowToPlay()
        {
            object response = null;
            var howToPlay = await GeneralHelpers.HowToPlay();
            if (howToPlay.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            if (howToPlay.meta.statusCode != StatusCodeConsts.UnAuthorized)
                response = JsonConvert.DeserializeObject<HowToPlayResponse>(howToPlay.data);
            return View(response);
        }

        public async Task<ActionResult> EditHowToPlay([FromBody] EditHowToPlayRequest request)
        {
            var response = await GeneralHelpers.EditHowToPlay(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            return JsonSuccessResponse(response.meta.message);
        }
        #endregion How To Play

        #region Country
        [Route("country", Name = "Country")]
        public async Task<IActionResult> Country()
        {
            var country = await GeneralHelpers.Country();
            if (country.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return View();
        }

        public async Task<List<cityDetail>> GetCountry()
        {
            var countryList = await GeneralHelpers.Country();
            var response = JsonConvert.DeserializeObject<CountryListResponse>(countryList.data);
            for (int i = 0; response.details.Count > i; i++)
            {
                response.details[i].number = i + 1;
            }
            return response.details;
        }

        [HttpPost]
        public async Task<ActionResult> CountryPersist([FromBody] CountryPersistRequest request)
        {
            if (request.id == "")
                request.id = null;
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
            request.languageIdCsv = String.Join(",", request.selectedLanguage.Select(p => p.ToString()).ToArray());
            request.currencyIdCsv = String.Join(",", request.selectedCurrency.Select(p => p.ToString()).ToArray());
            var response = await GeneralHelpers.PersistCountry(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }

        [HttpPost]
        public async Task<ActionResult> CountryStatusOrDelete([FromBody] StatusGeneralRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);

            var response = await GeneralHelpers.StatusCountry(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
        #endregion Country

        #region Currency
        [Route("Currency", Name = "Currency")]
        public async Task<IActionResult> Currency()
        {
            var Currency = await GeneralHelpers.Currency();
            if (Currency.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return View();
        }

        public async Task<List<currencyDetail>> GetCurrency()
        {
            var CurrencyList = await GeneralHelpers.Currency();
            var response = JsonConvert.DeserializeObject<CurrencyListResponse>(CurrencyList.data);
            for (int i = 0; response.details.Count > i; i++)
            {
                response.details[i].number = i + 1;
            }
            return response.details;
        }

        [HttpPost]
        public async Task<ActionResult> CurrencyPersist([FromBody] CurrencyPersistRequest request)
        {
            if (request.id == "")
                request.id = null;
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
            var response = await GeneralHelpers.PersistCurrency(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }

        [HttpPost]
        public async Task<ActionResult> CurrencyStatusOrDelete([FromBody] StatusGeneralRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);

            var response = await GeneralHelpers.StatusCurrency(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
        #endregion Currency

        #region Language
        [Route("Language", Name = "Language")]
        public async Task<IActionResult> Language()
        {
            var Language = await GeneralHelpers.Language();
            if (Language.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return View();
        }

        public async Task<List<languageDetail>> GetLanguage()
        {
            var LanguageList = await GeneralHelpers.Language();
            var response = JsonConvert.DeserializeObject<LanguageListResponse>(LanguageList.data);
            for (int i = 0; response.details.Count > i; i++)
            {
                response.details[i].number = i + 1;
            }
            return response.details;
        }

        [HttpPost]
        public async Task<ActionResult> LanguagePersist([FromBody] LanguagePersistRequest request)
        {
            if (request.id == "")
                request.id = null;
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
            var response = await GeneralHelpers.PersistLanguage(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }

        [HttpPost]
        public async Task<ActionResult> LanguageStatusOrDelete([FromBody] StatusGeneralRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);

            var response = await GeneralHelpers.StatusLanguage(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
        #endregion Language
    }
}