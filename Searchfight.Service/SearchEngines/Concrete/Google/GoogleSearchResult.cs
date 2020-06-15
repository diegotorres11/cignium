namespace Service.SearchEngines.Concrete.Google
{
    public class GoogleSearchResult
    {
        public SearchInformation SearchInformation { get; set; }
    }

    public class SearchInformation
    {
        public long TotalResults { get; set; }
    }
}
