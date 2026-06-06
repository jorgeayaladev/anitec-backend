using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Subscription.Domain.Model.Aggregates;

public class SubscriptionPlan : IAuditableEntity
{
    public SubscriptionPlan(string name, string? description, decimal price, string currency,
        string billingCycle, int maxAnimals, int maxFarms, int maxUsers, string? features)
    {
        Name = name;
        Description = description;
        Price = price;
        Currency = currency;
        BillingCycle = billingCycle;
        MaxAnimals = maxAnimals;
        MaxFarms = maxFarms;
        MaxUsers = maxUsers;
        Features = features;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public string Currency { get; private set; }
    public string BillingCycle { get; private set; }
    public int MaxAnimals { get; private set; }
    public int MaxFarms { get; private set; }
    public int MaxUsers { get; private set; }
    public string? Features { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public SubscriptionPlan UpdateDetails(string name, string? description, decimal price, string currency,
        string billingCycle, int maxAnimals, int maxFarms, int maxUsers, string? features, bool isActive)
    {
        Name = name;
        Description = description;
        Price = price;
        Currency = currency;
        BillingCycle = billingCycle;
        MaxAnimals = maxAnimals;
        MaxFarms = maxFarms;
        MaxUsers = maxUsers;
        Features = features;
        IsActive = isActive;
        return this;
    }

    public SubscriptionPlan Deactivate()
    {
        IsActive = false;
        return this;
    }
}
