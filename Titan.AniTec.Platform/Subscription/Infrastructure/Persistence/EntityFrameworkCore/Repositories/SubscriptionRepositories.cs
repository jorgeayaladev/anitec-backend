using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Subscription.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Subscription.Domain.Repositories;

namespace Titan.AniTec.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SubscriptionPlanRepository(AppDbContext context)
    : BaseRepository<SubscriptionPlan>(context), ISubscriptionPlanRepository
{
    public async Task<IReadOnlyCollection<SubscriptionPlan>> FindAllActiveAsync()
        => await Context.Set<SubscriptionPlan>()
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
            .ToListAsync();

    public async Task<SubscriptionPlan?> FindByNameAsync(string name)
        => await Context.Set<SubscriptionPlan>()
            .FirstOrDefaultAsync(p => p.Name == name);
}

public class UserSubscriptionRepository(AppDbContext context)
    : BaseRepository<UserSubscription>(context), IUserSubscriptionRepository
{
    public async Task<UserSubscription?> FindByFarmIdAsync(int farmId)
        => await Context.Set<UserSubscription>()
            .OrderByDescending(s => s.Id)
            .FirstOrDefaultAsync(s => s.FarmId == farmId);

    public async Task<IReadOnlyCollection<UserSubscription>> FindAllAsync()
        => await Context.Set<UserSubscription>()
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<UserSubscription>> FindByStatusAsync(string status)
        => await Context.Set<UserSubscription>()
            .Where(s => s.Status == status)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<UserSubscription>> FindActiveAsync()
        => await FindByStatusAsync("active");

    public async Task<IReadOnlyCollection<UserSubscription>> FindExpiredAsync()
        => await FindByStatusAsync("expired");

    public async Task<IReadOnlyCollection<UserSubscription>> FindCanceledAsync()
        => await FindByStatusAsync("canceled");
}

public class InvoiceRepository(AppDbContext context)
    : BaseRepository<Invoice>(context), IInvoiceRepository
{
    public async Task<IReadOnlyCollection<Invoice>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Invoice>()
            .Where(i => i.FarmId == farmId)
            .OrderByDescending(i => i.DueDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Invoice>> FindByUserSubscriptionIdAsync(int userSubscriptionId)
        => await Context.Set<Invoice>()
            .Where(i => i.UserSubscriptionId == userSubscriptionId)
            .OrderByDescending(i => i.DueDate)
            .ToListAsync();
}

public class PaymentMethodRepository(AppDbContext context)
    : BaseRepository<PaymentMethod>(context), IPaymentMethodRepository
{
    public async Task<IReadOnlyCollection<PaymentMethod>> FindByFarmIdAsync(int farmId)
        => await Context.Set<PaymentMethod>()
            .Where(m => m.FarmId == farmId)
            .OrderByDescending(m => m.IsDefault)
            .ThenByDescending(m => m.CreatedAt)
            .ToListAsync();

    public async Task<PaymentMethod?> FindDefaultByFarmIdAsync(int farmId)
        => await Context.Set<PaymentMethod>()
            .FirstOrDefaultAsync(m => m.FarmId == farmId && m.IsDefault);
}

public class PaymentRepository(AppDbContext context)
    : BaseRepository<Payment>(context), IPaymentRepository
{
    public async Task<IReadOnlyCollection<Payment>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Payment>()
            .Where(p => p.FarmId == farmId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Payment>> FindByInvoiceIdAsync(int invoiceId)
        => await Context.Set<Payment>()
            .Where(p => p.InvoiceId == invoiceId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
}
