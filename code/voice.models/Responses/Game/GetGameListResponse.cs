using System;

namespace voice.models.Responses.Game
{
    public class GetGameListResponse
    {
        public Guid Id { get; set; }
        public string GameName { get; set; }
        public string GameTag { get; set; }
        public string GameImageURL { get; set; }
        public string GameLaunchURL { get; set; }
        public string GameCategory { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}