namespace Service.SearchEngines.Concrete.Bing
{
    public class BingSearchResult
    {
        public WebPages WebPages { get; set; }
    }

    public class WebPages
    {
        public long TotalEstimatedMatches { get; set; }
    }
}
