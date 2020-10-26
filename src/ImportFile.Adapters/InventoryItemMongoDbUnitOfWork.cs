using ImportFile.Core.Inventory.InventoryAggregate;
using ImportFile.Core.Inventory.Ports;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace ImportFile.Adapters
{
    public class InventoryItemMongoDbUnitOfWork : IInventoryItemUnitOfWork
    {
        internal const string DatabaseName = "import-file-db";
        internal const string CollectionName = "inventory-item";

        private readonly IMongoCollection<InventoryItem> _collection;

        public InventoryItemMongoDbUnitOfWork(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(DatabaseName);
            _collection = database.GetCollection<InventoryItem>(CollectionName);
        }

        public async Task CreateOrUpdate(InventoryItem inventoryItem, Action<InventoryItem> update)
        {
            var cursor = await _collection
                .FindAsync(f => f.InventoryFileId == inventoryItem.InventoryFileId && f.Key == inventoryItem.Key);

            InventoryItem existing = await cursor.SingleOrDefaultAsync();

            if (existing is null)
            {
                await _collection.InsertOneAsync(inventoryItem);
            }
            else
            {
                update(existing);

                await _collection.ReplaceOneAsync(
                    i => i.Id == existing.Id,
                    existing);
            }
        }
    }
}