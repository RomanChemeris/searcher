using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Searcher.DAL.Entities;

namespace Searcher.DAL
{
    public class SearcherContext : DbContext
    {
        public SearcherContext() : base("SearcherConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SearcherContext>());
        }

        public DbSet<SearchRequest> SearchRequests { get; set; }
        public DbSet<SearchResult> SearchResults { get; set; }
    }
}
