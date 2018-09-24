using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using Searcher.Core.Models;

namespace Searcher.Core.SearchEngins
{
    public class GoogleSearchEngine : ISearchEngine
    {
        public async Task<SearchEngineResult> DoSearch(string textToSearch)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = ConfigurationManager.AppSettings["GoogleApiKey"] });
            var listRequest = customSearchService.Cse.List(textToSearch);
            listRequest.Cx = ConfigurationManager.AppSettings["GoogleSearchEngineId"];
            var resultItems = await listRequest.ExecuteAsync();
            stopwatch.Stop();

            return new SearchEngineResult
            {
                EngineName = "Google",
                SearchItems = resultItems.Items.Select(x => new SearchItem
                {
                    Link = x.Link,
                    LinkText = x.Title
                }).ToList(),
                Ticks = stopwatch.ElapsedMilliseconds
            };
        }
    }
}
