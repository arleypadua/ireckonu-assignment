using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ImportFile.Core.Inventory.UseCases
{
    internal class ImportCsvFileHandler : IRequestHandler<ImportCsvFileUseCase.Arguments>
    {
        public ImportCsvFileHandler()
        {
            
        }

        public Task<Unit> Handle(ImportCsvFileUseCase.Arguments request, 
            CancellationToken cancellationToken)
        {
            // todo
            throw new System.NotImplementedException();
        }
    }
}