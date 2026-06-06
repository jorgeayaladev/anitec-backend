using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Titan.AniTec.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RoleRepository(AppDbContext context) : BaseRepository<Role>(context), IRoleRepository
{
    public async Task<IReadOnlyCollection<Role>> ListAllAsync(CancellationToken cancellationToken)
        => await Context.Set<Role>().OrderBy(r => r.Name).ToListAsync(cancellationToken);

    public async Task<Role?> FindByNameAsync(string name, CancellationToken cancellationToken)
        => await Context.Set<Role>().FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
}
