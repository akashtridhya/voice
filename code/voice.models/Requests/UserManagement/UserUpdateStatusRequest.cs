namespace voice.models
{
    public class UserUpdateStatusRequest : BaseRequiredIdRequest
    {
        public bool? Active { get; set; }

        public bool? Deleted { get; set; }
    }
}