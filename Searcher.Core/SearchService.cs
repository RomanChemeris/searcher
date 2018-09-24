using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Searcher.Core.Models;
using Searcher.Core.SearchEngins;
using Unity.Attributes;

namespace Searcher.Core
{
    public class SearchService : ISearchService
    {
        private List<ISearchEngine> Engines { get; set; }

        public SearchService(List<ISearchEngine> searchEngines)
        {
            Engines = searchEngines;
        }

        [InjectionConstructor]
        public SearchService()
        {
            Engines = new List<ISearchEngine>
            {
                new GoogleSearchEngine(),
                new BingSearchEngine(),
                new YandexSearchEngine()
            };
        }

        public SearchEngineResult GetSearchResults(string searchStr)
        {
            SearchEngineResult result = null;

            List<Task> searchTasks = new List<Task>();

            foreach (var searchEngine in Engines)
            {
                searchTasks.Add(Task.Run(async () => result = await searchEngine.DoSearch(searchStr)));
            }

            Task.WaitAny(searchTasks.ToArray());

            return result;
        }
    }
}
