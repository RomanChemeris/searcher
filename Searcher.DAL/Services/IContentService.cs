using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Searcher.DAL.Entities;

namespace Searcher.DAL.Services
{
    public interface IContentService
    {
        Task SaveSearchRequest(SearchRequest searchRequest);
        Task<SearchRequest> SearchInRequests(string searchText);
    }
}
