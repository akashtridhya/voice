using System;

namespace voice.models.Requests.Game
{
    public class PersistGameRequest :BaseIdRequest
    {
        public string Role { get; set; }
       
        public string GameName { get; set; }

        public string GameTag { get; set; }

        public Guid XpPointsId { get; set; }

        public string GameImageURL { get; set; }

        public string GameCategory { get; set; }
    }
}