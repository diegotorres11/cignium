using System.Collections.Generic;

namespace Service.Configuration.Abstract
{
    public interface IConfiguration
    {
        string EngineName { get; }
        string EngineApiUrl { get; }

        IDictionary<string, object> Parameters { get; }
        IDictionary<string, object> Headers { get; }
    }
}
