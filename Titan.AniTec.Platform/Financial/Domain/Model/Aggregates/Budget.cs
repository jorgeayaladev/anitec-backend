using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Financial.Domain.Model.Aggregates;

public class Budget : IAuditableEntity
{
    public Budget(int farmId, int year, int month, string category, string budgetType,
        decimal plannedAmount, string? notes)
    {
        FarmId = farmId;
        Year = year;
        Month = month;
        Category = category;
        BudgetType = budgetType;
        PlannedAmount = plannedAmount;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int Year { get; private set; }
    public int Month { get; private set; }
    public string Category { get; private set; }
    public string BudgetType { get; private set; }
    public decimal PlannedAmount { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Budget UpdateDetails(int year, int month, string category, string budgetType,
        decimal plannedAmount, string? notes)
    {
        Year = year;
        Month = month;
        Category = category;
        BudgetType = budgetType;
        PlannedAmount = plannedAmount;
        Notes = notes;
        return this;
    }
}
