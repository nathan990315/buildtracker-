﻿using System;
using BuildFeed.Model;

namespace BuildFeed.ViewModel
{
    public class FrontBuildGroup
    {
        public int BuildCount { get; set; }
        public BuildGroup Key { get; set; }
        public DateTime? LastBuild { get; set; }
    }
}