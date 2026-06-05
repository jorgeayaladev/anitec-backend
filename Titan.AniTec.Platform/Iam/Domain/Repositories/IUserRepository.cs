using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Iam.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
    Task<IEnumerable<User>> FindByRoleAsync(string role, CancellationToken cancellationToken);
    Task<IEnumerable<User>> FindByStatusAsync(bool isActive, CancellationToken cancellationToken);
    Task<IEnumerable<User>> SearchAsync(string term, CancellationToken cancellationToken);
}
