using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Util
{
    public class WebServiceConsumer
    {
        public static async Task<T> Consume<T>(string url, IDictionary<string, object> headers = null)
        {
            return await ConsumeAsync<T>(new Uri(url), headers);
        }

        public static async Task<T> ConsumeAsync<T>(Uri url, IDictionary<string, object> headers = null)
        {
            var client = new HttpClient();

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value.ToString());
                }
            }

            string resultString = await client.GetStringAsync(url);
            T result = JsonConvert.DeserializeObject<T>(resultString);

            return result;
        }
    }
}
