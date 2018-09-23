using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher.DAL.Entities
{
    public class SearchRequest
    {
        [Key]
        public int Id { get; set; }

        public string SearchText { get; set; }

        public string SearchEngine { get; set; }

        public string Ip { get; set; }

        public DateTime Date { get; set; }

        public List<SearchResult> SearchResults { get; set; }
    }
}
