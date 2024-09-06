using N5Domain.Base;
using N5Domain.Entities;

namespace N5Domain.Repositories;

public interface IPermissionTypeRepository : IRepository<PermissionType>, IPermissionTypeRepositoryReadOnly<PermissionType>
{
}

public interface IPermissionTypeRepositoryReadOnly<T> where T : Entity { }