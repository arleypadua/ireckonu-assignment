using ImportFile.Core.Inventory.Ports;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ImportFile.Core.Inventory.UseCases.SaveInventoryItem
{
    internal class SaveInventoryItemCommandHandler : IRequestHandler<SaveInventoryItemCommand>
    {
        private readonly IInventoryItemUnitOfWork _uow;

        public SaveInventoryItemCommandHandler(IInventoryItemUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(SaveInventoryItemCommand request, CancellationToken cancellationToken)
        {
            await _uow.CreateOrUpdate(request.Item, existing =>
            {
                existing.MergeWith(request.Item);
            });

            return Unit.Value;
        }
    }
}