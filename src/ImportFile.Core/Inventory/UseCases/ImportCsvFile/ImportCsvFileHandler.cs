using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ImportFile.Core.Inventory.AggregateRoot;
using ImportFile.Core.Inventory.Ports;
using ImportFile.Core.Inventory.UseCases.ImportCsvLine;
using ImportFile.SharedKernel.Messaging;
using MediatR;

namespace ImportFile.Core.Inventory.UseCases.ImportCsvFile
{
    internal class ImportCsvFileHandler : IRequestHandler<ImportCsvFileUseCase.Arguments>
    {
        private readonly ISendMessages _messageSender;
        private readonly IDownloadFiles _fileDownloader;

        public ImportCsvFileHandler(ISendMessages messageSender,
            IDownloadFiles fileDownloader)
        {
            _messageSender = messageSender;
            _fileDownloader = fileDownloader;
        }

        public async Task<Unit> Handle(ImportCsvFileUseCase.Arguments request, 
            CancellationToken cancellationToken)
        {
            using var fileStream = await _fileDownloader.Download(request.FileUrl);
            using var fileStreamReader = new StreamReader(fileStream);

            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream);
            
            await streamWriter.WriteAsync("[");

            if (request.ContainsHeader)
            {
                await fileStreamReader.ReadLineAsync();
            }

            while (!fileStreamReader.EndOfStream)
            {
                string line = await fileStreamReader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                InventoryItem item = InventoryItemFactory.FromCsvLine(line, request.FileUrl);

                await Task.WhenAll(
                    SendSaveInventoryItemCommand(item),
                    WriteJsonIntoStream(streamWriter, item));

                if (!fileStreamReader.EndOfStream)
                {
                    await streamWriter.WriteAsync(",");
                }
            }

            await streamWriter.WriteAsync("]");

            return Unit.Value;
        }

        private Task SendSaveInventoryItemCommand(InventoryItem item)
        {
            return _messageSender.SendCommand(new ImportCsvLineUseCase.Arguments {Item = item});
        }

        private Task WriteJsonIntoStream(StreamWriter stream, InventoryItem item)
        {
            return stream.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(item));
        }
    }
}