using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Inventory.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Inventory.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Titan.AniTec.Platform.Inventory.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    public async Task<IReadOnlyCollection<Product>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Product>()
            .Where(p => p.FarmId == farmId)
            .OrderBy(p => p.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Product>> SearchAsync(int farmId, string term)
        => await Context.Set<Product>()
            .Where(p => p.FarmId == farmId && (p.Name.Contains(term) || p.Sku!.Contains(term)))
            .OrderBy(p => p.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Product>> FindByCategoryAsync(int farmId, string category)
        => await Context.Set<Product>()
            .Where(p => p.FarmId == farmId && p.Category == category)
            .OrderBy(p => p.Name)
            .ToListAsync();

    public async Task<Product?> FindBySkuAsync(int farmId, string sku)
        => await Context.Set<Product>()
            .FirstOrDefaultAsync(p => p.FarmId == farmId && p.Sku == sku);
}

public class InventoryItemRepository(AppDbContext context) : BaseRepository<InventoryItem>(context), IInventoryItemRepository
{
    public async Task<IReadOnlyCollection<InventoryItem>> FindByFarmIdAsync(int farmId)
        => await Context.Set<InventoryItem>()
            .Where(i => i.FarmId == farmId)
            .OrderBy(i => i.ProductId)
            .ToListAsync();

    public async Task<InventoryItem?> FindByProductIdAsync(int productId)
        => await Context.Set<InventoryItem>()
            .FirstOrDefaultAsync(i => i.ProductId == productId);

    public async Task<IReadOnlyCollection<InventoryItem>> FindLowStockByFarmIdAsync(int farmId)
    {
        var items = await Context.Set<InventoryItem>()
            .Where(i => i.FarmId == farmId)
            .ToListAsync();

        return items.Where(i => i.IsLowStock).OrderBy(i => i.ProductId).ToList();
    }
}

public class StockMovementRepository(AppDbContext context) : BaseRepository<StockMovement>(context), IStockMovementRepository
{
    public async Task<IReadOnlyCollection<StockMovement>> FindByFarmIdAsync(int farmId)
        => await Context.Set<StockMovement>()
            .Where(m => m.FarmId == farmId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<StockMovement>> FindByInventoryItemIdAsync(int inventoryItemId)
        => await Context.Set<StockMovement>()
            .Where(m => m.InventoryItemId == inventoryItemId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<StockMovement>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end)
        => await Context.Set<StockMovement>()
            .Where(m => m.FarmId == farmId && m.CreatedAt >= start && m.CreatedAt <= end)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<StockMovement>> FindByTypeAsync(int farmId, string movementType)
        => await Context.Set<StockMovement>()
            .Where(m => m.FarmId == farmId && m.MovementType == movementType)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
}

public class SupplierRepository(AppDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
{
    public async Task<IReadOnlyCollection<Supplier>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Supplier>()
            .Where(s => s.FarmId == farmId)
            .OrderBy(s => s.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Supplier>> SearchAsync(int farmId, string term)
        => await Context.Set<Supplier>()
            .Where(s => s.FarmId == farmId && (s.Name.Contains(term) || s.ContactPerson!.Contains(term)))
            .OrderBy(s => s.Name)
            .ToListAsync();
}
