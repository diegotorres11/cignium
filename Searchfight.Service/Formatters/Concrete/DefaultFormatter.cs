using Domain.Model;
using Service.Formatters.Abstract;
using System.Text;

namespace Service.Formatters.Concrete
{
    public class DefaultFormatter : IFormatter
    {
        public string FormatResultsPerTerm(ResultsPerTerm resultsPerTerm)
        {
            var sb = new StringBuilder();

            foreach (var result in resultsPerTerm)
            {
                sb.AppendLine($"Results for '{result.Key}'");

                foreach (var searchResult in result.Value)
                {
                    sb.AppendLine($"\t{searchResult.SearchEngine}: {searchResult.ResultsCount}");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public string FormatTotalWinner(TotalWinner totalWinner)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Total winner: {totalWinner.SearchTerm}");

            return sb.ToString();
        }

        public string FormatWinnersPerEngine(WinnersPerEngine winnersPerEngine)
        {
            var sb = new StringBuilder();

            foreach (var winnerPerEngine in winnersPerEngine)
            {
                sb.AppendLine($"{winnerPerEngine.Key} winner: {winnerPerEngine.Value.SearchTerm}");
            }

            return sb.ToString();
        }
    }
}
