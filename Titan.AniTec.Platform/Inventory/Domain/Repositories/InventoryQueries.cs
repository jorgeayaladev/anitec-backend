namespace Titan.AniTec.Platform.Inventory.Domain.Repositories;

public record GetAllProductsQuery(int UserId);
public record GetProductByIdQuery(int UserId, int ProductId);
public record SearchProductsQuery(int UserId, string Term);
public record GetProductsByCategoryQuery(int UserId, string Category);

public record GetAllInventoryItemsQuery(int UserId);
public record GetInventoryItemByIdQuery(int UserId, int ItemId);
public record GetInventoryItemByProductQuery(int UserId, int ProductId);
public record GetLowStockItemsQuery(int UserId);

public record GetAllStockMovementsQuery(int UserId);
public record GetStockMovementByIdQuery(int UserId, int MovementId);
public record GetStockMovementsByItemQuery(int UserId, int InventoryItemId);
public record GetStockMovementsByDateRangeQuery(int UserId, DateTime Start, DateTime End);
public record GetStockMovementsByTypeQuery(int UserId, string MovementType);

public record GetAllSuppliersQuery(int UserId);
public record GetSupplierByIdQuery(int UserId, int SupplierId);
public record SearchSuppliersQuery(int UserId, string Term);
