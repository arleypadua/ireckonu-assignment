using System.Threading;
using System.Threading.Tasks;

namespace ImportFile.SharedKernel.Persistence
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}