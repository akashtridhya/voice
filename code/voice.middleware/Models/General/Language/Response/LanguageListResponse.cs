using System;
using System.Collections.Generic;

namespace voice.middleware.Models.General.Language.Response
{
    public class languageDetail
    {
        public string Id { get; set; }
        public int number { get; set; }

        public string Language { get; set; }
        public string LanguageCode { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class LanguageListResponse
    {
        public List<languageDetail> details { get; set; }
    }
}