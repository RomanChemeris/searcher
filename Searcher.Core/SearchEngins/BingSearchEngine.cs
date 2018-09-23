using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.WebPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Searcher.Core.Models;

namespace Searcher.Core.SearchEngins
{
    public class BingSearchEngine : ISearchEngine
    {
        const string UriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";

        const string AccessKey = "c9f48ddcea524d08b28ce8184e7ba356";

        public async Task<SearchEngineResult> DoSearch(string textToSearch)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var uriQuery = UriBase + "?q=" + Uri.EscapeDataString(textToSearch);

            // Perform the Web request and get the response
            WebRequest request = WebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = AccessKey;
            HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();
            string json = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();

            var parsedJson = JsonConvert.DeserializeObject<BingResult>(json);
            stopwatch.Stop();

            return new SearchEngineResult
            {
                EngineName = "Bing",
                SearchItems = parsedJson.WebPage.Values.Select(x =>
                    new SearchItem
                    {
                        Link = x.Url,
                        LinkText = x.Name,
                    }
                ).ToList(),
                Ticks = stopwatch.ElapsedMilliseconds
            };
        }
    }
}
