﻿using Newtonsoft.Json;

namespace voice.blazor.Models.General.TermsAndConditions.Response
{
    public class EditTermsAndConditionResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        public int StatusCode { get; set; }
    }
}