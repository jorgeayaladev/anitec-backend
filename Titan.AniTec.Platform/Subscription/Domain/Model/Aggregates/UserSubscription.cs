using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Subscription.Domain.Model.Aggregates;

public class UserSubscription : IAuditableEntity
{
    public UserSubscription(int farmId, int planId, DateTime startDate, DateTime? endDate,
        DateTime? trialEndDate, bool autoRenew)
    {
        FarmId = farmId;
        PlanId = planId;
        StartDate = startDate;
        EndDate = endDate;
        TrialEndDate = trialEndDate;
        AutoRenew = autoRenew;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int PlanId { get; private set; }
    public string Status { get; private set; } = "active";
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public DateTime? TrialEndDate { get; private set; }
    public DateTime? CanceledAt { get; private set; }
    public bool AutoRenew { get; private set; }
    public string? StripeSubscriptionId { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public UserSubscription ChangePlan(int newPlanId)
    {
        PlanId = newPlanId;
        return this;
    }

    public UserSubscription Cancel()
    {
        Status = "canceled";
        CanceledAt = DateTime.UtcNow;
        AutoRenew = false;
        return this;
    }

    public UserSubscription SetStripeSubscriptionId(string stripeSubscriptionId)
    {
        StripeSubscriptionId = stripeSubscriptionId;
        return this;
    }
}
