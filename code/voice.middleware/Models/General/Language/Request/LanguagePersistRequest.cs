using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace voice.middleware.Models.General.Language.Request
{
    public class LanguagePersistRequest
    {
        public string id { get; set; }
        public string language { get; set; }
        public string languageCode { get; set; }
    }
}
