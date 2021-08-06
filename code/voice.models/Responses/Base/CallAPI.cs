using Newtonsoft.Json;
using System.Collections.Generic;

namespace voice.models.callapi
{
    public class CallAPI
    {
        public ResponseMetaCallAPI error { get; set; }

        public dynamic data { get; set; }

        public string SessionCorrelationId { get; set; }
        public string UserCorrelationId { get; set; }

        public string GetJsonData()
        {
            return JsonConvert.SerializeObject(data);
        }
    }

    public class CallAPIList
    {
        public ResponseMetaListCallAPI error { get; set; }
    }

    public class ResponseMetaCallAPI
    {
        public int statusCode { get; set; }

        public dynamic message { get; set; }
    }

    public class ResponseMetaListCallAPI
    {
        public int statusCode { get; set; }

        public int code { get; set; }
        public List<ErrorProperty> errorProperties { get; set; }
        public int httpCode { get; set; }
        public string message { get; set; }
        public string type { get; set; }
    }

    public class ErrorProperty
    {
        public int code { get; set; }
        public string message { get; set; }
        public string property { get; set; }
    }
}