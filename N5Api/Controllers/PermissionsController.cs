using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5Api.Services;
using N5Application.Commands;
using N5Application.DTO;
using N5Application.Queries;

namespace N5Api.Controllers
{
    public class PermissionsController : BaseController, IPermissionService
    {
        public PermissionsController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [ProducesResponseType(typeof(CreatePermissionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreatePermission([FromBody] AddPermissionDto dto)
        {
            var request = new CreatePermissionCommand(dto.EmployeeForename, dto.EmployeeSurname, dto.PermissionTypeId, dto.PermissionDate);
            return Result(await Mediator.Send(request));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdatePermissionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePermission([FromRoute(Name = "id")] int id, [FromBody] UpdatePermissionDto dto)
        {
            var request = new UpdatePermissionCommand(id, dto.EmployeeForename, dto.EmployeeSurname, dto.PermissionDate, dto.PermissionTypeId);
            return Result(await Mediator.Send(request));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PermissionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPermissions()
        {
            return Result(await Mediator.Send(new GetAllPermissions()));
        }
    }
}