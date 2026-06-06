using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Inventory.Domain.Model.Aggregates;

public class Product : IAuditableEntity
{
    public Product(int farmId, string name, string category, string unit, string? sku, string? description)
    {
        FarmId = farmId;
        Name = name;
        Category = category;
        Unit = unit;
        Sku = sku;
        Description = description;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public string Name { get; private set; }
    public string Category { get; private set; }
    public string Unit { get; private set; }
    public string? Sku { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Product UpdateDetails(string name, string category, string unit, string? sku, string? description)
    {
        Name = name;
        Category = category;
        Unit = unit;
        Sku = sku;
        Description = description;
        return this;
    }
}
