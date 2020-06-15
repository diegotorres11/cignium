using Domain.Exceptions;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class SearchTermTest
    {
        [TestMethod]
        [ExpectedException(typeof(SearchTermException))]
        public void TestIncorrectSearchArguments()
        {
            var arguments = new string[] { ".net" };
            var searchTerm = new SearchTerm(arguments);
        }

        [TestMethod]
        public void TestCorrectSearchArguments()
        {
            var arguments = new string[] { ".net", "java", "python" };
            var searchTerm = new SearchTerm(arguments);

            Assert.AreEqual(arguments.Length, searchTerm.Terms.Count());
        }
    }
}
