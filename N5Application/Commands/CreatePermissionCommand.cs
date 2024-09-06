using Application.Common.Services;
using MediatR;
using N5Application.DTO;
using N5Application.Models;
using N5Domain.DomainEvents;
using N5Domain.Entities;
using N5Domain.Repositories;

namespace N5Application.Commands;

public record struct CreatePermissionCommand(string EmployeeForename, string EmployeeSurname, int PermissionTypeId, DateTime PermissionDate) : IRequest<Response<CreatePermissionResponse>>
{
}

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Response<CreatePermissionResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPermissionRepository _repository;
    private readonly IPermissionTypeRepository _permissionTypeRepository;
    private readonly IPublisherService _publisherService;
    private readonly IElasticProducer _elasticProducer;

    public CreatePermissionCommandHandler(IUnitOfWork unitOfWork, IPermissionRepository repository, IPermissionTypeRepository permissionTypeRepository, IPublisherService publisherService, IElasticProducer elasticProducer)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _permissionTypeRepository = permissionTypeRepository ?? throw new ArgumentNullException(nameof(permissionTypeRepository));
        _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
        _elasticProducer = elasticProducer ?? throw new ArgumentNullException(nameof(elasticProducer));
    }

    public async Task<Response<CreatePermissionResponse>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<CreatePermissionResponse>();

        var existPermissionType = await _permissionTypeRepository.GetById(request.PermissionTypeId);

        if (existPermissionType is null)
        {
            response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            response.AddNotification("Entity Not Found", $"{nameof(request.PermissionTypeId)}", "PermissionType not found, a valid PermissionTypeId is required.");

            return response;
        }

        var addPermissionDto = new N5Domain.DTOs.AddPermissionDto
        {
            PermissionDate = request.PermissionDate,
            EmployeeForename = request.EmployeeForename,
            EmployeeSurname = request.EmployeeSurname,
            PermissionTypeId = request.PermissionTypeId
        };

        var entity = Permission.Create(addPermissionDto);

        await _repository.Add(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        entity.AddEvent(new PermissionCreatedDomainEvent());

        await _elasticProducer.IndexPermissionDocumentAsync(entity, cancellationToken);

        var guid = Guid.NewGuid().ToString();

        await _publisherService.Publish(new PublishDto
        {
            Id = guid,
            OperationName = "Request"
        }, cancellationToken);

        response.Content = new CreatePermissionResponse(entity.Id);
        response.StatusCode = System.Net.HttpStatusCode.Created;
        return response;
    }

}
