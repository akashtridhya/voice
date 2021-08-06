using System;
using System.Collections.Generic;

namespace voice.middleware.Models.Spin.Response
{
    public class DetailSpin
    {
        public int number { get; set; }
        public string Id { get; set; }
        public string SpinValue { get; set; }
        public string SpinFrequency { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class SpinListResponse
    {
        public IEnumerable<DetailSpin> details { get; set; }
    }
}
