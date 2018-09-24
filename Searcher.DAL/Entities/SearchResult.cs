using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher.DAL.Entities
{
    public class SearchResult
    {
        [Key]
        public int Id { get; set; }

        public string LinkName { get; set; }

        public string Link { get; set; }

        public SearchRequest SearchRequest { get; set; }
    }
}
