using System.Threading;
using System.Threading.Tasks;

namespace TrackingCode.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> CompleteAsync(CancellationToken cancellationToken = default);
    }
}