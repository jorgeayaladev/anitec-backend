namespace Titan.AniTec.Platform.Subscription.Domain.Repositories;

public record GetAllPlansQuery;

public record GetPlanByIdQuery(int PlanId);

public record GetMySubscriptionQuery(int UserId);

public record GetMyInvoicesQuery(int UserId);

public record GetMyInvoiceByIdQuery(int UserId, int InvoiceId);

public record GetMyPaymentMethodsQuery(int UserId);

public record GetMyPaymentHistoryQuery(int UserId);

public record GetAllSubscriptionsQuery;

public record GetSubscriptionByIdQuery(int SubscriptionId);

public record GetSubscriptionsByStatusQuery(string Status);

public record GetRevenueReportQuery;

public record GetMonthlyRevenueQuery;

public record GetRevenueByPlanQuery;
