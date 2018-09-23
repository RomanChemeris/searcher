using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Searcher.Core.Models;
using Searcher.Core.SearchEngins;

namespace Searcher.Core
{
    public class SearchService : ISearchService
    {
        public SearchEngineResult GetSearchResults(string searchStr)
        {
            SearchEngineResult result = null;

            var searchEngines = new List<ISearchEngine>
            {
                new GoogleSearchEngine(),
                new BingSearchEngine(),
                new YandexSearchEngine()
            };

            List<Task> searchTasks = new List<Task>();

            foreach (var searchEngine in searchEngines)
            {
                searchTasks.Add(Task.Run(async () => result = await searchEngine.DoSearch(searchStr)));
            }

            Task.WaitAny(searchTasks.ToArray());

            return result;
        }
    }
}
