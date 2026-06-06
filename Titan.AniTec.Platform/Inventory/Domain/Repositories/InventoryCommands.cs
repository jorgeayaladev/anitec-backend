namespace Titan.AniTec.Platform.Inventory.Domain.Repositories;

public record RegisterProductCommand(int UserId, string Name, string Category, string Unit, string? Sku, string? Description);
public record UpdateProductCommand(int UserId, int ProductId, string Name, string Category, string Unit, string? Sku, string? Description);
public record DeleteProductCommand(int UserId, int ProductId);

public record RegisterInventoryItemCommand(int UserId, int ProductId, double CurrentStock, double MinimumStock, string? Location);
public record UpdateInventoryItemCommand(int UserId, int ItemId, double CurrentStock, double MinimumStock, string? Location);
public record DeleteInventoryItemCommand(int UserId, int ItemId);

public record RegisterStockMovementCommand(int UserId, int InventoryItemId, string MovementType,
    double Quantity, decimal? UnitPrice, string? Reference, string? Notes);

public record RegisterSupplierCommand(int UserId, string Name, string? ContactPerson, string? Phone, string? Email, string? Address, string? Notes);
public record UpdateSupplierCommand(int UserId, int SupplierId, string Name, string? ContactPerson, string? Phone, string? Email, string? Address, string? Notes);
public record DeleteSupplierCommand(int UserId, int SupplierId);
