namespace Titan.AniTec.Platform.Inventory.Domain.Model.Aggregates;

public class StockMovement
{
    public StockMovement(int farmId, int inventoryItemId, string movementType,
        double quantity, decimal? unitPrice, string? reference, string? notes)
    {
        FarmId = farmId;
        InventoryItemId = inventoryItemId;
        MovementType = movementType;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Reference = reference;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int InventoryItemId { get; private set; }
    public string MovementType { get; private set; }
    public double Quantity { get; private set; }
    public decimal? UnitPrice { get; private set; }
    public string? Reference { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
}
