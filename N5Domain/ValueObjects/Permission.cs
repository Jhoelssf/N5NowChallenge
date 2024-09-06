using N5Domain.Base;
using N5Domain.DTOs;

namespace N5Domain.Entities;

public class Permission : AggregateRoot
{
    public Permission(int id, string employeeForename, string employeeSurname, DateTime permissionDate, int permissionTypeId) : base(id)
    {
        EmployeeForename = employeeForename;
        EmployeeSurname = employeeSurname;
        PermissionDate = permissionDate;
        PermissionTypeId = permissionTypeId;
    }

    public string EmployeeForename { get; private set; }
    public string EmployeeSurname { get; private set; }
    public int PermissionTypeId { get; private set; }
    public DateTime PermissionDate { get; private set; }

    private Permission(int id) : base(id)
    {
        this.Id = id;
    }

    public static Permission Create(AddPermissionDto addDto)
    {
        var permission = new Permission(0)
        {
            PermissionTypeId = addDto.PermissionTypeId,
            EmployeeForename = addDto.EmployeeForename,
            EmployeeSurname = addDto.EmployeeSurname,
            PermissionDate = addDto.PermissionDate,
        };

        return permission;
    }

    public static Permission UpdateProperties(Permission entity, UpdatePermissionDto updatePermissionDto)
    {
        if (entity.EmployeeForename != updatePermissionDto.EmployeeForename)
        {
            entity.EmployeeForename = updatePermissionDto.EmployeeForename;
        }

        if (entity.EmployeeSurname != updatePermissionDto.EmployeeSurname)
        {
            entity.EmployeeSurname = updatePermissionDto.EmployeeSurname;
        }

        if (entity.PermissionTypeId != updatePermissionDto.PermissionTypeId)
        {
            entity.PermissionTypeId = updatePermissionDto.PermissionTypeId;
        }

        return entity;
    }
}
