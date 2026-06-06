using Titan.AniTec.Platform.Inventory.Domain.Model;
using Titan.AniTec.Platform.Inventory.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Inventory.Domain.Repositories;
using Titan.AniTec.Platform.Inventory.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Inventory.Application.Internal.QueryServices;

public class InventoryQueryService(
    IProductRepository productRepository,
    IInventoryItemRepository inventoryItemRepository,
    IStockMovementRepository stockMovementRepository,
    ISupplierRepository supplierRepository) : IInventoryQueryService
{
    public async Task<Result<IReadOnlyCollection<Product>>> GetAllProductsAsync(GetAllProductsQuery query)
    {
        try
        {
            var items = await productRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Product>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Product>>.Failure(InventoryError.ProductNotFound);
        }
    }

    public async Task<Result<Product>> GetProductByIdAsync(GetProductByIdQuery query)
    {
        try
        {
            var item = await productRepository.FindByIdAsync(query.ProductId);
            return item != null
                ? Result<Product>.Success(item)
                : Result<Product>.Failure(InventoryError.ProductNotFound);
        }
        catch (Exception)
        {
            return Result<Product>.Failure(InventoryError.ProductNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Product>>> SearchProductsAsync(SearchProductsQuery query)
    {
        try
        {
            var items = await productRepository.SearchAsync(query.UserId, query.Term);
            return Result<IReadOnlyCollection<Product>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Product>>.Failure(InventoryError.ProductNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Product>>> GetProductsByCategoryAsync(GetProductsByCategoryQuery query)
    {
        try
        {
            var items = await productRepository.FindByCategoryAsync(query.UserId, query.Category);
            return Result<IReadOnlyCollection<Product>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Product>>.Failure(InventoryError.ProductNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<InventoryItem>>> GetAllInventoryItemsAsync(GetAllInventoryItemsQuery query)
    {
        try
        {
            var items = await inventoryItemRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<InventoryItem>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<InventoryItem>>.Failure(InventoryError.InventoryItemNotFound);
        }
    }

    public async Task<Result<InventoryItem>> GetInventoryItemByIdAsync(GetInventoryItemByIdQuery query)
    {
        try
        {
            var item = await inventoryItemRepository.FindByIdAsync(query.ItemId);
            return item != null
                ? Result<InventoryItem>.Success(item)
                : Result<InventoryItem>.Failure(InventoryError.InventoryItemNotFound);
        }
        catch (Exception)
        {
            return Result<InventoryItem>.Failure(InventoryError.InventoryItemNotFound);
        }
    }

    public async Task<Result<InventoryItem>> GetInventoryItemByProductAsync(GetInventoryItemByProductQuery query)
    {
        try
        {
            var item = await inventoryItemRepository.FindByProductIdAsync(query.ProductId);
            return item != null
                ? Result<InventoryItem>.Success(item)
                : Result<InventoryItem>.Failure(InventoryError.InventoryItemNotFound);
        }
        catch (Exception)
        {
            return Result<InventoryItem>.Failure(InventoryError.InventoryItemNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<InventoryItem>>> GetLowStockItemsAsync(GetLowStockItemsQuery query)
    {
        try
        {
            var items = await inventoryItemRepository.FindLowStockByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<InventoryItem>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<InventoryItem>>.Failure(InventoryError.InventoryItemNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<StockMovement>>> GetAllStockMovementsAsync(GetAllStockMovementsQuery query)
    {
        try
        {
            var items = await stockMovementRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<StockMovement>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<StockMovement>>.Failure(InventoryError.StockMovementNotFound);
        }
    }

    public async Task<Result<StockMovement>> GetStockMovementByIdAsync(GetStockMovementByIdQuery query)
    {
        try
        {
            var item = await stockMovementRepository.FindByIdAsync(query.MovementId);
            return item != null
                ? Result<StockMovement>.Success(item)
                : Result<StockMovement>.Failure(InventoryError.StockMovementNotFound);
        }
        catch (Exception)
        {
            return Result<StockMovement>.Failure(InventoryError.StockMovementNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<StockMovement>>> GetStockMovementsByItemAsync(GetStockMovementsByItemQuery query)
    {
        try
        {
            var items = await stockMovementRepository.FindByInventoryItemIdAsync(query.InventoryItemId);
            return Result<IReadOnlyCollection<StockMovement>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<StockMovement>>.Failure(InventoryError.StockMovementNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<StockMovement>>> GetStockMovementsByDateRangeAsync(GetStockMovementsByDateRangeQuery query)
    {
        try
        {
            var items = await stockMovementRepository.FindByDateRangeAsync(query.UserId, query.Start, query.End);
            return Result<IReadOnlyCollection<StockMovement>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<StockMovement>>.Failure(InventoryError.StockMovementNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<StockMovement>>> GetStockMovementsByTypeAsync(GetStockMovementsByTypeQuery query)
    {
        try
        {
            var items = await stockMovementRepository.FindByTypeAsync(query.UserId, query.MovementType);
            return Result<IReadOnlyCollection<StockMovement>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<StockMovement>>.Failure(InventoryError.StockMovementNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Supplier>>> GetAllSuppliersAsync(GetAllSuppliersQuery query)
    {
        try
        {
            var items = await supplierRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Supplier>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Supplier>>.Failure(InventoryError.SupplierNotFound);
        }
    }

    public async Task<Result<Supplier>> GetSupplierByIdAsync(GetSupplierByIdQuery query)
    {
        try
        {
            var item = await supplierRepository.FindByIdAsync(query.SupplierId);
            return item != null
                ? Result<Supplier>.Success(item)
                : Result<Supplier>.Failure(InventoryError.SupplierNotFound);
        }
        catch (Exception)
        {
            return Result<Supplier>.Failure(InventoryError.SupplierNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Supplier>>> SearchSuppliersAsync(SearchSuppliersQuery query)
    {
        try
        {
            var items = await supplierRepository.SearchAsync(query.UserId, query.Term);
            return Result<IReadOnlyCollection<Supplier>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Supplier>>.Failure(InventoryError.SupplierNotFound);
        }
    }
}
