using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher.Core.Models
{
    public class SearchEngineResult
    {
        public string EngineName { get; set; }

        public long Ticks { get; set; }

        public List<SearchItem> SearchItems { get; set; }
    }
}
