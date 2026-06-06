using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Inventory.Domain.Model.Aggregates;

public class InventoryItem : IAuditableEntity
{
    public InventoryItem(int farmId, int productId, double currentStock, double minimumStock, string? location)
    {
        FarmId = farmId;
        ProductId = productId;
        CurrentStock = currentStock;
        MinimumStock = minimumStock;
        Location = location;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int ProductId { get; private set; }
    public double CurrentStock { get; private set; }
    public double MinimumStock { get; private set; }
    public string? Location { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public bool IsLowStock => CurrentStock <= MinimumStock;

    public InventoryItem UpdateStock(double currentStock, double minimumStock, string? location)
    {
        CurrentStock = currentStock;
        MinimumStock = minimumStock;
        Location = location;
        return this;
    }

    public InventoryItem AdjustStock(double quantity)
    {
        CurrentStock += quantity;
        return this;
    }
}
