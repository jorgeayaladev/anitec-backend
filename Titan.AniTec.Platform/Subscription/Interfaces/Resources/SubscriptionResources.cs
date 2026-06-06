namespace Titan.AniTec.Platform.Subscription.Interfaces.Resources;

public record SubscriptionPlanResource(
    int Id, string Name, string? Description, decimal Price, string Currency,
    string BillingCycle, int MaxAnimals, int MaxFarms, int MaxUsers,
    string? Features, bool IsActive);

public record CreateSubscriptionPlanResource(
    string Name, string? Description, decimal Price, string Currency,
    string BillingCycle, int MaxAnimals, int MaxFarms, int MaxUsers, string? Features);

public record UpdateSubscriptionPlanResource(
    string Name, string? Description, decimal Price, string Currency,
    string BillingCycle, int MaxAnimals, int MaxFarms, int MaxUsers,
    string? Features, bool IsActive);

public record UserSubscriptionResource(
    int Id, int FarmId, int PlanId, string Status, DateTime StartDate,
    DateTime? EndDate, DateTime? TrialEndDate, DateTime? CanceledAt, bool AutoRenew,
    string? StripeSubscriptionId);

public record InvoiceResource(
    int Id, int UserSubscriptionId, int FarmId, decimal Amount, string Currency,
    string Status, DateTime DueDate, DateTime? PaidAt, string? StripeInvoiceId);

public record PaymentMethodResource(
    int Id, int FarmId, string StripePaymentMethodId, string CardBrand,
    string Last4, int ExpMonth, int ExpYear, bool IsDefault);

public record CreatePaymentMethodResource(
    string StripePaymentMethodId, string CardBrand,
    string Last4, int ExpMonth, int ExpYear);

public record PaymentResource(
    int Id, int InvoiceId, int FarmId, decimal Amount, string Currency,
    string Status, string? StripePaymentIntentId, int? PaymentMethodId);

public record SubscribeResource(int PlanId, DateTime? TrialEndDate, bool AutoRenew);

public record ChangePlanResource(int NewPlanId);

public record SetDefaultPaymentMethodResource(int PaymentMethodId);

public record WebhookResource(string EventType, string StripeEventJson);
