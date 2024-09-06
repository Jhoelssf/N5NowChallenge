using Application.Common.Services;
using MediatR;
using N5Application.DTO;
using N5Domain.DomainEvents;

namespace N5Application.Events;

public class PermissionsGetDomainEventHandler : INotificationHandler<PermissionsGetDomainEvent>
{
    private readonly IPublisherService _publisher;

    public PermissionsGetDomainEventHandler(IPublisherService publisher)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public async Task Handle(PermissionsGetDomainEvent notification, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid().ToString();

        await _publisher.Publish(new PublishDto
        {
            Id = guid,
            OperationName = "Get"
        }, cancellationToken);
    }
}