using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Util
{
    public class WebServiceConsumer
    {
        private static volatile HttpClient _client;
        private static object _locker = new object();

        private static HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    lock (_locker)
                    {
                        if (_client == null)
                        {
                            _client = new HttpClient();
                        }
                    }
                }

                return _client;
            }
        }

        public static async Task<T> Consume<T>(string url, IDictionary<string, object> headers = null)
        {
            return await ConsumeAsync<T>(new Uri(url), headers);
        }

        public static async Task<T> ConsumeAsync<T>(Uri url, IDictionary<string, object> headers = null)
        {
            ClearHeaders();

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    Client.DefaultRequestHeaders.Add(header.Key, header.Value.ToString());
                }
            }

            string resultString = await Client.GetStringAsync(url);
            T result = JsonConvert.DeserializeObject<T>(resultString);

            return result;
        }

        private static void ClearHeaders()
        {
            Client.DefaultRequestHeaders.Clear();
        }
    }
}
