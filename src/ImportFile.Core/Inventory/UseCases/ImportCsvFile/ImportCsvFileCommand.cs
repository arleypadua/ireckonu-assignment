using ImportFile.SharedKernel.Messaging;

namespace ImportFile.Core.Inventory.UseCases.ImportCsvFile
{
    public class ImportCsvFileCommand : ICommand
    {
        public string FileUrl { get; set; }
        public bool ContainsHeader { get; set; }
    }
}