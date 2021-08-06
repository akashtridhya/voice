using System;

namespace voice.models
{
    public class BaseIdRequest : BaseValidateRequest
    {
        public Guid? Id { get; set; }
    }
}