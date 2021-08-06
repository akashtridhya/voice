namespace voice.middleware.Models
{
    public class ApiEndPointConsts
    {
        const string Version = "v1/";

#if DEBUG
        public const string BaseRoute = "http://18.134.82.112/api/api/" + Version;
        //public const string BaseRoute = "https://localhost:44368/api/" + Version;
#elif QA
        public const string BaseRoute = "http://13.232.217.47:9000/api/" + Version;
#elif RELEASE
        public const string BaseRoute = "https://aps.gujaratgas.com/dma-uat-api/api/" + Version;
#elif PRODUCTION
        public const string BaseRoute = "https://customerconnect.gujaratgas.com/dma-api/api/" + Version;
#endif

        public class Generic
        {
            public const string ImageUpload = BaseRoute + "generic/attachment_upload";

            public const string GetSourceURL = BaseRoute + "generic/get_source_url/";
        }

        public class Account
        {
            public const string Login = BaseRoute + "account/login";

            public const string Logout = BaseRoute + "account/logout";

            public const string PasswordFlag = BaseRoute + "account/password_flag/update";

            public const string ForgetPassword = BaseRoute + "account/forgot_password";

            public const string ResetPassword = BaseRoute + "account/reset_password";

            public const string ChangePassword = BaseRoute + "account/change_password";

            public const string EditProfile = BaseRoute + "account/profile/update";

            public const string RetriveProfile = BaseRoute + "account/profile/retrieve";

            public const string ImageProfile = BaseRoute + "account/profile_picture/update";

            public const string RemoveImageProfile = BaseRoute + "account/profile_picture/remove";

            public const string AddUser = BaseRoute + "account/register";

            public const string GetDashboardData = BaseRoute + "account/dashboard/rerieve";
        }

        public class Dashboard
        {
            public const string GetDashBoardData = BaseRoute + "dashboard/getdata";
        }

        public class UsersManagement
        {
            public const string UsersList = BaseRoute + "users/retrieve";

            public const string UserStatus = BaseRoute + "users/statusordelete";

            public const string UserUpdate = BaseRoute + "account/profile/update";
        }

        public class GeneralManagement
        {
            public const string GetTermsAndConditions = BaseRoute + "general/termsandcoditions/retrive";

            public const string PersistTermsAndConditions = BaseRoute + "general/termsandconditions/persist";



            public const string GetPrivacyPolicy = BaseRoute + "general/privacypolicy/retrive";

            public const string PersistPrivacyPolicy = BaseRoute + "general/privacypolicy/persist";



            public const string GetHowToPlay = BaseRoute + "general/howtoplay/retrive";

            public const string PersistHowToPlay = BaseRoute + "general/howtoplay/persist";


            public const string GetCountries = BaseRoute + "general/country/retrive";

            public const string PersistCountry = BaseRoute + "general/country/persist";

            public const string StatusConutry = BaseRoute + "general/country/statusordelete";


            public const string GetCurrency = BaseRoute + "general/currency/retrive";

            public const string PersistCurrency = BaseRoute + "general/currency/persist";

            public const string StatusCurrency = BaseRoute + "general/currency/statusordelete";



            public const string GetLanguage = BaseRoute + "general/language/retrive";

            public const string PersistLanguage = BaseRoute + "general/language/persist";

            public const string StatusLanguage = BaseRoute + "general/language/statusordelete";
        }

        public class TokenManagement
        {
            public const string TokenList = BaseRoute + "token/retrive";

            public const string TokenStatus = BaseRoute + "token/status";

            public const string TokenPersist = BaseRoute + "token/persist";
        }

        public class AdsManagement
        {
            public const string AdsList = BaseRoute + "ads/retrive";

            public const string AdsStatus = BaseRoute + "ads/statusordelete";

            public const string AdsPersist = BaseRoute + "ads/persist";
        }

        public class SpinManagement
        {
            public const string SpinList = BaseRoute + "spin/retrive";

            public const string SpinCheckValid = BaseRoute + "spin/checkvalid";

            public const string SpinStatus = BaseRoute + "spin/statusordelete";

            public const string SpinPersist = BaseRoute + "spin/persist";
        }

        public class GameManagement
        {
            public const string GameList = BaseRoute + "games/retrive";

            public const string GameStatus = BaseRoute + "games/statusordelete";

            public const string GamePersist = BaseRoute + "games/persist";
        }

        public class XpPointsManagement
        {
            public const string XpPointsPersist = BaseRoute + "xppoints/persist";

            public const string XpPointsRetrive = BaseRoute + "xppoints/retrive";

            public const string XpPointsStatusOrDelete = BaseRoute + "xppoints/statusordelete";
        }

        public class LevelManagement
        {
            public const string LevelPersist = BaseRoute + "level/persist";

            public const string LevelRetrive = BaseRoute + "level/retrive";

            public const string LevelStatusOrDelete = BaseRoute + "level/statusordelete";
        }
    }
}