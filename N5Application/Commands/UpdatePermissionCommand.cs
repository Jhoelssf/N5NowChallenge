using Application.Common.Services;
using MediatR;
using N5Application.DTO;
using N5Application.Models;
using N5Domain.DomainEvents;
using N5Domain.Entities;
using N5Domain.Repositories;

namespace N5Application.Commands;
public record struct UpdatePermissionCommand(
        int Id,
        string EmployeeForename,
        string EmployeeSurname,
        DateTime? PermissionDate,
        int PermissionTypeId) : IRequest<Response<UpdatePermissionResponse>>
{
}

public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, Response<UpdatePermissionResponse>>
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPermissionTypeRepository _permissionTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisherService _publisherService;
    private readonly IElasticProducer _elasticProducer;
    public UpdatePermissionCommandHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork, IPermissionTypeRepository permissionTypeRepository, IPublisherService publisherService, IElasticProducer elasticProducer)
    {
        _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _permissionTypeRepository = permissionTypeRepository ?? throw new ArgumentNullException(nameof(permissionTypeRepository));
        _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
        _elasticProducer = elasticProducer ?? throw new ArgumentNullException(nameof(elasticProducer));
    }

    public async Task<Response<UpdatePermissionResponse>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<UpdatePermissionResponse>();

        var existsPermission = await _permissionRepository.GetById(request.Id);
        var existsPermissionType = await _permissionTypeRepository.GetById(request.PermissionTypeId);

        if (existsPermission is null)
        {
            response.StatusCode = System.Net.HttpStatusCode.NotFound;
            response.AddNotification("Entity not Found", $"{nameof(request.Id)}", "Permission not found, a valid Id is required.");

            return response;
        }

        if (existsPermissionType is null)
        {
            response.StatusCode = System.Net.HttpStatusCode.NotFound;
            response.AddNotification("Entity not Found", $"{nameof(request.PermissionTypeId)}", "PermissionType not found, a valid PermissionTypeId is required.");

            return response;
        }
        var permissionUpdatedProperties = Permission.UpdateProperties(existsPermission, new N5Domain.DTOs.UpdatePermissionDto
        {
            EmployeeForename = request.EmployeeForename,
            EmployeeSurname = request.EmployeeSurname,
            PermissionTypeId = request.PermissionTypeId,
            PermissionDate = request.PermissionDate ?? DateTime.Now
        });

        permissionUpdatedProperties.AddEvent(new PermissionUpdatedDomainEvent());

        await _permissionRepository.Update(permissionUpdatedProperties);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var guid = Guid.NewGuid().ToString();

        await _publisherService.Publish(new PublishDto
        {
            Id = guid,
            OperationName = "Modify"
        }, cancellationToken);

        await _elasticProducer.IndexPermissionDocumentAsync(permissionUpdatedProperties, cancellationToken);

        response.Content = new UpdatePermissionResponse(request.Id);
        return response;
    }
}