namespace BuildFeed.Model
{
    public class MongoConfig
    {
        public string Host { get; }
        public int Port { get; }
        public string Database { get; }
        public string Username { get; }
        public string Password { get; }

        public MongoConfig(string host, int? port, string database, string username, string password)
        {
            Host = !string.IsNullOrEmpty(host)
                ? host
                : "localhost";

            if (!port.HasValue)
            {
                port = 27017; // mongo default port
            }

            Port = port.Value;

            Database = !string.IsNullOrEmpty(database)
                ? database
                : "MongoAuth";

            Username = username ?? "";
            Password = password ?? "";
        }

        public void SetupIndexes()
        {
            var b = new BuildRepository(this);
            #pragma warning disable 4014
            b.SetupIndexes();
            #pragma warning restore 4014
        }
    }
}