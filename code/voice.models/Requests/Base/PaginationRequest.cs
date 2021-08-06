namespace voice.models
{
    public class PaginationRequest : BaseValidateRequest
    {
        public int? PageNo { get; set; }

        public int? PageSize { get; set; }
    }
}