using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Titan.AniTec.Platform.Inventory.Application.CommandServices;
using Titan.AniTec.Platform.Inventory.Domain.Repositories;
using Titan.AniTec.Platform.Inventory.Interfaces.Assemblers;
using Titan.AniTec.Platform.Inventory.Interfaces.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Inventory.Interfaces.Controllers;

[ApiController]
[Route("api/inventory")]
[Authorize]
public class InventoryController(
    IInventoryQueryService queryService,
    IInventoryCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    // Products
    [HttpGet("products")]
    public async Task<IActionResult> GetAllProducts()
    {
        var query = new GetAllProductsQuery(CurrentUserId);
        var result = await queryService.GetAllProductsAsync(query);
        return InventoryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(InventoryAssembler.ToResource).ToList()));
    }

    [HttpGet("products/{productId:int}")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        var query = new GetProductByIdQuery(CurrentUserId, productId);
        var result = await queryService.GetProductByIdAsync(query);
        return InventoryActionResultAssembler.ToActionResult(result.Map(InventoryAssembler.ToResource));
    }

    [HttpGet("products/search")]
    public async Task<IActionResult> SearchProducts([FromQuery] string q)
    {
        var query = new SearchProductsQuery(CurrentUserId, q);
        var result = await queryService.SearchProductsAsync(query);
        return InventoryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(InventoryAssembler.ToResource).ToList()));
    }

    [HttpGet("products/by-category/{category}")]
    public async Task<IActionResult> GetProductsByCategory(string category)
    {
        var query = new GetProductsByCategoryQuery(CurrentUserId, category);
        var result = await queryService.GetProductsByCategoryAsync(query);
        return InventoryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(InventoryAssembler.ToResource).ToList()));
    }

    [HttpPost("products")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductResource resource)
    {
        var command = InventoryAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterProductAsync(command);
        return InventoryActionResultAssembler.ToCreatedActionResult(result.Map(InventoryAssembler.ToResource));
    }

    [HttpPut("products/{productId:int}")]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductResource resource)
    {
        var command = InventoryAssembler.ToCommand(CurrentUserId, productId, resource);
        var result = await commandService.UpdateProductAsync(command);
        return InventoryActionResultAssembler.ToActionResult(result.Map(InventoryAssembler.ToResource));
    }

    [HttpDelete("products/{productId:int}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var command = new DeleteProductCommand(CurrentUserId, productId);
        var result = await commandService.DeleteProductAsync(command);
        return InventoryActionResultAssembler.ToActionResult(result);
    }

    // Inventory Items
    [HttpGet("items")]
    public async Task<IActionResult> GetAllItems()
    {
        var query = new GetAllInventoryItemsQuery(CurrentUserId);
        var result = await queryService.GetAllInventoryItemsAsync(query);
        return InventoryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(InventoryAssembler.ToResource).ToList()));
    }

    [HttpGet("items/{itemId:int}")]
    public async Task<IActionResult> GetItemById(int itemId)
    {
        var query = new GetInventoryItemByIdQuery(CurrentUserId, itemId);
        var result = await queryService.GetInventoryItemByIdAsync(query);
        return InventoryActionResultAssembler.ToActionResult(result.Map(InventoryAssembler.ToResource));
    }

    [HttpGet("items/by-product/{productId:int}")]
    public async Task<IActionResult> GetItemByProduct(int productId)
    {
        var query = new GetInventoryItemByProductQuery(CurrentUserId, productId);
        var result = await queryService.GetInventoryItemByProductAsync(query);
        return InventoryActionResultAssembler.ToActionResult(result.Map(InventoryAssembler.ToResource));
    }

    [HttpGet("items/low-stock")]
    public async Task<IActionResult> GetLowStockItems()
    {
        var query = new GetLowStockItemsQuery(CurrentUserId);
        var result = await queryService.GetLowStockItemsAsync(query);
        return InventoryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(InventoryAssembler.ToResource).ToList()));
    }

    [HttpPost("items")]
    public async Task<IActionResult> CreateItem([FromBody] CreateInventoryItemResource resource)
    {
        var command = InventoryAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterInventoryItemAsync(command);
        return InventoryActionResultAssembler.ToCreatedActionResult(result.Map(InventoryAssembler.ToResource));
    }

    [HttpPut("items/{itemId:int}")]
    public async Task<IActionResult> UpdateItem(int itemId, [FromBody] UpdateInventoryItemResource resource)
    {
        var command = InventoryAssembler.ToCommand(CurrentUserId, itemId, resource);
        var result = await commandService.UpdateInventoryItemAsync(command);
        return InventoryActionResultAssembler.ToActionResult(result.Map(InventoryAssembler.ToResource));
    }

    [HttpDelete("items/{itemId:int}")]
    public async Task<IActionResult> DeleteItem(int itemId)
    {
        var command = new DeleteInventoryItemCommand(CurrentUserId, itemId);
        var result = await commandService.DeleteInventoryItemAsync(command);
        return InventoryActionResultAssembler.ToActionResult(result);
    }

    // Stock Movements
    [HttpGet("movements")]
    public async Task<IActionResult> GetAllMovements()
    {
        var query = new GetAllStockMovementsQuery(CurrentUserId);
        var result = await queryService.GetAllStockMovementsAsync(query);
        return InventoryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(InventoryAssembler.ToResource).ToList()));
    }

    [HttpGet("movements/{movementId:int}")]
    public async Task<IActionResult> GetMovementById(int movementId)
    {
        var query = new GetStockMovementByIdQuery(CurrentUserId, movementId);
        var result = await queryService.GetStockMovementByIdAsync(query);
        return InventoryActionResultAssembler.ToActionResult(result.Map(InventoryAssembler.ToResource));
    }

    [HttpGet("movements/by-item/{itemId:int}")]
    public async Task<IActionResult> GetMovementsByItem(int itemId)
    {
        var query = new GetStockMovementsByItemQuery(CurrentUserId, itemId);
        var result = await queryService.GetStockMovementsByItemAsync(query);
        return InventoryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(InventoryAssembler.ToResource).ToList()));
    }

    [HttpGet("movements/by-date-range")]
    public async Task<IActionResult> GetMovementsByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var query = new GetStockMovementsByDateRangeQuery(CurrentUserId, start, end);
        var result = await queryService.GetStockMovementsByDateRangeAsync(query);
        return InventoryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(InventoryAssembler.ToResource).ToList()));
    }

    [HttpGet("movements/by-type/{movementType}")]
    public async Task<IActionResult> GetMovementsByType(string movementType)
    {
        var query = new GetStockMovementsByTypeQuery(CurrentUserId, movementType);
        var result = await queryService.GetStockMovementsByTypeAsync(query);
        return InventoryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(InventoryAssembler.ToResource).ToList()));
    }

    [HttpPost("movements")]
    public async Task<IActionResult> CreateMovement([FromBody] CreateStockMovementResource resource)
    {
        var command = InventoryAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterStockMovementAsync(command);
        return InventoryActionResultAssembler.ToCreatedActionResult(result.Map(InventoryAssembler.ToResource));
    }

    // Suppliers
    [HttpGet("suppliers")]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var query = new GetAllSuppliersQuery(CurrentUserId);
        var result = await queryService.GetAllSuppliersAsync(query);
        return InventoryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(InventoryAssembler.ToResource).ToList()));
    }

    [HttpGet("suppliers/{supplierId:int}")]
    public async Task<IActionResult> GetSupplierById(int supplierId)
    {
        var query = new GetSupplierByIdQuery(CurrentUserId, supplierId);
        var result = await queryService.GetSupplierByIdAsync(query);
        return InventoryActionResultAssembler.ToActionResult(result.Map(InventoryAssembler.ToResource));
    }

    [HttpGet("suppliers/search")]
    public async Task<IActionResult> SearchSuppliers([FromQuery] string q)
    {
        var query = new SearchSuppliersQuery(CurrentUserId, q);
        var result = await queryService.SearchSuppliersAsync(query);
        return InventoryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(InventoryAssembler.ToResource).ToList()));
    }

    [HttpPost("suppliers")]
    public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierResource resource)
    {
        var command = InventoryAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterSupplierAsync(command);
        return InventoryActionResultAssembler.ToCreatedActionResult(result.Map(InventoryAssembler.ToResource));
    }

    [HttpPut("suppliers/{supplierId:int}")]
    public async Task<IActionResult> UpdateSupplier(int supplierId, [FromBody] UpdateSupplierResource resource)
    {
        var command = InventoryAssembler.ToCommand(CurrentUserId, supplierId, resource);
        var result = await commandService.UpdateSupplierAsync(command);
        return InventoryActionResultAssembler.ToActionResult(result.Map(InventoryAssembler.ToResource));
    }

    [HttpDelete("suppliers/{supplierId:int}")]
    public async Task<IActionResult> DeleteSupplier(int supplierId)
    {
        var command = new DeleteSupplierCommand(CurrentUserId, supplierId);
        var result = await commandService.DeleteSupplierAsync(command);
        return InventoryActionResultAssembler.ToActionResult(result);
    }
}
