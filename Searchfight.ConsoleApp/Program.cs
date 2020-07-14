using Domain.Model;
using Service;
using Service.Calculator.Abstract;
using Service.Calculator.Concrete;
using Service.Factory;
using Service.Formatters.Abstract;
using Service.Formatters.Concrete;
using Service.SearchEngines.Abstract;
using System;
using System.Collections.Generic;
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
            var searchTerm = new SearchTerm(args);
            var searchers = GetSearchers();
            ICalculator calculator = new DefaultCalculator();
            var searcher = new SearchService(searchTerm, searchers, calculator);

            await searcher.Search();

            IFormatter formatter = new DefaultFormatter();
            var sb = new StringBuilder();

            sb.Append(formatter.FormatResultsPerTerm(searcher.ResultsPerTerm));
            sb.Append(formatter.FormatWinnersPerEngine(searcher.WinnersPerEngine));
            sb.Append(formatter.FormatTotalWinner(searcher.TotalWinner));

            Console.WriteLine(sb);
            Console.ReadLine();
        }

        static List<Searcher> GetSearchers()
        {
            var searchFactory = new SearchFactory();

            return new List<Searcher>
            {
                searchFactory.CreateSearcher(SearchEngine.Google),
                searchFactory.CreateSearcher(SearchEngine.Bing)
            };
        }
    }
}
