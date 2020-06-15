using Service.Configuration.Abstract;
using System.Collections.Generic;
using System.Configuration;

namespace Service.Configuration.Concrete
{
    public class BingConfiguration : IConfiguration
    {
        public BingConfiguration()
        {
            Parameters = new Dictionary<string, object>()
            {
                { "q", string.Empty }
            };

            Headers = new Dictionary<string, object>()
            {
                { ConfigurationManager.AppSettings["BingSubscriptionKeyName"], ConfigurationManager.AppSettings["BingSubscriptionKeyValue"] }
            };
        }

        public string EngineName => ConfigurationManager.AppSettings["BingEngineName"];

        public string EngineApiUrl => ConfigurationManager.AppSettings["BingApiUrl"];

        public IDictionary<string, object> Parameters { get; }

        public IDictionary<string, object> Headers { get; }
    }
}
