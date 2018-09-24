using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Searcher.Core;
using Searcher.Core.Models;
using Searcher.Core.SearchEngins;
using Searcher.DAL;
using Searcher.DAL.Entities;
using Searcher.DAL.Services;

namespace Searcher.Tests.CoreTests
{
    [TestClass]
    public class SearchServiceTests
    {
        [TestMethod]
        public void GetSearchResultsTest()
        {
            var testEngine = new Mock<ISearchEngine>();
            testEngine.Setup(m => m.DoSearch("test")).Returns(Task.FromResult(new SearchEngineResult
            {
                EngineName = "Test engine",
                SearchItems = new List<SearchItem>
                {
                    new SearchItem
                    {
                        Link = "test.com",
                        LinkText = "test"
                    }
                }
            }));
            var service = new SearchService(new List<ISearchEngine> {testEngine.Object});

            var results = service.GetSearchResults("test");

            Assert.IsTrue(results != null);
            Assert.IsTrue(results.SearchItems.Any());
            Assert.IsTrue(results.EngineName == "Test engine");
        }
    }
}
