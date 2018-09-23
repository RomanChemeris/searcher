using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Searcher.Core.Models;

namespace Searcher.Core.SearchEngins
{
    public interface ISearchEngine
    {
        Task<SearchEngineResult> DoSearch(string textToSearch);
    }
}
