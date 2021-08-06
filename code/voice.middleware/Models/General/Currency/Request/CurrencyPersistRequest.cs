using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace voice.middleware.Models.General.Currency.Request
{
    public class CurrencyPersistRequest
    {
        public string id { get; set; }
        public string currency { get; set; }
        public string currencyCode { get; set; }
    }
}
