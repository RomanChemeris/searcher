using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Searcher.DAL.Entities;

namespace Searcher.DAL.Services
{
    public class ContentService : IContentService
    {
        private SearcherContext _context { get; set; }

        public ContentService(SearcherContext context)
        {
            _context = context;
        }

        public async Task SaveSearchRequest(SearchRequest searchRequest)
        {
            _context.SearchRequests.Add(searchRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<SearchRequest> SearchInRequests(string searchText)
        {
            return await _context.SearchRequests.Include(x => x.SearchResults)
                .Where(x => x.SearchText.Contains(searchText))
                .FirstOrDefaultAsync();
        }
    }
}
