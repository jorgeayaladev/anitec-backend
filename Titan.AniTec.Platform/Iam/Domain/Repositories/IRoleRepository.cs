using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Iam.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<IReadOnlyCollection<Role>> ListAllAsync(CancellationToken cancellationToken);
    Task<Role?> FindByNameAsync(string name, CancellationToken cancellationToken);
}
