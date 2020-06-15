using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Extensions
{
    public static class DictionaryExtensions
    {
        public static string ToQueryString(this IDictionary<string, object> dic)
        {
            if (dic == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            foreach (var item in dic)
            {
                sb.Append($"{item.Key}={item.Value}&");
            }

            sb.Length--;

            return sb.ToString();
        }
    }
}
