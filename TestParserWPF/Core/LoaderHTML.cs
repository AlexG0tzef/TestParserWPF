using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestParserWPF.Core
{
    public class LoaderHTML
    {
        public readonly HttpClient httpClient;

        public LoaderHTML()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> GetSourceByUrl(string url)
        {
            var response = await httpClient.GetAsync(url);
            var source = "";
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }
            return source;
        }
    }
}
