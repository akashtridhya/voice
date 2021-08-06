using System;
using System.Collections.Generic;

namespace voice.middleware.Models.General.PrivacyPolicy.Response
{
    public class Detail
    {
        public string Id { get; set; }
        public string PrivacyPolicy { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class PrivacyPolicyResponse
    {
        public List<Detail> details { get; set; }
    }
}