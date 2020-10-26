using System.IO;
using System.Threading.Tasks;

namespace ImportFile.Core.Inventory.Ports
{
    public interface IDownloadFiles
    {
        Task<Stream> Download(string url);
    }
}