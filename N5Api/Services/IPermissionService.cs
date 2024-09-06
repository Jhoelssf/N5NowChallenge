using Microsoft.AspNetCore.Mvc;
using N5Application.DTO;

namespace N5Api.Services
{
    public interface IPermissionService
    {
        Task<IActionResult> UpdatePermission(int id, UpdatePermissionDto dto);
        Task<IActionResult> CreatePermission(AddPermissionDto dto);
        Task<IActionResult> GetPermissions();
    }
}