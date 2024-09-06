using Application.Common.Services;
using MediatR;
using N5Application.DTO;
using N5Domain.DomainEvents;

namespace N5Application.Events;
public class PermissionCreatedDomainEventHandler : INotificationHandler<PermissionCreatedDomainEvent>
{
    private readonly IPublisherService _publisherService;

    public PermissionCreatedDomainEventHandler(IPublisherService publisherService)
    {
        _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
    }

    public async Task Handle(PermissionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid().ToString();

        await _publisherService.Publish(
            new PublishDto
            {
                Id = guid,
                OperationName = "Request"
            },
            cancellationToken);
    }
}