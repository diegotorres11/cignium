using Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Model
{
    public class SearchTerm
    {
        public IEnumerable<string> Terms { get; private set; }

        public SearchTerm(string[] searchTerms)
        {
            if (searchTerms.Length < 2)
            {
                throw new SearchTermException("Provide at least two seach terms to do the comparison");
            }

            Terms = ProcessSearchTerm(searchTerms);
        }

        private IEnumerable<string> ProcessSearchTerm(string[] searchTerm)
        {
            return searchTerm.AsEnumerable();
        }
    }
}
