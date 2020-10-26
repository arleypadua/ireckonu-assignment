using MediatR;

namespace ImportFile.Core.Inventory.UseCases
{
    public static class ImportCsvFileUseCase
    {
        public class Arguments : IRequest<Unit>
        {
            public string FileUrl { get; set; }
            public bool ContainsHeader { get; set; }
        }
    }
}