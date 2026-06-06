using Titan.AniTec.Platform.Shared.Domain.Repositories;
using Titan.AniTec.Platform.Subscription.Domain.Model.Aggregates;

namespace Titan.AniTec.Platform.Subscription.Domain.Repositories;

public interface ISubscriptionPlanRepository : IBaseRepository<SubscriptionPlan>
{
    Task<IReadOnlyCollection<SubscriptionPlan>> FindAllActiveAsync();
    Task<SubscriptionPlan?> FindByNameAsync(string name);
}

public interface IUserSubscriptionRepository : IBaseRepository<UserSubscription>
{
    Task<UserSubscription?> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<UserSubscription>> FindAllAsync();
    Task<IReadOnlyCollection<UserSubscription>> FindByStatusAsync(string status);
    Task<IReadOnlyCollection<UserSubscription>> FindActiveAsync();
    Task<IReadOnlyCollection<UserSubscription>> FindExpiredAsync();
    Task<IReadOnlyCollection<UserSubscription>> FindCanceledAsync();
}

public interface IInvoiceRepository : IBaseRepository<Invoice>
{
    Task<IReadOnlyCollection<Invoice>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Invoice>> FindByUserSubscriptionIdAsync(int userSubscriptionId);
}

public interface IPaymentMethodRepository : IBaseRepository<PaymentMethod>
{
    Task<IReadOnlyCollection<PaymentMethod>> FindByFarmIdAsync(int farmId);
    Task<PaymentMethod?> FindDefaultByFarmIdAsync(int farmId);
}

public interface IPaymentRepository : IBaseRepository<Payment>
{
    Task<IReadOnlyCollection<Payment>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Payment>> FindByInvoiceIdAsync(int invoiceId);
}
