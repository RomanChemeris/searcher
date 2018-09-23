using System;
using System.Collections.Generic;
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
        const string ApiKey = "AIzaSyBKUAUh7C47XcVP8lPxfnNOmQsnjJHHM8M";
        const string SearchEngineId = "014860650480985424406:idqtrmeeocs";

        public async Task<SearchEngineResult> DoSearch(string textToSearch)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = ApiKey });
            var listRequest = customSearchService.Cse.List(textToSearch);
            listRequest.Cx = SearchEngineId;
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
