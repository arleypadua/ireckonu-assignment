using ImportFile.Core.Inventory.InventoryAggregate;
using ImportFile.SharedKernel.Messaging;

namespace ImportFile.Core.Inventory.UseCases.ImportCsvLine
{
    internal class SaveInventoryItemCommand : ICommand
    {
        public InventoryItem Item { get; set; }
    }
}