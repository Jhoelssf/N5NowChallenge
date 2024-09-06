using Microsoft.EntityFrameworkCore;
using N5Domain.Entities;
using N5Domain.Repositories;
using N5Infrastructure.Data;

namespace N5Infrastructure.Repository;
public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    public PermissionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        var results = await _dbSet.OrderByDescending(f => f.PermissionDate)
            .ToListAsync();

        return results;
    }
}
