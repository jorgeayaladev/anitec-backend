using Titan.AniTec.Platform.Inventory.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Inventory.Domain.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IReadOnlyCollection<Product>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Product>> SearchAsync(int farmId, string term);
    Task<IReadOnlyCollection<Product>> FindByCategoryAsync(int farmId, string category);
    Task<Product?> FindBySkuAsync(int farmId, string sku);
}

public interface IInventoryItemRepository : IBaseRepository<InventoryItem>
{
    Task<IReadOnlyCollection<InventoryItem>> FindByFarmIdAsync(int farmId);
    Task<InventoryItem?> FindByProductIdAsync(int productId);
    Task<IReadOnlyCollection<InventoryItem>> FindLowStockByFarmIdAsync(int farmId);
}

public interface IStockMovementRepository : IBaseRepository<StockMovement>
{
    Task<IReadOnlyCollection<StockMovement>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<StockMovement>> FindByInventoryItemIdAsync(int inventoryItemId);
    Task<IReadOnlyCollection<StockMovement>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end);
    Task<IReadOnlyCollection<StockMovement>> FindByTypeAsync(int farmId, string movementType);
}

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task<IReadOnlyCollection<Supplier>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Supplier>> SearchAsync(int farmId, string term);
}
