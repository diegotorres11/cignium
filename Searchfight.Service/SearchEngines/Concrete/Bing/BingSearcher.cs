using Domain.Model;
using Service.Configuration.Concrete;
using Service.SearchEngines.Abstract;
using System.Threading.Tasks;
using Util;

namespace Service.SearchEngines.Concrete.Bing
{
    public class BingSearcher : Searcher
    {
        public BingSearcher()
        {
            Config = new BingConfiguration();
        }

        public override async Task<SearchResult> Search(string term)
        {
            Config.Parameters["q"] = term;
            var result = await WebServiceConsumer.ConsumeAsync<BingSearchResult>(Url, Config.Headers);
            var mappedResult = Map(result, term);

            return mappedResult;
        }

        private SearchResult Map(BingSearchResult result, string term)
        {
            return new SearchResult
            {
                SearchEngine = EngineName,
                SearchTerm = term,
                ResultsCount = result.WebPages.TotalEstimatedMatches
            };
        }
    }
}
