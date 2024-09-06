using N5Domain.Entities;

namespace N5Domain.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<IEnumerable<Permission>> GetAllAsync();
    }
}