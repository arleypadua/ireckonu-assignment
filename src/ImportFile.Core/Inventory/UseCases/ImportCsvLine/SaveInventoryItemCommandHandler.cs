﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ImportFile.Core.Inventory.UseCases.ImportCsvLine
{
    internal class SaveInventoryItemCommandHandler : IRequestHandler<SaveInventoryItemCommand>
    {
        public Task<Unit> Handle(SaveInventoryItemCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}