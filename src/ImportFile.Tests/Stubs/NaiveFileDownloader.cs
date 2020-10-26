using ImportFile.Core.Inventory.Ports;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImportFile.Tests.Stubs
{
    public class NaiveFileDownloader : IDownloadFiles
    {
        private const string FileContent = @"Key,ArtikelCode,ColorCode,Description,Price,DiscountPrice,DeliveredIn,Q1,Size,Color
2800104,2,broek,Gaastra,8,0,1-3 werkdagen,baby,104,grijs
00000002groe56,2,broek,Gaastra,8,0,1-3 werkdagen,baby,56,groen
00000002groe62,2,broek,Gaastra,8,0,1-3 werkdagen,baby,62,groen
00000002groe68,2,broek,Gaastra,8,0,1-3 werkdagen,baby,68,groen
00000002groe74,2,broek,Gaastra,8,0,1-3 werkdagen,baby,74,groen
00000002groe80,2,broek,Gaastra,8,0,1-3 werkdagen,baby,80,groen
00000002groe86,2,broek,Gaastra,88,0,1-3 werkdagen,baby,86,groen
00000002groe92,2,broek,Gaastra,8,0,1-3 werkdagen,baby,92,groen
";

        public Task<Stream> Download(string url)
        {
            FileDownloaded = true;
            Stream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(FileContent));
            return Task.FromResult(memoryStream);
        }

        public bool FileDownloaded { get; private set; }
    }
}