using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Subscription.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Subscription.Domain.Repositories;

namespace Titan.AniTec.Platform.Subscription.Application.CommandServices;

public interface ISubscriptionCommandService
{
    Task<Result<SubscriptionPlan>> RegisterPlanAsync(RegisterPlanCommand command);
    Task<Result<SubscriptionPlan>> UpdatePlanAsync(UpdatePlanCommand command);
    Task<Result> DeletePlanAsync(DeletePlanCommand command);
    Task<Result<UserSubscription>> SubscribeAsync(SubscribeCommand command);
    Task<Result<UserSubscription>> ChangePlanAsync(ChangePlanCommand command);
    Task<Result> CancelSubscriptionAsync(CancelSubscriptionCommand command);
    Task<Result<PaymentMethod>> AddPaymentMethodAsync(AddPaymentMethodCommand command);
    Task<Result> DeletePaymentMethodAsync(DeletePaymentMethodCommand command);
    Task<Result> SetDefaultPaymentMethodAsync(SetDefaultPaymentMethodCommand command);
    Task<Result> ProcessWebhookAsync(ProcessWebhookCommand command);
}

public interface ISubscriptionQueryService
{
    Task<Result<IReadOnlyCollection<SubscriptionPlan>>> GetAllPlansAsync(GetAllPlansQuery query);
    Task<Result<SubscriptionPlan>> GetPlanByIdAsync(GetPlanByIdQuery query);
    Task<Result<UserSubscription>> GetMySubscriptionAsync(GetMySubscriptionQuery query);
    Task<Result<IReadOnlyCollection<Invoice>>> GetMyInvoicesAsync(GetMyInvoicesQuery query);
    Task<Result<Invoice>> GetMyInvoiceByIdAsync(GetMyInvoiceByIdQuery query);
    Task<Result<IReadOnlyCollection<PaymentMethod>>> GetMyPaymentMethodsAsync(GetMyPaymentMethodsQuery query);
    Task<Result<IReadOnlyCollection<Payment>>> GetMyPaymentHistoryAsync(GetMyPaymentHistoryQuery query);
    Task<Result<IReadOnlyCollection<UserSubscription>>> GetAllSubscriptionsAsync(GetAllSubscriptionsQuery query);
    Task<Result<UserSubscription>> GetSubscriptionByIdAsync(GetSubscriptionByIdQuery query);
    Task<Result<IReadOnlyCollection<UserSubscription>>> GetSubscriptionsByStatusAsync(GetSubscriptionsByStatusQuery query);
    Task<Result<object>> GetRevenueReportAsync(GetRevenueReportQuery query);
    Task<Result<object>> GetMonthlyRevenueAsync(GetMonthlyRevenueQuery query);
    Task<Result<object>> GetRevenueByPlanAsync(GetRevenueByPlanQuery query);
}
