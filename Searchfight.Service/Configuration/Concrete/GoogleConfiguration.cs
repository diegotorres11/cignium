using Service.Configuration.Abstract;
using System.Collections.Generic;
using System.Configuration;

namespace Service.Configuration.Concrete
{
    public class GoogleConfiguration : IConfiguration
    {
        public GoogleConfiguration()
        {
            Parameters = new Dictionary<string, object>()
            {
                { "key", ConfigurationManager.AppSettings["GoogleApiKey"] },
                { "cx",  ConfigurationManager.AppSettings["GoogleApiEngine"] },
                { "q", string.Empty }
            };

            Headers = new Dictionary<string, object>();
        }

        public string EngineName => ConfigurationManager.AppSettings["GoogleEngineName"];

        public string EngineApiUrl => ConfigurationManager.AppSettings["GoogleApiUrl"];

        public IDictionary<string, object> Parameters { get; }

        public IDictionary<string, object> Headers { get; }
    }
}
