namespace BuildFeed.ViewModel
{
    public class FrontPage
    {
        public FrontPageBuild CurrentCanary { get; set; }
        public FrontPageBuild CurrentInsider { get; set; }
        public FrontPageBuild CurrentRelease { get; set; }
        public FrontPageBuild CurrentXbox { get; set; }
        public FrontPageBuild CurrentAnalog { get; set; }
    }
}