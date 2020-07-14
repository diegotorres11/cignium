using Domain.Model;
using Service.Configuration.Concrete;
using Service.SearchEngines.Abstract;
using System;
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
            try
            {
                var result = await WebServiceConsumer.ConsumeAsync<BingSearchResult>(Url, Config.Headers);
                var mappedResult = Map(result, term);

                return mappedResult;
            }
            catch (Exception ex)
            {
                //TODO: Log ex to a target
                return new SearchResult
                {
                    SearchEngine = EngineName,
                    SearchTerm = term,
                    ResultsCount = 0,
                    Status = SearchResultStatus.Error,
                    ErrorMessage = "There was an issue"
                };
            }
        }

        private SearchResult Map(BingSearchResult result, string term)
        {
            return new SearchResult
            {
                SearchEngine = EngineName,
                SearchTerm = term,
                ResultsCount = result.WebPages.TotalEstimatedMatches,
                Status = SearchResultStatus.Success
            };
        }
    }
}
