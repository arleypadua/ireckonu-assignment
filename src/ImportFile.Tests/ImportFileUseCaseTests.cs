using FluentAssertions;
using ImportFile.Adapters;
using ImportFile.Core.Inventory.UseCases.ImportCsvFile;
using ImportFile.Tests.Stubs;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace ImportFile.Tests
{
    public class ImportFileUseCaseTests
    {
        [Test]
        public async Task GivenUrl_WhenImporting_FileShouldBeCreatedAndCommandsShouldBeSent()
        {
            var messageSender = new MessageSenderStub();
            var fileDownloader = new NaiveFileDownloader();
            var jsonWriter = new JsonIntoStreamWriter();

            var handler = new ImportCsvFileCommandHandler(messageSender, fileDownloader, jsonWriter);
            await handler.Handle(new ImportCsvFileCommand
            {
                ContainsHeader = true,
                FileUrl = "http://whatever.io"
            }, CancellationToken.None);

            messageSender.CommandsSent.Should().HaveCountGreaterThan(0);
            fileDownloader.FileDownloaded.Should().BeTrue();
        }
    }
}