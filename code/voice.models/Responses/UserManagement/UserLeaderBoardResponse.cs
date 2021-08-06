using System.Collections.Generic;

namespace voice.models.Responses.UserManagement
{
    public class UserLeaderBoardResponse
    {
        public int Position { get; set; }
        public string UserName { get; set; }
        public string XpPoints { get; set; }
    }

    public class User
    {
        public int Position { get; set; }
        public string UserName { get; set; }
        public string XpPoints { get; set; }
    }

    public class demo
    {
        public User user { get; set; }
        public IEnumerable<UserLeaderBoardResponse> details { get; set; }

    }

}
