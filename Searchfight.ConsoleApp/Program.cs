using Domain.Model;
using Service;
using Service.Calculator.Abstract;
using Service.Calculator.Concrete;
using Service.Factory;
using Service.Formatters.Abstract;
using Service.Formatters.Concrete;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Searchfight.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            var searchFactory = new SearchFactory();
            var searchTerm = new SearchTerm(args);
            ICalculator calculator = new DefaultCalculator();
            var searcher = new SearchService(searchTerm, calculator);
            searcher.AddSearchEngine(searchFactory.CreateSearcher(SearchEngine.Google));
            searcher.AddSearchEngine(searchFactory.CreateSearcher(SearchEngine.Bing));

            await searcher.Search();

            IFormatter formatter = new DefaultFormatter();
            var sb = new StringBuilder();

            sb.Append(formatter.FormatResultsPerTerm(searcher.ResultsPerTerm));
            sb.Append(formatter.FormatWinnersPerEngine(searcher.WinnersPerEngine));
            sb.Append(formatter.FormatTotalWinner(searcher.TotalWinner));

            Console.WriteLine(sb);
            Console.ReadLine();
        }
    }
}
