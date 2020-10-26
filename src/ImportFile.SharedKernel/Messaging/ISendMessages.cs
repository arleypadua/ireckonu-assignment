using System.Threading.Tasks;

namespace ImportFile.SharedKernel.Messaging
{
    public interface ISendMessages
    {
        Task SendCommand();
    }
}