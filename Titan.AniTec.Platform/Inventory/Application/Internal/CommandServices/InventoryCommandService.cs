using Titan.AniTec.Platform.Inventory.Domain.Model;
using Titan.AniTec.Platform.Inventory.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Inventory.Domain.Repositories;
using Titan.AniTec.Platform.Inventory.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Inventory.Application.Internal.CommandServices;

public class InventoryCommandService(
    IProductRepository productRepository,
    IInventoryItemRepository inventoryItemRepository,
    IStockMovementRepository stockMovementRepository,
    ISupplierRepository supplierRepository,
    IUnitOfWork unitOfWork) : IInventoryCommandService
{
    public async Task<Result<Product>> RegisterProductAsync(RegisterProductCommand command)
    {
        try
        {
            var existing = await productRepository.FindBySkuAsync(command.UserId, command.Sku ?? string.Empty);
            if (existing != null && command.Sku != null)
                return Result<Product>.Failure(InventoryError.ProductAlreadyExists);

            var product = new Product(command.UserId, command.Name, command.Category, command.Unit, command.Sku, command.Description);
            await productRepository.AddAsync(product);
            await unitOfWork.CompleteAsync();
            return Result<Product>.Success(product);
        }
        catch (Exception)
        {
            return Result<Product>.Failure(InventoryError.InvalidProductData);
        }
    }

    public async Task<Result<Product>> UpdateProductAsync(UpdateProductCommand command)
    {
        try
        {
            var product = await productRepository.FindByIdAsync(command.ProductId);
            if (product == null)
                return Result<Product>.Failure(InventoryError.ProductNotFound);

            product.UpdateDetails(command.Name, command.Category, command.Unit, command.Sku, command.Description);
            await unitOfWork.CompleteAsync();
            return Result<Product>.Success(product);
        }
        catch (Exception)
        {
            return Result<Product>.Failure(InventoryError.InvalidProductData);
        }
    }

    public async Task<Result> DeleteProductAsync(DeleteProductCommand command)
    {
        try
        {
            var product = await productRepository.FindByIdAsync(command.ProductId);
            if (product == null)
                return Result.Failure(InventoryError.ProductNotFound);

            productRepository.Remove(product);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(InventoryError.ProductNotFound);
        }
    }

    public async Task<Result<InventoryItem>> RegisterInventoryItemAsync(RegisterInventoryItemCommand command)
    {
        try
        {
            var existing = await inventoryItemRepository.FindByProductIdAsync(command.ProductId);
            if (existing != null)
                return Result<InventoryItem>.Failure(InventoryError.InventoryItemNotFound);

            var item = new InventoryItem(command.UserId, command.ProductId, command.CurrentStock, command.MinimumStock, command.Location);
            await inventoryItemRepository.AddAsync(item);
            await unitOfWork.CompleteAsync();
            return Result<InventoryItem>.Success(item);
        }
        catch (Exception)
        {
            return Result<InventoryItem>.Failure(InventoryError.InvalidInventoryItemData);
        }
    }

    public async Task<Result<InventoryItem>> UpdateInventoryItemAsync(UpdateInventoryItemCommand command)
    {
        try
        {
            var item = await inventoryItemRepository.FindByIdAsync(command.ItemId);
            if (item == null)
                return Result<InventoryItem>.Failure(InventoryError.InventoryItemNotFound);

            item.UpdateStock(command.CurrentStock, command.MinimumStock, command.Location);
            await unitOfWork.CompleteAsync();
            return Result<InventoryItem>.Success(item);
        }
        catch (Exception)
        {
            return Result<InventoryItem>.Failure(InventoryError.InvalidInventoryItemData);
        }
    }

    public async Task<Result> DeleteInventoryItemAsync(DeleteInventoryItemCommand command)
    {
        try
        {
            var item = await inventoryItemRepository.FindByIdAsync(command.ItemId);
            if (item == null)
                return Result.Failure(InventoryError.InventoryItemNotFound);

            inventoryItemRepository.Remove(item);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(InventoryError.InventoryItemNotFound);
        }
    }

    public async Task<Result<StockMovement>> RegisterStockMovementAsync(RegisterStockMovementCommand command)
    {
        try
        {
            var item = await inventoryItemRepository.FindByIdAsync(command.InventoryItemId);
            if (item == null)
                return Result<StockMovement>.Failure(InventoryError.InventoryItemNotFound);

            var movement = new StockMovement(command.UserId, command.InventoryItemId, command.MovementType,
                command.Quantity, command.UnitPrice, command.Reference, command.Notes);

            item.AdjustStock(command.MovementType == "out" ? -command.Quantity : command.Quantity);

            await stockMovementRepository.AddAsync(movement);
            await unitOfWork.CompleteAsync();
            return Result<StockMovement>.Success(movement);
        }
        catch (Exception)
        {
            return Result<StockMovement>.Failure(InventoryError.InvalidStockMovementData);
        }
    }

    public async Task<Result<Supplier>> RegisterSupplierAsync(RegisterSupplierCommand command)
    {
        try
        {
            var supplier = new Supplier(command.UserId, command.Name, command.ContactPerson,
                command.Phone, command.Email, command.Address, command.Notes);
            await supplierRepository.AddAsync(supplier);
            await unitOfWork.CompleteAsync();
            return Result<Supplier>.Success(supplier);
        }
        catch (Exception)
        {
            return Result<Supplier>.Failure(InventoryError.InvalidSupplierData);
        }
    }

    public async Task<Result<Supplier>> UpdateSupplierAsync(UpdateSupplierCommand command)
    {
        try
        {
            var supplier = await supplierRepository.FindByIdAsync(command.SupplierId);
            if (supplier == null)
                return Result<Supplier>.Failure(InventoryError.SupplierNotFound);

            supplier.UpdateDetails(command.Name, command.ContactPerson, command.Phone,
                command.Email, command.Address, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<Supplier>.Success(supplier);
        }
        catch (Exception)
        {
            return Result<Supplier>.Failure(InventoryError.InvalidSupplierData);
        }
    }

    public async Task<Result> DeleteSupplierAsync(DeleteSupplierCommand command)
    {
        try
        {
            var supplier = await supplierRepository.FindByIdAsync(command.SupplierId);
            if (supplier == null)
                return Result.Failure(InventoryError.SupplierNotFound);

            supplierRepository.Remove(supplier);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(InventoryError.SupplierNotFound);
        }
    }
}
