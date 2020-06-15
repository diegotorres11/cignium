using Domain.Model;
using Service.Configuration.Abstract;
using Service.Extensions;
using System;
using System.Threading.Tasks;

namespace Service.SearchEngines.Abstract
{
    public abstract class Searcher
    {
        protected IConfiguration Config { get; set; }
        protected string EngineName => Config.EngineName;
        protected Uri Url => new Uri(Config.EngineApiUrl + "?" + Config.Parameters.ToQueryString());

        public abstract Task<SearchResult> Search(string term);
    }
}