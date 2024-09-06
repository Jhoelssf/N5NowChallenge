using N5Domain.Base;
using N5Domain.DTOs;

namespace N5Domain.Entities;
public class PermissionType : Entity
{
    public int Id { get; set; }
    public string Description { get; set; }

    public PermissionType(int id, string description) : base(id)
    {
        Description = description;
    }
    public IEnumerable<Permission> Permissions { get; private set; }
    public static PermissionType Create(AddPermissionTypeDto type)
    {
        var permissionType = new PermissionType(0, type.Description)
        {
            Description = type.Description
        };
        return permissionType;
    }

    public void Update(string description)
    {
        Description = description;
    }
}