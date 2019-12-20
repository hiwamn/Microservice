using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Mongo
{
    public static class Extensions
    {
        public static void AddMongo(this IServiceCollection service, IConfiguration config)
        {
            service.Configure<MongoOptions>(config.GetSection("mongo"));
            service.AddSingleton<MongoClient>(c=>
            {
                var option = c.GetService<IOptions<MongoOptions>>();
                return new MongoClient(option.Value.ConnectionString);
            });

            service.AddSingleton<IMongoDatabase>(c=>
            {
                var option = c.GetService<IOptions<MongoOptions>>();
                var client = c.GetService<MongoClient>();
                return client.GetDatabase(option.Value.Database);
            });

            service.AddSingleton<IDatabaseInitializer,MongoInitializer>();
            service.AddSingleton<IDatabaseSeeder,MongoSeeder>();
        }
    }
}
