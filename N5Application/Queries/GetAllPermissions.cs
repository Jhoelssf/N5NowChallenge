using Application.Common.Services;
using MediatR;
using N5Application.DTO;
using N5Application.Models;
using N5Domain.Repositories;

namespace N5Application.Queries
{
    public record struct GetAllPermissions : IRequest<Response<List<PermissionDto>>>
    {
    }

    public class GetAllPermissionsHandler : IRequestHandler<GetAllPermissions, Response<List<PermissionDto>>>
    {
        private readonly IPermissionRepository _repository;
        private readonly IPublisherService _publisherService;
        public GetAllPermissionsHandler(IPermissionRepository repository, IPublisherService publisherService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
        }

        public async Task<Response<List<PermissionDto>>> Handle(GetAllPermissions request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetAllAsync();

            var guid = Guid.NewGuid().ToString();

            await _publisherService.Publish(new PublishDto
            {
                Id = guid,
                OperationName = "Get"
            }, cancellationToken);
            var response = new Response<List<PermissionDto>>
            {
                Content = results.Select(x =>
                {
                    return new PermissionDto
                    {
                        Id = x.Id,
                        EmployeeForename = x.EmployeeForename ?? string.Empty,
                        EmployeeSurname = x.EmployeeSurname,
                        PermissionTypeId = x.PermissionTypeId,
                        PermissionDate = x.PermissionDate,
                    };
                }).ToList(),
                StatusCode = System.Net.HttpStatusCode.OK,
            };
            return response;
        }
    }
}
