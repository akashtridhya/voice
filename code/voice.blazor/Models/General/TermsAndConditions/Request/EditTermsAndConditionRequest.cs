using System;

namespace voice.blazor.Models.General.TermsAndConditions.Request
{
    public class EditTermsAndConditionRequest
    {
        public Guid Id { get; set; }

        public string termsAndConditions { get; set; }
    }
}