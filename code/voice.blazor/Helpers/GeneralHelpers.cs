using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using voice.blazor.Models;
using voice.blazor.Models.Account;
using voice.blazor.Models.Consts;
using voice.blazor.Models.General.Country.Request;
using voice.blazor.Models.General.Currency.Request;
using voice.blazor.Models.General.HowToPlay.Request;
using voice.blazor.Models.General.Language.Request;
using voice.blazor.Models.General.PrivacyPolicy.Request;
using voice.blazor.Models.General.TermsAndConditions.Request;

namespace voice.blazor.Helpers
{
    public class GeneralHelpers : IDisposable
    {

        #region Terms And Conditions
        public static async Task<dynamic> GetTermsAndConditions()
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.GetTermsAndConditions}";
            var response = await Services.Service.GetAPIWithToken(url);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }

        internal static async Task<dynamic> EditTermsAndCondition(EditTermsAndConditionRequest request)
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.PersistTermsAndConditions}";

            var APIRequest = new EditTermsAndConditionRequest
            {
                Id = request.Id,
                termsAndConditions = request.termsAndConditions
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(APIRequest), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion Terms And Conditions

        #region Privacy Policy
        public static async Task<dynamic> PrivacyPolicy()
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.GetPrivacyPolicy}";
            var response = await Services.Service.GetAPIWithToken(url);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }

        internal static async Task<dynamic> EditPrivacyPolicy(EditPrivacyPolicyRequest request)
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.PersistPrivacyPolicy}";

            var APIRequest = new EditPrivacyPolicyRequest
            {
                Id = request.Id,
                privacyPolicy = request.privacyPolicy
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(APIRequest), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion Privacy Policy

        #region How to Play
        public static async Task<dynamic> HowToPlay()
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.GetHowToPlay}";
            var response = await Services.Service.GetAPIWithToken(url);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }

        internal static async Task<dynamic> EditHowToPlay(EditHowToPlayRequest request)
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.PersistHowToPlay}";

            var APIRequest = new EditHowToPlayRequest
            {
                Id = request.Id,
                howToPlay = request.howToPlay
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(APIRequest), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion How to Play

        #region Country
        public static async Task<dynamic> Country(string Token = "")
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.GetCountries}";
            var response = await Services.Service.GetAPIWithToken(url, Token);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }

        internal static async Task<dynamic> PersistCountry(CountryPersistRequest request, string Token = "")
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.PersistCountry}";

            var APIRequest = new CountryPersistRequest
            {
                id = request.id,
                country = request.country,
                countryCode = request.countryCode
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(APIRequest), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent, Token);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }

        internal static async Task<dynamic> StatusCountry(StatusGeneralRequest request, string Token = "")
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.StatusConutry}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent, Token);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion Country

        #region Currency
        public static async Task<dynamic> Currency()
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.GetCurrency}";
            var response = await Services.Service.GetAPIWithToken(url);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }

        internal static async Task<dynamic> PersistCurrency(CurrencyPersistRequest request)
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.PersistCurrency}";

            var APIRequest = new CurrencyPersistRequest
            {
                id = request.id,
                currency = request.currency,
                currencyCode = request.currencyCode
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(APIRequest), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }

        internal static async Task<dynamic> StatusCurrency(StatusGeneralRequest request)
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.StatusCurrency}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion Currency

        #region Language
        public static async Task<dynamic> Language(string Token = "")
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.GetLanguage}";
            var response = await Services.Service.GetAPIWithToken(url, Token);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }

        internal static async Task<dynamic> PersistLanguage(LanguagePersistRequest request, string Token = "")
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.PersistLanguage}";

            var APIRequest = new LanguagePersistRequest
            {
                id = request.id,
                language = request.language,
                languageCode = request.languageCode
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(APIRequest), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent, Token);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }

        internal static async Task<dynamic> StatusLanguage(StatusGeneralRequest request, string Token = "")
        {
            var url = $"{ApiEndPointConsts.GeneralManagement.StatusLanguage}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Services.Service.PostAPIWithToken(url, stringContent, Token);
            if (response.meta.statusCode != StatusCodeConsts.Success)
            {
                return new CallAPIList()
                {
                    meta = response.meta,
                };
            }
            return new CallAPI()
            {
                meta = response.meta,
                data = response.data
            };
        }
        #endregion Language

        #region House Keeping
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool dispose)
        {
            if (dispose)
            {
            }
        }
        #endregion
    }
}