using System;

namespace voice.models
{
    public class BaseResponse
    {
        public Guid Id { get; set; }
    }

    public class BaseAuditResponse : BaseIdResponse
    {
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }

    public class BaseIdResponse
    {
        public Guid Id { get; set; }
    }
}