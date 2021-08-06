namespace voice.models
{
    public class SearchParamRequest : PaginationRequest
    {
        public string SearchParam { get; set; }

        public string OrderBy { get; set; }
    }
}