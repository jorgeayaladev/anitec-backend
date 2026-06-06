namespace Titan.AniTec.Platform.Financial.Domain.Repositories;

public record RegisterTransactionCommand(int UserId, DateTime TransactionDate, string Type, string Category,
    string Description, decimal Amount, int? AnimalId, string? PaymentMethod, string? Reference, string? Notes);

public record UpdateTransactionCommand(int UserId, int TransactionId, DateTime TransactionDate, string Type,
    string Category, string Description, decimal Amount, int? AnimalId,
    string? PaymentMethod, string? Reference, string? Notes);

public record DeleteTransactionCommand(int UserId, int TransactionId);

public record RegisterBudgetCommand(int UserId, int Year, int Month, string Category, string BudgetType,
    decimal PlannedAmount, string? Notes);

public record UpdateBudgetCommand(int UserId, int BudgetId, int Year, int Month, string Category, string BudgetType,
    decimal PlannedAmount, string? Notes);

public record DeleteBudgetCommand(int UserId, int BudgetId);
public record BatchCreateTransactionsCommand(int UserId, IReadOnlyCollection<RegisterTransactionCommand> Transactions);

public record CreateCategoryCommand(int UserId, string Name, string Type, string? Description);
public record UpdateCategoryCommand(int UserId, int CategoryId, string Name, string Type, string? Description);
public record DeleteCategoryCommand(int UserId, int CategoryId);
