using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace BuildFeed.Model.View
{
    public class SitemapData
    {
        public IReadOnlyDictionary<string, IReadOnlyCollection<SitemapPagedAction>> Actions { get; set; }
        public IReadOnlyCollection<SitemapDataBuildGroup> Builds { get; set; }

        public IReadOnlyCollection<string> Labs { get; set; }
    }

    public class SitemapDataBuildGroup
    {
        public IReadOnlyCollection<SitemapDataBuild> Builds { get; set; }
        public BuildGroup Id { get; set; }
    }

    public class SitemapDataBuild
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class SitemapPagedAction
    {
        public string Action => UrlParams["action"].ToString();

        public string Name { get; set; }
        public int Pages { get; set; }

        public string UniqueId => UrlParams.GetHashCode().ToString("X8").ToLower();

        public RouteValueDictionary UrlParams { get; set; }
    }
}