using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Financial.Domain.Model.Aggregates;

public class FinancialCategory : IAuditableEntity
{
    public FinancialCategory(int farmId, string name, string type, string? description)
    {
        FarmId = farmId;
        Name = name;
        Type = type;
        Description = description;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public FinancialCategory UpdateDetails(string name, string type, string? description)
    {
        Name = name;
        Type = type;
        Description = description;
        return this;
    }
}
