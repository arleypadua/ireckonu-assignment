using ImportFile.Core.Inventory.Ports;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImportFile.Adapters
{
    public class HttpClientFileDownloader : IDownloadFiles
    {
        private readonly IHttpClientFactory _factory;

        public const string ClientName = "file-downloader";

        public HttpClientFileDownloader(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public Task<Stream> Download(string url)
        {
            // using the factory avoids port exhaustion
            HttpClient client = _factory.CreateClient(ClientName);
            return client.GetStreamAsync(url);
        }
    }
}