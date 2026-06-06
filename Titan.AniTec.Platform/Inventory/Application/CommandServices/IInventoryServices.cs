using Titan.AniTec.Platform.Inventory.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Inventory.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Inventory.Application.CommandServices;

public interface IInventoryCommandService
{
    Task<Result<Product>> RegisterProductAsync(RegisterProductCommand command);
    Task<Result<Product>> UpdateProductAsync(UpdateProductCommand command);
    Task<Result> DeleteProductAsync(DeleteProductCommand command);
    Task<Result<InventoryItem>> RegisterInventoryItemAsync(RegisterInventoryItemCommand command);
    Task<Result<InventoryItem>> UpdateInventoryItemAsync(UpdateInventoryItemCommand command);
    Task<Result> DeleteInventoryItemAsync(DeleteInventoryItemCommand command);
    Task<Result<StockMovement>> RegisterStockMovementAsync(RegisterStockMovementCommand command);
    Task<Result<Supplier>> RegisterSupplierAsync(RegisterSupplierCommand command);
    Task<Result<Supplier>> UpdateSupplierAsync(UpdateSupplierCommand command);
    Task<Result> DeleteSupplierAsync(DeleteSupplierCommand command);
}

public interface IInventoryQueryService
{
    Task<Result<IReadOnlyCollection<Product>>> GetAllProductsAsync(GetAllProductsQuery query);
    Task<Result<Product>> GetProductByIdAsync(GetProductByIdQuery query);
    Task<Result<IReadOnlyCollection<Product>>> SearchProductsAsync(SearchProductsQuery query);
    Task<Result<IReadOnlyCollection<Product>>> GetProductsByCategoryAsync(GetProductsByCategoryQuery query);
    Task<Result<IReadOnlyCollection<InventoryItem>>> GetAllInventoryItemsAsync(GetAllInventoryItemsQuery query);
    Task<Result<InventoryItem>> GetInventoryItemByIdAsync(GetInventoryItemByIdQuery query);
    Task<Result<InventoryItem>> GetInventoryItemByProductAsync(GetInventoryItemByProductQuery query);
    Task<Result<IReadOnlyCollection<InventoryItem>>> GetLowStockItemsAsync(GetLowStockItemsQuery query);
    Task<Result<IReadOnlyCollection<StockMovement>>> GetAllStockMovementsAsync(GetAllStockMovementsQuery query);
    Task<Result<StockMovement>> GetStockMovementByIdAsync(GetStockMovementByIdQuery query);
    Task<Result<IReadOnlyCollection<StockMovement>>> GetStockMovementsByItemAsync(GetStockMovementsByItemQuery query);
    Task<Result<IReadOnlyCollection<StockMovement>>> GetStockMovementsByDateRangeAsync(GetStockMovementsByDateRangeQuery query);
    Task<Result<IReadOnlyCollection<StockMovement>>> GetStockMovementsByTypeAsync(GetStockMovementsByTypeQuery query);
    Task<Result<IReadOnlyCollection<Supplier>>> GetAllSuppliersAsync(GetAllSuppliersQuery query);
    Task<Result<Supplier>> GetSupplierByIdAsync(GetSupplierByIdQuery query);
    Task<Result<IReadOnlyCollection<Supplier>>> SearchSuppliersAsync(SearchSuppliersQuery query);
}
