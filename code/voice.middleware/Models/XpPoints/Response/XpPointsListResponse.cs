using System;
using System.Collections.Generic;

namespace voice.middleware.Models.XpPoints.Response
{
    public class xpPointsDetail
    {
        public string Id { get; set; }
        public int number { get; set; }
        public int XpTime { get; set; }
        public int XpValue { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class XpPointsListResponse
    {
        public List<xpPointsDetail> details { get; set; }
    }
}