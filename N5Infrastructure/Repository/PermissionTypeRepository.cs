using N5Domain.Entities;
using N5Domain.Repositories;
using N5Infrastructure.Data;
using N5Infrastructure.Repository;

namespace Infrastructure.Persistance.Repositories;

public class PermissionTypeRepository : Repository<PermissionType>, IPermissionTypeRepository
{
    private readonly ApplicationDbContext _context;
    public PermissionTypeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
