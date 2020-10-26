using Ardalis.ApiEndpoints;
using ImportFile.Core.Inventory.UseCases;
using ImportFile.SharedKernel.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ImportFile.Api.Inventory
{
    [ApiController]
    [Route("inventory")]
    public class ImportCsvFile : BaseAsyncEndpoint
    {
        private readonly ISendMessages _messageSender;

        public ImportCsvFile(ISendMessages messageSender)
        {
            _messageSender = messageSender;
        }

        [Route("import/csv")]
        [HttpPost]
        public async Task<IActionResult> ImportCsvFileAction([FromBody] Input input)
        {
            // using an interface that abstracts the sending of a command in a queue.
            // the implementation provided here, relies on MediatR to implement this functionality without the need of using queues.
            // in practice this will complete synchronously, but the intention would be to put a command in a queue and respond: Accepted
            await _messageSender.SendCommand(new ImportCsvFileUseCase.Arguments
            {
                FileUrl = input.FileUrl,
                ContainsHeader = input.ContainsHeader
            });

            return Accepted();
        }

        public class Input
        {
            [Required]
            public string FileUrl { get; set; }

            public bool ContainsHeader { get; set; } = true;
        }
    }
}
