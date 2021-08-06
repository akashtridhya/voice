using Microsoft.AspNetCore.Http;

namespace voice.middleware.Models.Ads.Request
{
    public class PersistAdsRequest
    {
        public string id { get; set; }
        public string adName { get; set; }
        public string adImageURL { get; set; }
        public IFormFile File { get; set; }
    }
}