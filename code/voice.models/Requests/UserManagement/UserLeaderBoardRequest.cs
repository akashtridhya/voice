using System.Text.Json.Serialization;

namespace voice.models
{
    public class UserLeaderBoardRequest : BaseValidateRequest
    {
        public string Location { get; set; }
    }
}
