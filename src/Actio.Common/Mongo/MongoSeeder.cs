using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace Actio.Common.Mongo
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase _database;

        public MongoSeeder(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task SeedAsync()
        {
            var collectionCursor = await _database.ListCollectionsAsync();
            var collections = await collectionCursor.ToListAsync();
            if (collections.Any())
            {
                return;
            }
            await CusomSeedAsync();
        }

        protected virtual async Task CusomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}
