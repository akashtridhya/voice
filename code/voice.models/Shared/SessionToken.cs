namespace voice.api.Shared
{
    public static class SessionToken
    {
        public static string Token { get; set; }

        public static bool flag { get; set; }

        public static string UniqueId { get; set; }

        public static string Role { get; set; }

        public static void setToken(string token)
        {
            Token = token;
            flag = true;
        }

        public static void setRole(string role)
        {
            Role = role;
        }

        public static string getToken()
        {
            return Token;
        }

        public static string getUniqueId()
        {
            return UniqueId;
        }

        public static bool getFlag()
        {
            return flag;
        }

        public static string getRole()
        {
            return Role;
        }
    }
}
