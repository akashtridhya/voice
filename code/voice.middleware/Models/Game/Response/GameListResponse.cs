using System.Collections.Generic;

namespace voice.middleware.Models.Game.Response
{
    public class GameDetail
    {
        public string Id { get; set; }
        public string GameName { get; set; }
        public string GameImageURL { get; set; }
        public string GameCategory { get; set; }
        public bool Active { get; set; }
        public string GameTag { get; set; }
        public string XpPointsId { get; set; }
        public string XpPoints { get; set; }
        public string GameLaunchURL { get; set; }
        public int number { get; set; }
    }

    public class GameListResponse
    {
        public List<GameDetail> details { get; set; }
    }
}