using Microsoft.AspNetCore.Http;

namespace voice.middleware.Models.Game.Request
{
    public class PersistGameRequest
    {
        public string id { get; set; }
        public string gameName { get; set; }
        public string gameTag { get; set; }
        public string xpPointsid { get; set; }
        public string gameImageURL { get; set; }
        public string gameCategory { get; set; }

        public IFormFile File { get; set; }
    }
}