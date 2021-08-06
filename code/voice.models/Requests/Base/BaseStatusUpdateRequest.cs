namespace voice.models
{
    public class BaseStatusUpdateRequest : BaseRequiredIdRequest
    {
        public bool? Active { get; set; }

        public bool? Deleted { get; set; }
    }
}
