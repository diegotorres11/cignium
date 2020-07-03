using Domain.Model;
using Service.SearchEngines.Abstract;
using Service.SearchEngines.Concrete.Bing;
using Service.SearchEngines.Concrete.Google;

namespace Service.Factory
{
    public class SearchFactory
    {
        public Searcher CreateSearcher(SearchEngine engine)
        {
            switch (engine)
            {
                case SearchEngine.Google: return new GoogleSearcher();
                case SearchEngine.Bing: return new BingSearcher();
                default: return new GoogleSearcher();
            }
        }
    }
}
