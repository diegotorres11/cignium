using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service;
using Service.Calculator.Abstract;
using Service.Calculator.Concrete;
using Service.Formatters.Abstract;
using Service.Formatters.Concrete;
using Service.SearchEngines.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class SearcherTest
    {
        [TestMethod]
        public void TestMainSearcher()
        {
            //Arrange
            var arguments = new string[] { "java", ".net" };
            var searchTerm = new SearchTerm(arguments);
            ICalculator calculator = new DefaultCalculator();
            var yahooJavaSearchResult = new SearchResult { ResultsCount = 100, SearchEngine = "Yahoo", SearchTerm = "java" };
            var yahooDotNetSearchResult = new SearchResult { ResultsCount = 20, SearchEngine = "Yahoo", SearchTerm = ".net" };
            var yandexJavaSearchResult = new SearchResult { ResultsCount = 50, SearchEngine = "Yandex", SearchTerm = "java" };
            var yandexDotNetSearchResult = new SearchResult { ResultsCount = 80, SearchEngine = "Yandex", SearchTerm = ".net" };

            var moqYahooSearcher = new Mock<Searcher>();
            moqYahooSearcher.Setup(s => s.Search(It.Is<string>(term => term == "java"))).ReturnsAsync(yahooJavaSearchResult);
            moqYahooSearcher.Setup(s => s.Search(It.Is<string>(term => term == ".net"))).ReturnsAsync(yahooDotNetSearchResult);

            var moqYandexSearcher = new Mock<Searcher>();
            moqYandexSearcher.Setup(s => s.Search(It.Is<string>(term => term == "java"))).ReturnsAsync(yandexJavaSearchResult);
            moqYandexSearcher.Setup(s => s.Search(It.Is<string>(term => term == ".net"))).ReturnsAsync(yandexDotNetSearchResult);

            var searchers = new List<Searcher> { moqYahooSearcher.Object, moqYandexSearcher.Object };
            SearchService mainSearcher = new SearchService(searchTerm, searchers, calculator);

            //Act
            mainSearcher.Search().Wait();

            //Assert
            var searchResults = mainSearcher.SearchResults.ToArray();

            Assert.AreSame(yahooJavaSearchResult, searchResults[0]);
            Assert.AreSame(yandexJavaSearchResult, searchResults[2]);

            //Winner per engine
            Assert.AreEqual("java", mainSearcher.WinnersPerEngine["Yahoo"].SearchTerm);
            Assert.AreEqual(".net", mainSearcher.WinnersPerEngine["Yandex"].SearchTerm);

            //Total winner
            Assert.AreEqual("java", mainSearcher.TotalWinner.SearchTerm);
            Assert.AreEqual(150, mainSearcher.TotalWinner.SearchResultsCount);
        }
    }
}
