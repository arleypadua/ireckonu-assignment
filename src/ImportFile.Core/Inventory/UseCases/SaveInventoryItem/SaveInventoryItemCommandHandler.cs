using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ImportFile.Core.Inventory.UseCases.SaveInventoryItem
{
    internal class SaveInventoryItemCommandHandler : IRequestHandler<SaveInventoryItemCommand>
    {
        private readonly ILogger<SaveInventoryItemCommandHandler> _logger;

        public SaveInventoryItemCommandHandler(ILogger<SaveInventoryItemCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task<Unit> Handle(SaveInventoryItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"processing {request.Item.ArtikelCode}");

            return Unit.Task;
        }
    }
}