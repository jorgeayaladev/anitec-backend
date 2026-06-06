using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Subscription.Domain.Model.Aggregates;

public class Invoice : IAuditableEntity
{
    public Invoice(int userSubscriptionId, int farmId, decimal amount, string currency, DateTime dueDate)
    {
        UserSubscriptionId = userSubscriptionId;
        FarmId = farmId;
        Amount = amount;
        Currency = currency;
        DueDate = dueDate;
    }

    public int Id { get; private set; }
    public int UserSubscriptionId { get; private set; }
    public int FarmId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public string Status { get; private set; } = "pending";
    public DateTime DueDate { get; private set; }
    public DateTime? PaidAt { get; private set; }
    public string? StripeInvoiceId { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Invoice MarkPaid(DateTime paidAt)
    {
        Status = "paid";
        PaidAt = paidAt;
        return this;
    }

    public Invoice MarkFailed()
    {
        Status = "failed";
        return this;
    }

    public Invoice MarkRefunded()
    {
        Status = "refunded";
        return this;
    }

    public Invoice SetStripeInvoiceId(string stripeInvoiceId)
    {
        StripeInvoiceId = stripeInvoiceId;
        return this;
    }
}
