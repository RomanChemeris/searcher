using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Searcher.Core;
using Searcher.Core.Models;
using Searcher.DAL;
using Searcher.DAL.Entities;
using Searcher.DAL.Services;
using Searcher.Tests.Helpers;

namespace Searcher.Tests.Database
{
    [TestClass]
    public class ContentServiceTests
    {
        [TestMethod]
        public async Task SaveSearchRequestTest()
        {
            var mockSet = new Mock<DbSet<SearchRequest>>();

            var mockContext = new Mock<SearcherContext>();
            mockContext.Setup(m => m.SearchRequests).Returns(mockSet.Object);

            var service = new ContentService(mockContext.Object);
            await service.SaveSearchRequest(new SearchRequest
            {
                Date = DateTime.Now,
                SearchResults = new List<SearchResult>(),
                SearchEngine = "test"
            });

            mockSet.Verify(m => m.Add(It.IsAny<SearchRequest>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task SearchInRequestsTest()
        {
            var list = new List<SearchRequest>()
            {
                new SearchRequest
                {
                    Id = 1,
                    SearchText = "test"
                }
            };

            var mockSet = list.AsQueryable().BuildMockDbSet();
            var mockContext = new Mock<SearcherContext>();
            
            mockContext.Setup(m => m.SearchRequests).Returns(mockSet);
            var service = new ContentService(mockContext.Object);

            var result = await service.SearchInRequests("test");

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.SearchText == "test");
        }
    }
}
