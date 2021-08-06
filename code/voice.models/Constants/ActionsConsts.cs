namespace voice.models
{
    public class ActionsConsts
    {
        public const string ApiVersion = "api/v1";

        public class Account
        {
            public const string Login = "account/login";

            public const string Register = "account/register";

            public const string Logout = "account/logout";

            public const string LoginDmaStaff = "account/login_dma_staff";

            public const string ForgotPassword = "account/forgot_password";

            public const string ResetPassword = "account/reset_password";

            public const string ChangePassword = "account/change_password";

            public const string Profile = "account/profile/retrieve";

            public const string UpdateProfile = "account/profile/update";

            public const string UpdateProfilePicture = "account/profile_picture/update";

            public const string DeleteProfilePicture = "account/profile_picture/remove";

            public const string UpdatePasswordChangeFlag = "account/password_flag/update";

            public const string UserDeviceInsert = "account/user_device/insert";
        }

        public class Dashboard
        {
            public const string GetDashboardData = "dashboard/getdata";
        }

        public class UserManagement
        {
            public const string RetrieveUsers = "users/retrieve";

            public const string AdminInsert = "users/addadmin";

            public const string StatusOrDeleteUser = "users/statusordelete";

            public const string InsertUserActiveTime = "users/insertactivetime";

            public const string GetUserActiveTime = "users/getactivetime";

            public const string PersistUserBalance = "users/persistbalance";

            public const string UserLeaderBoard = "users/userleaderboard";

            public const string PersistUserXpTime = "users/persistxptime";
        }

        public class GeneralManagement
        {
            #region TermsAndConditions
            public const string RetriveTermsAndConditions = "general/termsandcoditions/retrive";

            public const string RetriveMobileTermsAndConditions = "general/mobile/termsandcoditions/retrive";

            public const string PersistTermsAndConditions = "general/termsandconditions/persist";
            #endregion TermsAndConditions

            #region Privacy Policy
            public const string RetrivePrivacyPolicy = "general/privacypolicy/retrive";

            public const string RetriveMobilePrivacyPolicy = "general/mobile/privacypolicy/retrive";

            public const string PersistPrivacyPolicy = "general/privacypolicy/persist";
            #endregion Privacy Policy

            #region How To Play
            public const string RetriveHowToPlay = "general/howtoplay/retrive";

            public const string RetriveMobileHowToPlay = "general/mobile/howtoplay/retrive";

            public const string PersistHowToPlay = "general/howtoplay/persist";
            #endregion How To Play

            #region Country
            public const string RetriveCountry = "general/country/retrive";

            public const string RetriveMobileCountry = "general/mobile/country/retrive";

            public const string PersistCountry = "general/country/persist";

            public const string StatusOrDeleteCountry = "general/country/statusordelete";
            #endregion Country

            #region Language
            public const string PersistLanguage = "general/language/persist";

            public const string RetriveLanguage = "general/language/retrive";

            public const string RetriveMobileLanguage = "general/mobile/language/retrive";

            public const string StatusOrDeleteLanguage = "general/language/statusordelete";
            #endregion Language

            #region Currency
            public const string PersistCurrency = "general/currency/persist";

            public const string RetriveCurrency = "general/currency/retrive";

            public const string RetriveMobileCurrency = "general/mobile/currency/retrive";

            public const string StatusOrDeleteCurrency = "general/currency/statusordelete";
            #endregion Currency
        }

        public class Token
        {
            public const string PersistToken = "token/persist";

            public const string RetrieveToken = "token/retrive";

            public const string StatusToken = "token/status";
        }

        public class GameManagement
        {
            public const string RetriveGames = "games/retrive";

            public const string PersistGames = "games/persist";

            public const string StatusOrDeleteGames = "games/statusordelete";

            public const string UploadGameImage = "games/image/upload";

            public const string RemoveGameImage = "games/image/remove";

        }

        public class AdsManagement
        {
            public const string PersistAds = "ads/persist";

            public const string RetriveAds = "ads/retrive";

            public const string RetriveMobileAds = "mobile/ads/retrive";

            public const string StatusOrDeleteAds = "ads/statusordelete";
        }

        public class SpinManagement
        {
            public const string PersistSpin = "spin/persist";

            public const string SpinCheckValid = "spin/checkvalid";

            public const string RetriveSpin = "spin/retrive";

            public const string StatusOrDeleteSpin = "spin/statusordelete";

            public const string SpinValue = "spin/value";
        }

        public class Generic
        {
            public const string AttachmentUoload = "generic/attachment_upload";

            public const string SetShortURL = "generic/set_short_url";

            public const string GetSourceURL = "generic/get_source_url/";
        }

        public class XpPointsManagement
        {
            public const string PersistXpPoints = "xppoints/persist";

            public const string RetriveXpPoints = "xppoints/retrive";

            public const string RetriveMobileXpPoints = "mobile/xppoints/retrive";

            public const string StatusOrDeleteXpPoints = "xppoints/statusordelete";
        }

        public class LevelManagement
        {
            public const string PersistLevel = "level/persist";

            public const string RetriveLevel = "level/retrive";

            public const string RetriveMobileLevel = "mobile/level/retrive";

            public const string StatusOrDeleteLevel = "level/statusordelete";
        }
    }
}