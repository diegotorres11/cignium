using Domain.Model;
using Service.Configuration.Concrete;
using Service.SearchEngines.Abstract;
using System.Threading.Tasks;
using Util;

namespace Service.SearchEngines.Concrete.Google
{
    public class GoogleSearcher : Searcher
    {
        public GoogleSearcher()
        {
            Config = new GoogleConfiguration();
        }

        public override async Task<SearchResult> Search(string term)
        {
            Config.Parameters["q"] = term.Replace(" ", "+");
            var result = await WebServiceConsumer.ConsumeAsync<GoogleSearchResult>(Url);
            var mappedResult = Map(result, term);

            return mappedResult;
        }

        private SearchResult Map(GoogleSearchResult result, string term)
        {
            return new SearchResult
            {
                SearchEngine = EngineName,
                SearchTerm = term,
                ResultsCount = result.SearchInformation.TotalResults
            };
        }
    }
}
