using System;

namespace voice.middleware.Models.General.PrivacyPolicy.Request
{
    public class EditPrivacyPolicyRequest
    {
        public Guid Id { get; set; }

        public string privacyPolicy { get; set; }
    }
}