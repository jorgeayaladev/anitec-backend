namespace Titan.AniTec.Platform.Financial.Domain.Model;

public enum FinancialError
{
    TransactionNotFound,
    BudgetNotFound,
    BudgetAlreadyExists,
    InvalidTransactionData,
    InvalidBudgetData,
    UnauthorizedAccess,
    CategoryNotFound,
    InvalidCategoryData,
    ReportDataNotAvailable
}
