using Application.Common.Services;
using FluentAssertions;
using Moq;
using N5Application.Commands;
using N5Domain.Entities;
using N5Domain.Repositories;
using System.Net;

namespace N5Test.CommandService
{
    public class UpdatePermissionTest
    {
        private readonly Mock<IPermissionRepository> _repository;
        private readonly Mock<IPermissionTypeRepository> _permissionTypeRepository;
        private readonly Mock<IPublisherService> _publisherService;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IElasticProducer> _elasticProducer;
        private readonly UpdatePermissionCommandHandler _handler;
        private CancellationToken _cancellationToken;

        public UpdatePermissionTest()
        {
            //Arrange
            _repository = new Mock<IPermissionRepository>();
            _repository.Setup(x => x.GetById(1))
                .ReturnsAsync(
                    new Permission(1, "Prueba", "Uno", DateTime.UtcNow, 1));
            _permissionTypeRepository = new Mock<IPermissionTypeRepository>();
            _permissionTypeRepository.Setup(x => x.GetById(1)).ReturnsAsync(new PermissionType(1, "User"));
            _publisherService = new Mock<IPublisherService>();
            _elasticProducer = new Mock<IElasticProducer>();

            _unitOfWork = new Mock<IUnitOfWork>();
            _cancellationToken = CancellationToken.None;

            _handler = new UpdatePermissionCommandHandler(_repository.Object, _unitOfWork.Object, _permissionTypeRepository.Object, _publisherService.Object, _elasticProducer.Object);
        }

        [Fact]
        public async Task Handler_UpdatePermission_Success()
        {
            //Arrage
            var request = new UpdatePermissionCommand(1, "Update", "Prueba", DateTime.Now, 1);

            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.IsValid.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Handler_UpdatePermission_PermissionNotExists_NotFound()
        {
            //Arrange
            var request = new UpdatePermissionCommand(2, "Update", "Prueba", DateTime.Now, 1);

            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Handler_UpdatePermission_PermissionTypeNotExists_NotFound()
        {
            //Arrange
            var request = new UpdatePermissionCommand(1, "Update", "Prueba", DateTime.Now, 2);

            //Act
            var result = await _handler.Handle(request, _cancellationToken);

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.IsValid.Should().BeFalse();
        }
    }
}