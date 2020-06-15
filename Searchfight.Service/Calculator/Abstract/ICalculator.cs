using Domain.Model;
using System.Collections.Generic;

namespace Service.Calculator.Abstract
{
    public interface ICalculator
    {
        ResultsPerTerm GetResultsPerTern(IEnumerable<SearchResult> SearchResults);
        WinnersPerEngine CalcWinnersPerEngine(IEnumerable<SearchResult> SearchResults);
        TotalWinner CalcTotalWinner(IEnumerable<SearchResult> SearchResults);
    }
}
