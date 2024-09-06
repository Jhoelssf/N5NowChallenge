using Application.Common.Services;
using FluentAssertions;
using Moq;
using N5Application.DTO;
using N5Application.Queries;
using N5Domain.Entities;
using N5Domain.Repositories;
using System.Net;

namespace N5Test.QueryService
{
    public class GetAllPermissionsTest
    {
        private readonly Mock<IPermissionRepository> _repository;
        private GetAllPermissionsHandler _handler;
        private CancellationToken _cancellationToken;
        private readonly Mock<IPublisherService> _publisherService;

        public GetAllPermissionsTest()
        {
            _repository = new Mock<IPermissionRepository>();
            _publisherService = new Mock<IPublisherService>();
            _cancellationToken = CancellationToken.None;
            _handler = new GetAllPermissionsHandler(_repository.Object, _publisherService.Object);
        }

        [Fact]
        public async Task Handle_GetAllPermissions_Success()
        {
            //Arrange
            var request = new GetAllPermissions();
            var response = new List<PermissionDto>()
            {
                new PermissionDto(){Id = 0, EmployeeForename = "EmployeeForename 1",EmployeeSurname = "EmployeeSurname 1", PermissionDate= new DateTime(2022, 2, 24),  PermissionTypeId = 1 },
                new PermissionDto(){Id = 0, EmployeeForename = "EmployeeForename 2",EmployeeSurname = "EmployeeSurname 2", PermissionDate= new DateTime(2022, 2, 24),  PermissionTypeId = 2 }

            };
            var returnsRepository = new List<N5Domain.DTOs.AddPermissionDto>()
            {
                new N5Domain.DTOs.AddPermissionDto(){EmployeeForename = "EmployeeForename 1",EmployeeSurname = "EmployeeSurname 1",  PermissionTypeId = 1, PermissionDate = new DateTime(2022, 2, 24) },
                new N5Domain.DTOs.AddPermissionDto(){EmployeeForename = "EmployeeForename 2",EmployeeSurname = "EmployeeSurname 2",  PermissionTypeId = 2, PermissionDate = new DateTime(2022, 2, 24) }
            };

            var permissionRequest = new List<Permission>()
            {
                Permission.Create(returnsRepository[0]),
                Permission.Create(returnsRepository[1])
            };
            _repository.Setup(f => f.GetAllAsync()).ReturnsAsync(permissionRequest);

            //Act
            var result = await _handler.Handle(request, _cancellationToken);
            //Assert
            result.Content.Should().AllBeOfType<PermissionDto>();
            result.Content.Should().HaveCount(response.Count);
            result.IsValid.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}