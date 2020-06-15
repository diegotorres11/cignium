using Domain.Model;
using Service.Calculator.Abstract;
using Service.SearchEngines.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class SearcherService
    {
        public ICollection<Searcher> Searchers { get; private set; }
        public ICollection<SearchResult> SearchResults { get; private set; }
        public SearchTerm SearchTerm { get; }
        public ICalculator Calculator { get; }

        public SearcherService(SearchTerm searchTerm, ICalculator calculator)
        {
            SearchTerm = searchTerm;
            Calculator = calculator;
            Searchers = new List<Searcher>();
            SearchResults = new List<SearchResult>();
        }

        public void AddSearchEngine(Searcher searcher)
        {
            Searchers.Add(searcher);
        }

        public async Task Search()
        {
            foreach (Searcher searcher in Searchers)
            {
                foreach (string term in SearchTerm.Terms)
                {
                    var searchResult = await searcher.Search(term);
                    SearchResults.Add(searchResult);
                }
            }
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
