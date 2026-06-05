using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Titan.AniTec.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UserRepository(AppDbContext context)
    : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }

    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .AnyAsync(u => u.Username == username, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<IEnumerable<User>> FindByRoleAsync(string role, CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .Where(u => u.Role == role)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> FindByStatusAsync(bool isActive, CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .Where(u => u.IsActive == isActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> SearchAsync(string term, CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .Where(u => u.Username.Contains(term) || u.Email.Contains(term))
            .ToListAsync(cancellationToken);
    }
}
