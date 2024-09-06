using Application.Common.Services;
using MediatR;
using N5Application.DTO;
using N5Domain.DomainEvents;

namespace N5Application.Events;

public class PermissionUpdatedDomainEventHandler : INotificationHandler<PermissionUpdatedDomainEvent>
{
    private readonly IPublisherService _publisher;

    public PermissionUpdatedDomainEventHandler(IPublisherService publisher)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public async Task Handle(PermissionUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid().ToString();

        await _publisher.Publish(new PublishDto
        {
            Id = guid,
            OperationName = "Modify"
        }, cancellationToken);
    }
}