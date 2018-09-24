using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Searcher.Core;
using Searcher.Core.Models;
using Searcher.DAL.Entities;
using Searcher.DAL.Services;
using Searcher.Models;

namespace Searcher.Controllers
{
    public class HomeController : Controller
    {
        public IContentService ContentService { get; }
        public ISearchService SearchService { get; }

        public HomeController(IContentService contentService, ISearchService searchService)
        {
            ContentService = contentService;
            SearchService = searchService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> DoSearch(SearchModel search)
        {
            var searchResult = SearchService.GetSearchResults(search.SearchText);
            await ContentService.SaveSearchRequest(new SearchRequest
            {
                SearchText = search.SearchText,
                Ip = Request.UserHostAddress,
                SearchEngine = searchResult.EngineName,
                Date = DateTime.Now,
                SearchResults = searchResult.SearchItems.Select(x => new SearchResult
                {
                    Link = x.Link,
                    LinkName = x.LinkText
                }).ToList()
            });

            return View(searchResult);
        }

        public ActionResult SearchInResults()
        {
            return View();
        }

        public async Task<ActionResult> DoSearchInResults(SearchModel search)
        {
            return View(await ContentService.SearchInRequests(search.SearchText));
        }
    }
}