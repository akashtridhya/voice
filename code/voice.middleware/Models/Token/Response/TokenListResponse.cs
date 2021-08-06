using System;
using System.Collections.Generic;

namespace voice.middleware.Models.Token.Response
{
    public class Detail
    {
        public int number { get; set; }
        public string Id { get; set; }
        public string TokenName { get; set; }
        public string TokenDescription { get; set; }
        public double TokenAmout { get; set; }
        public string ProductId { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class TokenListResponse
    {
        public IEnumerable<Detail> details { get; set; }
    }
}
