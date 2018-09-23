using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Searcher.DAL;
using Searcher.DAL.Entities;
using Searcher.DAL.Services;

namespace Searcher.Tests.Database
{
    [TestClass]
    public class ContentServiceTests
    {
        [TestMethod]
        public async Task CreateBlog_saves_a_blog_via_context()
        {
            var mockSet = new Mock<DbSet<SearchRequest>>();

            var mockContext = new Mock<SearcherContext>();
            mockContext.Setup(m => m.SearchRequests).Returns(mockSet.Object);

            var service = new ContentService(mockContext.Object);
            await service.SaveSearchRequest(new SearchRequest());

            mockSet.Verify(m => m.Add(It.IsAny<SearchRequest>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
