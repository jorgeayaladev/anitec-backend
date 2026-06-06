using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Subscription.Application.CommandServices;
using Titan.AniTec.Platform.Subscription.Domain.Model;
using Titan.AniTec.Platform.Subscription.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Subscription.Domain.Repositories;

namespace Titan.AniTec.Platform.Subscription.Application.Internal.QueryServices;

public class SubscriptionQueryService(
    ISubscriptionPlanRepository planRepository,
    IUserSubscriptionRepository subscriptionRepository,
    IInvoiceRepository invoiceRepository,
    IPaymentMethodRepository paymentMethodRepository,
    IPaymentRepository paymentRepository) : ISubscriptionQueryService
{
    public async Task<Result<IReadOnlyCollection<SubscriptionPlan>>> GetAllPlansAsync(GetAllPlansQuery query)
    {
        try
        {
            var plans = await planRepository.FindAllActiveAsync();
            return Result<IReadOnlyCollection<SubscriptionPlan>>.Success(plans);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<SubscriptionPlan>>.Failure(SubscriptionError.PlanNotFound);
        }
    }

    public async Task<Result<SubscriptionPlan>> GetPlanByIdAsync(GetPlanByIdQuery query)
    {
        try
        {
            var plan = await planRepository.FindByIdAsync(query.PlanId);
            if (plan == null)
                return Result<SubscriptionPlan>.Failure(SubscriptionError.PlanNotFound);
            return Result<SubscriptionPlan>.Success(plan);
        }
        catch (Exception)
        {
            return Result<SubscriptionPlan>.Failure(SubscriptionError.PlanNotFound);
        }
    }

    public async Task<Result<UserSubscription>> GetMySubscriptionAsync(GetMySubscriptionQuery query)
    {
        try
        {
            var subscription = await subscriptionRepository.FindByFarmIdAsync(query.UserId);
            if (subscription == null)
                return Result<UserSubscription>.Failure(SubscriptionError.SubscriptionNotFound);
            return Result<UserSubscription>.Success(subscription);
        }
        catch (Exception)
        {
            return Result<UserSubscription>.Failure(SubscriptionError.SubscriptionNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Invoice>>> GetMyInvoicesAsync(GetMyInvoicesQuery query)
    {
        try
        {
            var invoices = await invoiceRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Invoice>>.Success(invoices);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Invoice>>.Failure(SubscriptionError.InvoiceNotFound);
        }
    }

    public async Task<Result<Invoice>> GetMyInvoiceByIdAsync(GetMyInvoiceByIdQuery query)
    {
        try
        {
            var invoice = await invoiceRepository.FindByIdAsync(query.InvoiceId);
            if (invoice == null || invoice.FarmId != query.UserId)
                return Result<Invoice>.Failure(SubscriptionError.InvoiceNotFound);
            return Result<Invoice>.Success(invoice);
        }
        catch (Exception)
        {
            return Result<Invoice>.Failure(SubscriptionError.InvoiceNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<PaymentMethod>>> GetMyPaymentMethodsAsync(GetMyPaymentMethodsQuery query)
    {
        try
        {
            var methods = await paymentMethodRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<PaymentMethod>>.Success(methods);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<PaymentMethod>>.Failure(SubscriptionError.PaymentMethodNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Payment>>> GetMyPaymentHistoryAsync(GetMyPaymentHistoryQuery query)
    {
        try
        {
            var payments = await paymentRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Payment>>.Success(payments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Payment>>.Failure(SubscriptionError.PaymentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<UserSubscription>>> GetAllSubscriptionsAsync(GetAllSubscriptionsQuery query)
    {
        try
        {
            var subscriptions = await subscriptionRepository.FindAllAsync();
            return Result<IReadOnlyCollection<UserSubscription>>.Success(subscriptions);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<UserSubscription>>.Failure(SubscriptionError.SubscriptionNotFound);
        }
    }

    public async Task<Result<UserSubscription>> GetSubscriptionByIdAsync(GetSubscriptionByIdQuery query)
    {
        try
        {
            var subscription = await subscriptionRepository.FindByIdAsync(query.SubscriptionId);
            if (subscription == null)
                return Result<UserSubscription>.Failure(SubscriptionError.SubscriptionNotFound);
            return Result<UserSubscription>.Success(subscription);
        }
        catch (Exception)
        {
            return Result<UserSubscription>.Failure(SubscriptionError.SubscriptionNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<UserSubscription>>> GetSubscriptionsByStatusAsync(GetSubscriptionsByStatusQuery query)
    {
        try
        {
            var subscriptions = await subscriptionRepository.FindByStatusAsync(query.Status);
            return Result<IReadOnlyCollection<UserSubscription>>.Success(subscriptions);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<UserSubscription>>.Failure(SubscriptionError.SubscriptionNotFound);
        }
    }

    public async Task<Result<object>> GetRevenueReportAsync(GetRevenueReportQuery query)
    {
        try
        {
            var payments = await paymentRepository.ListAsync();
            var totalRevenue = payments.Where(p => p.Status == "succeeded").Sum(p => p.Amount);
            return Result<object>.Success(new { TotalRevenue = totalRevenue, Currency = "USD" });
        }
        catch (Exception)
        {
            return Result<object>.Failure(SubscriptionError.PaymentNotFound);
        }
    }

    public async Task<Result<object>> GetMonthlyRevenueAsync(GetMonthlyRevenueQuery query)
    {
        try
        {
            var payments = await paymentRepository.ListAsync();
            var monthly = payments
                .Where(p => p.Status == "succeeded")
                .GroupBy(p => new { p.CreatedAt!.Value.Year, p.CreatedAt!.Value.Month })
                .Select(g => new { Year = g.Key.Year, Month = g.Key.Month, Revenue = g.Sum(p => p.Amount) })
                .OrderByDescending(r => r.Year).ThenByDescending(r => r.Month)
                .ToList();
            return Result<object>.Success(monthly);
        }
        catch (Exception)
        {
            return Result<object>.Failure(SubscriptionError.PaymentNotFound);
        }
    }

    public async Task<Result<object>> GetRevenueByPlanAsync(GetRevenueByPlanQuery query)
    {
        try
        {
            var payments = await paymentRepository.ListAsync();
            var invoices = await invoiceRepository.ListAsync();

            var revenueByPlan = payments
                .Where(p => p.Status == "succeeded")
                .Join(invoices, p => p.InvoiceId, i => i.Id, (p, i) => new { p.Amount, i.UserSubscriptionId })
                .GroupBy(x => x.UserSubscriptionId)
                .Select(g => new { UserSubscriptionId = g.Key, Revenue = g.Sum(x => x.Amount) })
                .ToList();
            return Result<object>.Success(revenueByPlan);
        }
        catch (Exception)
        {
            return Result<object>.Failure(SubscriptionError.PaymentNotFound);
        }
    }
}
