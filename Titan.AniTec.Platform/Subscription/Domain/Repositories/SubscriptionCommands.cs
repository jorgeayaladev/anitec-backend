namespace Titan.AniTec.Platform.Subscription.Domain.Repositories;

public record RegisterPlanCommand(string Name, string? Description, decimal Price, string Currency,
    string BillingCycle, int MaxAnimals, int MaxFarms, int MaxUsers, string? Features);

public record UpdatePlanCommand(int PlanId, string Name, string? Description, decimal Price, string Currency,
    string BillingCycle, int MaxAnimals, int MaxFarms, int MaxUsers, string? Features, bool IsActive);

public record DeletePlanCommand(int PlanId);

public record SubscribeCommand(int UserId, int PlanId, DateTime? TrialEndDate, bool AutoRenew);

public record ChangePlanCommand(int UserId, int NewPlanId);

public record CancelSubscriptionCommand(int UserId);

public record AddPaymentMethodCommand(int UserId, string StripePaymentMethodId, string CardBrand,
    string Last4, int ExpMonth, int ExpYear);

public record DeletePaymentMethodCommand(int UserId, int PaymentMethodId);

public record SetDefaultPaymentMethodCommand(int UserId, int PaymentMethodId);

public record ProcessWebhookCommand(string EventType, string StripeEventJson);
