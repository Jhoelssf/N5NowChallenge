using N5Application.DTO;

namespace Application.Common.Services
{
    public interface IPublisherService
    {
        Task Publish(PublishDto publishDto, CancellationToken cancellationToken);
    }
}
