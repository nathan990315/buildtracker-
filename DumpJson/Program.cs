using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BuildFeed.Model;
using Newtonsoft.Json;

namespace DumpJson
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var bRepo = new BuildRepository();
            var builds = (await bRepo.Select()).Select(b => new
            {
                b.MajorVersion,
                b.MinorVersion,
                b.Number,
                b.Revision,
                b.Lab,
                b.BuildTime,

                b.Family,

                b.SourceType,
                b.SourceDetails,
                b.LeakDate,

                b.FullBuildString,
                b.AlternateBuildString,
            });

            string outJson = JsonConvert.SerializeObject(builds, Formatting.Indented);

            using (TextWriter writer = File.CreateText("output.json"))
            {
                writer.Write(outJson);
            }
        }
    }
}
