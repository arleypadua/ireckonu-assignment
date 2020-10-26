using ImportFile.Core.Inventory.Ports;
using ImportFile.Core.Inventory.UseCases.ImportCsvLine;
using ImportFile.SharedKernel.Messaging;
using MediatR;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ImportFile.Core.Inventory.InventoryAggregate;

namespace ImportFile.Core.Inventory.UseCases.ImportCsvFile
{
    internal class ImportCsvFileCommandHandler : IRequestHandler<ImportCsvFileCommand>
    {
        private readonly ISendMessages _messageSender;
        private readonly IDownloadFiles _fileDownloader;
        private readonly IWriteJsonIntoStreams _jsonStreamWriter;

        public ImportCsvFileCommandHandler(
            ISendMessages messageSender,
            IDownloadFiles fileDownloader,
            IWriteJsonIntoStreams jsonStreamWriter)
        {
            _messageSender = messageSender;
            _fileDownloader = fileDownloader;
            _jsonStreamWriter = jsonStreamWriter;
        }
        
        // this handler is optimized to read big files and generate the json file as we read through the csv file.
        // another option would also separate these two processes by only parsing the file here, and asynchronously, each inventory item would create its own json file
        public async Task<Unit> Handle(ImportCsvFileCommand message, 
            CancellationToken cancellationToken)
        {
            using var inventoryFileStream = await _fileDownloader.Download(message.FileUrl);
            using var inventoryStreamReader = new StreamReader(inventoryFileStream);

            // todo: instead of storing the file locally, we could generate a stream in memory and upload it to Azure Blob Storage
            using var localFileWriter = new StreamWriter(Path.Combine(
                Environment.CurrentDirectory, $"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}_{Guid.NewGuid()}.json"));

            await _jsonStreamWriter.WriteArrayStartToken(localFileWriter);

            if (message.ContainsHeader)
            {
                await inventoryStreamReader.ReadLineAsync();
            }

            while (!inventoryStreamReader.EndOfStream)
            {
                string line = await inventoryStreamReader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] lineData = line.Split(',');
                if (lineData.All(string.IsNullOrWhiteSpace))
                {
                    continue;
                }

                InventoryItem item = InventoryItemFactory.FromCsvLine(lineData, message.FileUrl);

                await Task.WhenAll(
                    SendSaveInventoryItemCommand(item),
                    WriteInventoryItemAsJsonIntoStream(localFileWriter, item));

                await _jsonStreamWriter.WriteArraySeparatorIf(() => !inventoryStreamReader.EndOfStream, 
                    localFileWriter);
            }

            await _jsonStreamWriter.WriteArrayEndToken(localFileWriter);
            
            return Unit.Value;
        }

        private Task SendSaveInventoryItemCommand(InventoryItem item)
        {
            // this would send a message in a queue, and storing the inventory item in the database happens eventually in a different moment.
            return _messageSender.SendCommand(new SaveInventoryItemCommand.Arguments {Item = item});
        }

        private Task WriteInventoryItemAsJsonIntoStream(StreamWriter stream, InventoryItem item)
        {
            return _jsonStreamWriter.WriteSerialized(item, stream);
        }
    }
}