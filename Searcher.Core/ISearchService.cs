using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Searcher.Core.Models;

namespace Searcher.Core
{
    public interface ISearchService
    {
        SearchEngineResult GetSearchResults(string searchStr);
    }
}
