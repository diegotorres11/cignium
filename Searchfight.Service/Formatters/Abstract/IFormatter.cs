using Domain.Model;

namespace Service.Formatters.Abstract
{
    public interface IFormatter
    {
        string FormatResultsPerTerm(ResultsPerTerm resultsPerTerm);
        string FormatWinnersPerEngine(WinnersPerEngine winnersPerEngine);
        string FormatTotalWinner(TotalWinner totalWinner);
    }
}
