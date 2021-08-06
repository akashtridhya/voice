using System;
using System.Collections.Generic;

namespace voice.middleware.Models.Ads.Response
{
    public class AdDetail
    {
        public int number { get; set; }
        public string Id { get; set; }
        public string AdName { get; set; }
        public string AdImageURL { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class AdListResponse
    {
        public List<AdDetail> details { get; set; }
    }
}