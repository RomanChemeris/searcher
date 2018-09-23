using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Searcher.Core.Models
{
    public class BingResult
    {
        [JsonProperty(PropertyName = "webPages")]
        public BingWebPage WebPage { get; set; }
    }

    public class BingWebPage
    {
        [JsonProperty(PropertyName = "value")]
        public List<BingValue> Values { get; set; }
    }

    public class BingValue
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
