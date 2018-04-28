using System.Collections.Generic;

namespace BuildFeed.ViewModel
{
    public class SitemapData
    {
        public Dictionary<string, SitemapPagedAction[]> Actions { get; set; }
        public SitemapDataBuildGroup[] Builds { get; set; }

        public string[] Labs { get; set; }
    }
}