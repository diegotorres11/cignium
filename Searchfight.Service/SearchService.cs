using Domain.Model;
using Service.Calculator.Abstract;
using Service.SearchEngines.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class SearchService
    {
        public ICollection<Searcher> Searchers { get; private set; }
        public ICollection<SearchResult> SearchResults { get; private set; }
        public SearchTerm SearchTerm { get; }
        public ICalculator Calculator { get; }

        public SearchService(SearchTerm searchTerm, IList<Searcher> searchers, ICalculator calculator)
        {
            SearchTerm = searchTerm;
            Searchers = searchers;
            Calculator = calculator;
            SearchResults = new List<SearchResult>();
        }

        public void AddSearchEngine(Searcher searcher)
        {
            Searchers.Add(searcher);
        }

        public async Task Search()
        {
            var searchResultsTasks = new List<Task<SearchResult>>();

            foreach (Searcher searcher in Searchers)
            {
                foreach (string term in SearchTerm.Terms)
                {
                    var searchResult = searcher.Search(term);
                    searchResultsTasks.Add(searchResult);
                }
            }

            await Task.WhenAll(searchResultsTasks);

            searchResultsTasks.ForEach(task => SearchResults.Add(task.Result));
        }

        public ResultsPerTerm ResultsPerTerm
        {
            get { return Calculator.GetResultsPerTern(SearchResults); }
        }

        public WinnersPerEngine WinnersPerEngine
        {
            get { return Calculator.CalcWinnersPerEngine(SearchResults); }
        }

        public TotalWinner TotalWinner
        {
            get { return Calculator.CalcTotalWinner(SearchResults); }
        }
    }
}
