using System;
using System.Collections.Generic;

namespace voice.blazor.Models.General.Currency.Response
{
    public class currencyDetail
    {
        public string Id { get; set; }
        public int number { get; set; }
        public string Currency { get; set; }
        public string CurrencyCode { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class CurrencyListResponse
    {
        public List<currencyDetail> details { get; set; }
    }
}