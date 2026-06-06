using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Subscription.Domain.Model.Aggregates;

public class Payment : IAuditableEntity
{
    public Payment(int invoiceId, int farmId, decimal amount, string currency)
    {
        InvoiceId = invoiceId;
        FarmId = farmId;
        Amount = amount;
        Currency = currency;
    }

    public int Id { get; private set; }
    public int InvoiceId { get; private set; }
    public int FarmId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public string Status { get; private set; } = "succeeded";
    public string? StripePaymentIntentId { get; private set; }
    public int? PaymentMethodId { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Payment MarkFailed()
    {
        Status = "failed";
        return this;
    }

    public Payment MarkRefunded()
    {
        Status = "refunded";
        return this;
    }

    public Payment SetStripePaymentIntentId(string stripePaymentIntentId)
    {
        StripePaymentIntentId = stripePaymentIntentId;
        return this;
    }

    public Payment SetPaymentMethodId(int paymentMethodId)
    {
        PaymentMethodId = paymentMethodId;
        return this;
    }
}
