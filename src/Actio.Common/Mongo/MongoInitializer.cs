using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Actio.Common.Mongo
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private bool _initialized;
        private readonly IMongoDatabase _database;
        private readonly IDatabaseSeeder _databaseSeeder;
        private readonly bool _seed;

        public MongoInitializer(IMongoDatabase database, IOptions<MongoOptions> options,IDatabaseSeeder databaseSeeder)
        {
            _database = database;
            _databaseSeeder = databaseSeeder;
            _seed = options.Value.Seed;
        }
        public async Task InitialAsync()
        {
            if (_initialized)
                return;
            RegisterConventions();
            _initialized = true;
            if (!_seed)
            {
                return;
            }

                await _databaseSeeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("ActioConventions", new MongoConvention(), x => true);
        }
    }
}
