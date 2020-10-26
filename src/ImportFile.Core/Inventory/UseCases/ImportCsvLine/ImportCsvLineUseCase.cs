using ImportFile.Core.Inventory.AggregateRoot;
using ImportFile.SharedKernel.Messaging;

namespace ImportFile.Core.Inventory.UseCases.ImportCsvLine
{
    internal static class ImportCsvLineUseCase
    {
        public class Arguments : ICommand
        {
            public InventoryItem Item { get; set; }
        }
    }
}