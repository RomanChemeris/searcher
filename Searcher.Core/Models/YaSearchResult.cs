using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher.Core.Structures
{
    public struct YaSearchResult
    {
        public string DisplayUrl,
            CacheUrl,
            Title,
            Description,
            IndexedTime;

        public YaSearchResult(string url,
            string cacheUrl,
            string title,
            string description,
            string indexedTime)
        {
            this.DisplayUrl = url;
            this.CacheUrl = cacheUrl;
            this.Title = title;
            this.Description = description;
            this.IndexedTime = indexedTime;
        }
    }
}
