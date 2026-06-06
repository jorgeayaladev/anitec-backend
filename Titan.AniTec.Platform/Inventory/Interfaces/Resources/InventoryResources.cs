namespace Titan.AniTec.Platform.Inventory.Interfaces.Resources;

public record ProductResource(int Id, int FarmId, string Name, string Category, string Unit, string? Sku, string? Description);
public record CreateProductResource(string Name, string Category, string Unit, string? Sku, string? Description);
public record UpdateProductResource(string Name, string Category, string Unit, string? Sku, string? Description);

public record InventoryItemResource(int Id, int FarmId, int ProductId, double CurrentStock, double MinimumStock, string? Location, bool IsLowStock);
public record CreateInventoryItemResource(int ProductId, double CurrentStock, double MinimumStock, string? Location);
public record UpdateInventoryItemResource(double CurrentStock, double MinimumStock, string? Location);

public record StockMovementResource(int Id, int FarmId, int InventoryItemId, string MovementType,
    double Quantity, decimal? UnitPrice, string? Reference, string? Notes);

public record CreateStockMovementResource(int InventoryItemId, string MovementType,
    double Quantity, decimal? UnitPrice, string? Reference, string? Notes);

public record SupplierResource(int Id, int FarmId, string Name, string? ContactPerson, string? Phone, string? Email, string? Address, string? Notes);
public record CreateSupplierResource(string Name, string? ContactPerson, string? Phone, string? Email, string? Address, string? Notes);
public record UpdateSupplierResource(string Name, string? ContactPerson, string? Phone, string? Email, string? Address, string? Notes);
