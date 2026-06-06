using Titan.AniTec.Platform.Inventory.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Inventory.Domain.Repositories;
using Titan.AniTec.Platform.Inventory.Interfaces.Resources;
using Titan.AniTec.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Inventory.Interfaces.Assemblers;

public static class InventoryAssembler
{
    public static RegisterProductCommand ToCommand(int userId, CreateProductResource resource)
        => new(userId, resource.Name, resource.Category, resource.Unit, resource.Sku, resource.Description);

    public static UpdateProductCommand ToCommand(int userId, int productId, UpdateProductResource resource)
        => new(userId, productId, resource.Name, resource.Category, resource.Unit, resource.Sku, resource.Description);

    public static ProductResource ToResource(Product entity)
        => new(entity.Id, entity.FarmId, entity.Name, entity.Category, entity.Unit, entity.Sku, entity.Description);

    public static RegisterInventoryItemCommand ToCommand(int userId, CreateInventoryItemResource resource)
        => new(userId, resource.ProductId, resource.CurrentStock, resource.MinimumStock, resource.Location);

    public static UpdateInventoryItemCommand ToCommand(int userId, int itemId, UpdateInventoryItemResource resource)
        => new(userId, itemId, resource.CurrentStock, resource.MinimumStock, resource.Location);

    public static InventoryItemResource ToResource(InventoryItem entity)
        => new(entity.Id, entity.FarmId, entity.ProductId, entity.CurrentStock, entity.MinimumStock, entity.Location, entity.IsLowStock);

    public static RegisterStockMovementCommand ToCommand(int userId, CreateStockMovementResource resource)
        => new(userId, resource.InventoryItemId, resource.MovementType, resource.Quantity, resource.UnitPrice, resource.Reference, resource.Notes);

    public static StockMovementResource ToResource(StockMovement entity)
        => new(entity.Id, entity.FarmId, entity.InventoryItemId, entity.MovementType, entity.Quantity, entity.UnitPrice, entity.Reference, entity.Notes);

    public static RegisterSupplierCommand ToCommand(int userId, CreateSupplierResource resource)
        => new(userId, resource.Name, resource.ContactPerson, resource.Phone, resource.Email, resource.Address, resource.Notes);

    public static UpdateSupplierCommand ToCommand(int userId, int supplierId, UpdateSupplierResource resource)
        => new(userId, supplierId, resource.Name, resource.ContactPerson, resource.Phone, resource.Email, resource.Address, resource.Notes);

    public static SupplierResource ToResource(Supplier entity)
        => new(entity.Id, entity.FarmId, entity.Name, entity.ContactPerson, entity.Phone, entity.Email, entity.Address, entity.Notes);
}

public static class InventoryActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Data);

        return result.Error switch
        {
            Domain.Model.InventoryError.ProductNotFound => new NotFoundResult(),
            Domain.Model.InventoryError.InventoryItemNotFound => new NotFoundResult(),
            Domain.Model.InventoryError.StockMovementNotFound => new NotFoundResult(),
            Domain.Model.InventoryError.SupplierNotFound => new NotFoundResult(),
            _ => new BadRequestObjectResult(new ProblemDetails
            {
                Title = "Bad Request",
                Detail = result.Error?.ToString() ?? "Unknown error",
                Status = 400
            })
        };
    }

    public static IActionResult ToActionResult(Result result)
    {
        if (result.IsSuccess)
            return new OkResult();

        return result.Error switch
        {
            Domain.Model.InventoryError.ProductNotFound => new NotFoundResult(),
            Domain.Model.InventoryError.InventoryItemNotFound => new NotFoundResult(),
            Domain.Model.InventoryError.StockMovementNotFound => new NotFoundResult(),
            Domain.Model.InventoryError.SupplierNotFound => new NotFoundResult(),
            _ => new BadRequestObjectResult(new ProblemDetails
            {
                Title = "Bad Request",
                Detail = result.Error?.ToString() ?? "Unknown error",
                Status = 400
            })
        };
    }

    public static IActionResult ToCreatedActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return new CreatedResult(string.Empty, result.Value);

        return ToActionResult(result);
    }
}
