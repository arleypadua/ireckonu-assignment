using System;
using System.Threading.Tasks;
using ImportFile.Core.Inventory.InventoryAggregate;

namespace ImportFile.Core.Inventory.Ports
{
    public interface IInventoryItemUnitOfWork
    {
        Task CreateOrUpdate(InventoryItem inventoryItem, Action<InventoryItem> update);
    }
}