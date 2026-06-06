using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Subscription.Domain.Model.Aggregates;

public class PaymentMethod : IAuditableEntity
{
    public PaymentMethod(int farmId, string stripePaymentMethodId, string cardBrand,
        string last4, int expMonth, int expYear)
    {
        FarmId = farmId;
        StripePaymentMethodId = stripePaymentMethodId;
        CardBrand = cardBrand;
        Last4 = last4;
        ExpMonth = expMonth;
        ExpYear = expYear;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public string StripePaymentMethodId { get; private set; }
    public string CardBrand { get; private set; }
    public string Last4 { get; private set; }
    public int ExpMonth { get; private set; }
    public int ExpYear { get; private set; }
    public bool IsDefault { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public PaymentMethod SetAsDefault()
    {
        IsDefault = true;
        return this;
    }

    public PaymentMethod UnsetDefault()
    {
        IsDefault = false;
        return this;
    }
}
