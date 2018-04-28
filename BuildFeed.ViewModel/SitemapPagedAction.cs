using System.Collections.Generic;

namespace BuildFeed.ViewModel
{
    public class SitemapPagedAction
    {
        public string Action => UrlParams["action"].ToString();

        public string Name { get; set; }
        public int Pages { get; set; }

        public string UniqueId => UrlParams.GetHashCode().ToString("X8").ToLower();

        public Dictionary<string, object> UrlParams { get; set; }
    }
}