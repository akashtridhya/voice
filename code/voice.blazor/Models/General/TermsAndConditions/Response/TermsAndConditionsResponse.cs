using System;
using System.Collections.Generic;

namespace voice.blazor.Models.General.TermsAndConditions.Response
{
    public class Detail
    {
        public string Id { get; set; }
        public string TermsAndCondtions { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class TermsAndConditionsResponse
    {
        public List<Detail> details { get; set; }
    }
}