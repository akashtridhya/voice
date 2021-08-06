using Newtonsoft.Json;

namespace voice.models.Games.Request.Account
{
    public class GameLoginRequest
    {
        [JsonProperty("login.accountSystemTag")]
        public string LoginAccountSystemTag { get; set; }

        [JsonProperty("login.password")]
        public string LoginPassword { get; set; }

        [JsonProperty("login.platformTag")]
        public string LoginPlatformTag { get; set; }

        [JsonProperty("login.principle")]
        public string LoginPrinciple { get; set; }
    }
}