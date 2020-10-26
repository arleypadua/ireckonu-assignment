using System.Threading.Tasks;
using MediatR;

namespace ImportFile.SharedKernel.Messaging
{
    public interface ISendMessages
    {
        Task SendCommand(IRequest command);
    }
}