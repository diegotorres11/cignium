using Domain.Model;
using Service.Calculator.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Service.Calculator.Concrete
{
    public class DefaultCalculator : ICalculator
    {
        public ResultsPerTerm GetResultsPerTern(IEnumerable<SearchResult> SearchResults)
        {
            var results = new ResultsPerTerm();
            var searchTerms = SearchResults.Select(sr => sr.SearchTerm).Distinct();

            foreach (string term in searchTerms)
            {
                results.Add(term, new List<SearchResult>());

                var resultsByTerm = SearchResults.Where(sr => sr.SearchTerm == term);

                foreach (var result in resultsByTerm)
                {
                    var searchResult = new SearchResult
                    {
                        ResultsCount = result.ResultsCount,
                        SearchEngine = result.SearchEngine,
                        SearchTerm = result.SearchTerm
                    };

                    results[term].Add(searchResult);
                }
            }

            return results;
        }

        public TotalWinner CalcTotalWinner(IEnumerable<SearchResult> SearchResults)
        {
            var sortedResults = from result in SearchResults
                         group result by result.SearchTerm into g
                         orderby g.Sum(sr => sr.ResultsCount) descending
                         select new TotalWinner
                         {
                            SearchTerm = g.Key,
                            SearchResultsCount = g.Sum(sr => sr.ResultsCount)
                         };

            return sortedResults.First();
        }

        public WinnersPerEngine CalcWinnersPerEngine(IEnumerable<SearchResult> SearchResults)
        {
            var winners = new WinnersPerEngine();
            var engines = SearchResults.Select(sr => sr.SearchEngine).Distinct();

            foreach (string engine in engines)
            {
                SearchResult winnerEngine = SearchResults
                    .Where(sr => sr.SearchEngine == engine)
                    .OrderByDescending(sr => sr.ResultsCount)
                    .First();

                winners.Add(winnerEngine.SearchEngine, 
                    new TotalWinner
                    {
                        SearchTerm = winnerEngine.SearchTerm,
                        SearchResultsCount = winnerEngine.ResultsCount
                    });
            }

            return winners;
        }
    }
}
