using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildFeed.Model.Api;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace BuildFeed.Model
{
    public partial class BuildRepository
    {
        public Task<IReadOnlyCollection<ProjectFamily>> SelectAllFamilies(int limit = -1, int skip = 0) => Task.Run(() =>
        {
            Array values = Enum.GetValues(typeof(ProjectFamily));
            if (values.Length == 0)
            {
                return (IReadOnlyCollection<ProjectFamily>)Array.Empty<ProjectFamily>();
            }

            var valuesWithoutNone = new ProjectFamily[values.Length - 1];
            for (int i = 0,
                j = values.Length - 1;
                j > 0;
                j--, i++)
            {
                valuesWithoutNone[i] = (ProjectFamily)values.GetValue(j);
            }

            return (IReadOnlyCollection<ProjectFamily>)valuesWithoutNone;
        });

        public Task<long> SelectAllFamiliesCount() => Task.Run(() => Enum.GetValues(typeof(ProjectFamily)).LongLength);

        public async Task<IReadOnlyCollection<Build>> SelectFamily(ProjectFamily family, int limit = -1, int skip = 0)
        {
            var query = _buildCollection.Find(new BsonDocument(nameof(Build.Family), family))
                .Sort(sortByOrder)
                .Skip(skip);

            if (limit > 0)
            {
                query = query.Limit(limit);
            }

            return await query.ToListAsync();
        }

        public async Task<long> SelectFamilyCount(ProjectFamily family)
            => await _buildCollection.CountDocumentsAsync(new BsonDocument(nameof(Build.Family), family));

        public async Task<IReadOnlyCollection<FamilyOverview>> SelectFamilyOverviews()
        {
            var families = _buildCollection.Aggregate()
                .Sort(sortByOrder)
                .Group(new BsonDocument
                {
                    new BsonElement("_id", $"${nameof(Build.Family)}"),
                    new BsonElement(nameof(FamilyOverview.Count), new BsonDocument("$sum", 1)),
                    new BsonElement(nameof(FamilyOverview.Latest), new BsonDocument("$first", "$$CURRENT"))
                })
                .Sort(new BsonDocument("_id", -1));

            var result = await families.ToListAsync();

            return (from o in result
                select BsonSerializer.Deserialize<FamilyOverview>(o)).ToList();
        }
    }
}