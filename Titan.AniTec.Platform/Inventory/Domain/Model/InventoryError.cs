namespace Titan.AniTec.Platform.Inventory.Domain.Model;

public enum InventoryError
{
    ProductNotFound,
    ProductAlreadyExists,
    InventoryItemNotFound,
    StockMovementNotFound,
    SupplierNotFound,
    SupplierAlreadyExists,
    InvalidProductData,
    InvalidInventoryItemData,
    InvalidStockMovementData,
    InvalidSupplierData,
    InsufficientStock,
    UnauthorizedAccess
}
