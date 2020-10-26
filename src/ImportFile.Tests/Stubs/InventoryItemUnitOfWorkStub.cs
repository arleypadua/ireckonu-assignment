using ImportFile.Core.Inventory.InventoryAggregate;
using ImportFile.Core.Inventory.Ports;
using System;
using System.Threading.Tasks;

namespace ImportFile.Tests.Stubs
{
    public class InventoryItemUnitOfWorkStub : IInventoryItemUnitOfWork
    {
        public InventoryItem UnderlyingEntity { get; private set; }

        public Task CreateOrUpdate(InventoryItem inventoryItem, Action<InventoryItem> update)
        {
            if (UnderlyingEntity is null)
            {
                UnderlyingEntity = inventoryItem;
            }
            else
            {
                update(UnderlyingEntity);
            }

            return Task.CompletedTask;
        }

        public void SetUpExisting(InventoryItem item)
        {
            UnderlyingEntity = item;
        }
    }
}