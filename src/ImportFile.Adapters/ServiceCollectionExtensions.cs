using ImportFile.Core.Inventory.InventoryAggregate;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ImportFile.Adapters
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureMongoDb(this IServiceCollection services, string connectionString)
        {
            // this relies on a local mongo db instance
            services.AddSingleton<IMongoClient, MongoClient>(
                serviceProvider => ConfigureClient(connectionString));

            return services;
        }

        private static MongoClient ConfigureClient(string connectionString)
        {
            MongoClient client = new MongoClient(connectionString);

            BsonClassMap.RegisterClassMap<InventoryItem>(cm =>
            {
                cm.AutoMap();
            });

            IMongoDatabase database = client.GetDatabase(InventoryItemMongoDbUnitOfWork.DatabaseName);
            IMongoCollection<InventoryItem> collection = database.GetCollection<InventoryItem>(InventoryItemMongoDbUnitOfWork.CollectionName);
            collection.Indexes.CreateOne(new CreateIndexModel<InventoryItem>(
                new JsonIndexKeysDefinition<InventoryItem>("{ InventoryFileId: 1, Key: 1 }")));

            return client;
        }
    }
}