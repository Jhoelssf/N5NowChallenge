using Application.Common.Services;
using Moq;
using N5Application.DTO;
using N5Application.Events;
using N5Domain.DomainEvents;

namespace N5Test.Events
{
    public class PermissionUpdatedDomaindEventHandlerTest
    {
        private readonly Mock<IPublisherService> _publisher;

        private PermissionUpdatedDomainEventHandler _handler;

        public PermissionUpdatedDomaindEventHandlerTest()
        {
            _publisher = new Mock<IPublisherService>();
            _handler = new PermissionUpdatedDomainEventHandler(_publisher.Object);
        }

        [Fact]
        public async Task Handle_ValidUpdatedDomainEvent_CallsPublisherService()
        {
            // Arrange
            var domainEvent = new PermissionUpdatedDomainEvent();

            // Act
            await _handler.Handle(domainEvent, CancellationToken.None);

            // Assert
            _publisher.Verify(x => x.Publish(It.IsAny<PublishDto>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
