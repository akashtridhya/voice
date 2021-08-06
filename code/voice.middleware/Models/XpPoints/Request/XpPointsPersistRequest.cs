using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace voice.middleware.Models.XpPoints.Request
{
    public class XpPointsPersistRequest
    {
        public string id { get; set; }
        public int xpTime { get; set; }
        public int xpValue { get; set; }
    }
}
