using System.Net;
using Application.Common.Services;
using FluentAssertions;
using Moq;
using N5Application.Commands;
using N5Domain.Entities;
using N5Domain.Repositories;

namespace N5Test.CommandService;

public class CreatePermissionTest
{
    private readonly Mock<IPermissionRepository> _repository;
    private readonly Mock<IPermissionTypeRepository> _permissionTypeRepository;
    private readonly Mock<IPublisherService> _publisherService;
    private readonly Mock<IElasticProducer> _elasticClient;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CreatePermissionCommandHandler _handler;
    private CancellationToken _cancellationToken;

    public CreatePermissionTest()
    {
        //Arrange
        _repository = new Mock<IPermissionRepository>();
        _permissionTypeRepository = new Mock<IPermissionTypeRepository>();
        _permissionTypeRepository.Setup(x => x.GetById(1)).ReturnsAsync(new PermissionType(1, "User"));
        _publisherService = new Mock<IPublisherService>();
        _elasticClient = new Mock<IElasticProducer>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _cancellationToken = CancellationToken.None;

        _handler = new CreatePermissionCommandHandler(_unitOfWork.Object, _repository.Object, _permissionTypeRepository.Object, _publisherService.Object, _elasticClient.Object);
    }
    [Fact]
    public async Task Handle_CreatePermmission_Success()
    {
        //Arrange
        var request = new CreatePermissionCommand("Prueba", "Uno", 1, DateTime.Now);

        //Act
        var result = await _handler.Handle(request, _cancellationToken);

        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_CreatePermission_InvalidPermissionTypeId_BadRequest()
    {
        //Arrange
        var request = new CreatePermissionCommand("Prueba", "Dos", 5345, DateTime.Now);

        //Act
        var result = await _handler.Handle(request, _cancellationToken);

        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsValid.Should().BeFalse();
    }
}