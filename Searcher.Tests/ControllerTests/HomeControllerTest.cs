using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Searcher;
using Searcher.Controllers;
using Searcher.Core;
using Searcher.Core.Models;
using Searcher.DAL.Entities;
using Searcher.DAL.Services;
using Searcher.Models;
using Searcher.Tests.Helpers;

namespace Searcher.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        HomeController Controller { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            var mockContentService = new Mock<IContentService>();
            var mockSearchService = new Mock<ISearchService>();

            mockSearchService.Setup(s => s.GetSearchResults(It.IsAny<string>())).Returns(new SearchEngineResult
                {
                    EngineName = "TestEngine",
                    SearchItems = new List<SearchItem>
                    {
                        new SearchItem
                        {
                            Link = "test.com",
                            LinkText = "test link"
                        }
                    },
                    Ticks = 0
                });
            mockContentService.Setup(s => s.SaveSearchRequest(It.IsAny<SearchRequest>())).Returns(Task.CompletedTask);
            mockContentService.Setup(s => s.SearchInRequests(It.IsAny<string>())).Returns(Task.FromResult(
                new SearchRequest
                {
                    SearchResults = new List<SearchResult>
                    {
                        new SearchResult
                        {
                            Link = "test.com",
                            LinkName = "test link"
                        }
                    }
                }));
            

            Controller = new HomeController(mockContentService.Object, mockSearchService.Object);
            Controller.SetFakeContext();
        }

        [TestMethod]
        public void IndexTest()
        {
            ViewResult result = Controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SearchInResultsTest()
        {
            ViewResult result = Controller.SearchInResults() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DoSearchTest()
        {
            var result = Controller.DoSearch(new SearchModel{ SearchText = "test"});
            result.Wait();

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void DoSearchInResultsTest()
        {
            var result = Controller.DoSearchInResults(new SearchModel { SearchText = "test" });
            result.Wait();

            Assert.IsNotNull(result.Result);
        }
    }
}
