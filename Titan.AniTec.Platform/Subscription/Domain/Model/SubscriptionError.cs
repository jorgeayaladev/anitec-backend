namespace Titan.AniTec.Platform.Subscription.Domain.Model;

public enum SubscriptionError
{
    PlanNotFound,
    PlanAlreadyExists,
    InvalidPlanData,
    SubscriptionNotFound,
    SubscriptionAlreadyActive,
    InvalidSubscriptionData,
    InvoiceNotFound,
    InvalidInvoiceData,
    PaymentMethodNotFound,
    InvalidPaymentMethodData,
    PaymentNotFound,
    InvalidPaymentData,
    WebhookProcessingFailed
}
