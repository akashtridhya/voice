using System;
using System.Collections.Generic;

namespace voice.blazor.Models.Bonus.Response
{
    public class Detail
    {
        public int number { get; set; }
        public string Id { get; set; }
        public string BonusName { get; set; }
        public string BonusDescription { get; set; }
        public double BonusAmout { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class BonusListResponse
    {
        public IEnumerable<Detail> details { get; set; }
    }
}
