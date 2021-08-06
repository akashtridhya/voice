namespace voice.models
{
    public class SpConsts
    {
        public class Token
        {
            public const string CreateAndValidate = "[dbo].[UsersAccessToken_Insert]";

            public const string Expire = "[dbo].[UsersAccessToken_Update]";
        }

        public class Account
        {
            public const string AdminTokenInsert = "[dbo].[AdminToken_Persist]";

            public const string SetSeedData = "[dbo].[Seed_Insert]";

            public const string CheckForAdmin = "[dbo].[Users_CheckForAdmin]";

            public const string CheckUsers = "[dbo].[Users_Check]";

            public const string InsertUsers = "[dbo].[Users_Insert]";

            public const string SelectProfile = "[dbo].[Users_Select]";

            public const string SelectProfileById = "[dbo].[Users_Select_By_Id]";

            public const string SelectForLogin = "[dbo].[Users_Select_For_Login]";

            public const string SelectForLoginDmaStaff = "[dbo].[Users_Select_For_Login_DmaStaff]";

            public const string ForgotPassword = "[dbo].[Users_Select_Forgot_Password]";

            public const string SetPassword = "[dbo].[Users_Update_Password]";

            public const string UpdateUser = "[dbo].[Users_Update]";

            public const string UpdateProfilePicture = "[dbo].[Users_Update_Profile]";

            public const string SelectDashboardCount = "[dbo].[Dashboard_Counts_Select]";

            public const string UpdatePasswordChangeFlag = "[dbo].[Users_Update_PasswordChangedFlag]";

            public const string UsersDeviceInsert = "[dbo].[UsersDevice_Insert]";

            public const string UsersDeviceDelete = "[dbo].[UsersDevice_Delete]";

            public const string UsersDeviceSelect = "[dbo].[UsersDevice_Select]";
        }

        public class Dashboard
        {
            public const string GetDashboardData = "[dbo].[Dashboard_Data]";
        }

        public class UserManagement
        {
            public const string AdminInsert = "[dbo].[Add_admin]";

            public const string UsersList = "[dbo].[Users_List]";

            public const string UsersStatusOrDelete = "[dbo].[Users_StatusOrDelete]";

            public const string UserTimeInsert = "[dbo].[UserTime_Insert]";

            public const string UserActiveTime = "[dbo].[Users_ActiveTime]";

            public const string UserBalancePersist = "[dbo].[UsersBalance_Persist]";

            public const string UserXpPointsPersist = "[dbo].[UsersXpPoints_Persist]";

            public const string UserXpTimePersist = "[dbo].[XpTime_Insert]";

            public const string UserLeaderBoard = "[dbo].[User_LeaderBoard]";
        }

        public class GeneralManagement
        {
            public const string TermsAndConditionsSelect = "[dbo].[TermsAndConditions_Select]";

            public const string TermsAndConditionsPersist = "[dbo].[TermsAndConditions_Persist]";

            public const string PrivacyPolicySelect = "[dbo].[PrivacyPolicy_Select]";

            public const string PrivacyPolicyPersist = "[dbo].[PrivacyPolicy_Persist]";

            public const string HowToPlaySelect = "[dbo].[HowToPlay_Select]";

            public const string HowToPlayPersist = "[dbo].[HowToPlay_Persist]";

            
            public const string CountrySelect = "[dbo].[Country_Select]";

            public const string CountryPersist = "[dbo].[Country_Persist]";

            public const string CountryStatusOrDelete = "[dbo].[Country_StatusOrDelete]";


            public const string LanguagePersist = "[dbo].[Language_Persist]";

            public const string LanguageSelect = "[dbo].[Language_Select]";

            public const string LanguageStatusOrDelete = "[dbo].[Language_StatusOrDelete]";


            public const string CurrencyPersist = "[dbo].[Currency_Persist]";

            public const string CurrencySelect = "[dbo].[Currency_Select]";

            public const string CurrencyStatusOrDelete = "[dbo].[Currency_StatusOrDelete]";

        }

        public class TokenManagement
        {
            public const string TokenPersist = "[dbo].[Token_Persist]";

            public const string TokenList = "[dbo].[Token_List]";

            public const string TokenStatus = "[dbo].[Token_StatusOrDelete]";
        }

        public class Generic
        {
            public const string PersistRandomUrl = "[dbo].[RandomURL_Persist]";

            public const string SelectRandomUrl = "[dbo].[RandomURL_Select_List]";
        }

        public class AdsManagement
        {
            public const string AdsSelect = "[dbo].[Ads_Select]";

            public const string AdsPersist = "[dbo].[Ads_Persist]";

            public const string AdsStatusOrDelete = "[dbo].[Ads_StatusOrDelete]";
        }

        public class SpinManagement
        {
            public const string SpinSelect = "[dbo].[Spin_Select]";

            public const string SpinCheckValid = "[dbo].[SpinUser_CheckValid]";

            public const string SpinPersist = "[dbo].[Spin_Persist]";

            public const string SpinStatusOrDelete = "[dbo].[Spin_StatusOrDelete]";

            public const string SpinValue = "[dbo].[SpinUser_Select]";
        }

        public class GameManagement
        {
            public const string GamePersist = "[dbo].[Game_Persist]";

            public const string GameSelect = "[dbo].[Games_Select]";

            public const string GameStatus = "[dbo].[Game_StatusOrDelete]";
        }

        public class XpPointsManagement
        {
            public const string XpPointsPersist = "[dbo].[Xp_Persist]";

            public const string XpPointsSelect = "[dbo].[Xp_Select]";

            public const string XpPointsStatus = "[dbo].[Xp_StatusOrDelete]";
        }

        public class LevelManagement
        {
            public const string LevelPersist = "[dbo].[Level_Persist]";

            public const string LevelSelect = "[dbo].[Level_Select]";

            public const string LevelStatus = "[dbo].[Level_StatusOrDelete]";
        }
    }
}