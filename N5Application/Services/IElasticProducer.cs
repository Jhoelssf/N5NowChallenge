using N5Domain.Entities;

namespace Application.Common.Services
{
    public interface IElasticProducer
    {
        Task<string> IndexPermissionDocumentAsync(Permission permission, CancellationToken cancellationToken);
    }
}
