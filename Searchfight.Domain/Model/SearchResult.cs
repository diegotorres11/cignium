namespace Domain.Model
{
    public class SearchResult
    {
        public string SearchTerm { get; set; }
        public string SearchEngine { get; set; }
        public long ResultsCount { get; set; }
    }
}
