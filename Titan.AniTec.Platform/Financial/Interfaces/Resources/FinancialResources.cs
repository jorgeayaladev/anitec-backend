namespace Titan.AniTec.Platform.Financial.Interfaces.Resources;

public record TransactionResource(int Id, int FarmId, DateTime TransactionDate, string Type, string Category,
    string Description, decimal Amount, int? AnimalId, string? PaymentMethod, string? Reference, string? Notes);

public record CreateTransactionResource(DateTime TransactionDate, string Type, string Category,
    string Description, decimal Amount, int? AnimalId, string? PaymentMethod, string? Reference, string? Notes);

public record UpdateTransactionResource(DateTime TransactionDate, string Type, string Category,
    string Description, decimal Amount, int? AnimalId, string? PaymentMethod, string? Reference, string? Notes);

// Income resources (aliases for transaction resources with type="income")
public record IncomeResource(int Id, int FarmId, DateTime TransactionDate, string Category,
    string Description, decimal Amount, int? AnimalId, string? PaymentMethod, string? Reference, string? Notes);

public record CreateIncomeResource(DateTime TransactionDate, string Category,
    string Description, decimal Amount, int? AnimalId, string? PaymentMethod, string? Reference, string? Notes);

// Expense resources (aliases for transaction resources with type="expense")
public record ExpenseResource(int Id, int FarmId, DateTime TransactionDate, string Category,
    string Description, decimal Amount, int? AnimalId, string? PaymentMethod, string? Reference, string? Notes);

public record CreateExpenseResource(DateTime TransactionDate, string Category,
    string Description, decimal Amount, int? AnimalId, string? PaymentMethod, string? Reference, string? Notes);

// Budget resources
public record BudgetResource(int Id, int FarmId, int Year, int Month, string Category,
    string BudgetType, decimal PlannedAmount, string? Notes);

public record CreateBudgetResource(int Year, int Month, string Category, string BudgetType,
    decimal PlannedAmount, string? Notes);

public record UpdateBudgetResource(int Year, int Month, string Category, string BudgetType,
    decimal PlannedAmount, string? Notes);

public record BudgetVsActualResource(int BudgetId, string Category, string BudgetType,
    decimal PlannedAmount, decimal ActualAmount, decimal Difference, int Year, int Month);

// Category resources
public record FinancialCategoryResource(int Id, int FarmId, string Name, string Type, string? Description);

public record CreateFinancialCategoryResource(string Name, string Type, string? Description);

public record UpdateFinancialCategoryResource(string Name, string Type, string? Description);

// Report resources
public record FinancialReportSummaryResource(decimal TotalIncome, decimal TotalExpense, decimal NetBalance,
    int TransactionCount, int IncomeCount, int ExpenseCount);

public record IncomeStatementResource(decimal TotalIncome, decimal TotalExpense, decimal NetIncome,
    IReadOnlyCollection<IncomeStatementLineResource> Lines);

public record IncomeStatementLineResource(string Category, decimal Amount, decimal Percentage);

public record CashFlowReportResource(decimal OpeningBalance, decimal TotalInflows, decimal TotalOutflows,
    decimal ClosingBalance, IReadOnlyCollection<CashFlowLineResource> Lines);

public record CashFlowLineResource(DateTime Date, string Description, string Type, decimal Amount);

public record MonthlyComparisonReportResource(IReadOnlyCollection<MonthlyComparisonLineResource> Lines);

public record MonthlyComparisonLineResource(int Month, decimal Income, decimal Expense, decimal Net);

public record YearlyComparisonReportResource(IReadOnlyCollection<YearlyComparisonLineResource> Lines);

public record YearlyComparisonLineResource(int Year, decimal Income, decimal Expense, decimal Net);

public record ReportByAnimalResource(decimal TotalIncome, decimal TotalExpense, decimal Net, int AnimalId);

public record ReportByCategoryLineResource(string Category, decimal Income, decimal Expense, decimal Net);

public record ProfitabilityReportResource(decimal TotalIncome, decimal TotalExpense, decimal NetProfit,
    decimal ProfitMargin, decimal CostPerAnimal);

public record ProfitabilityByAnimalLineResource(int AnimalId, decimal Income, decimal Expense, decimal Profit);

public record ProfitabilityByRaceLineResource(string Race, decimal Income, decimal Expense, decimal Profit);

public record TaxSummaryResource(decimal TotalIncome, decimal TotalExpense, decimal TaxableIncome,
    decimal EstimatedTax, int Year);

public record DashboardIndicatorsResource(decimal CurrentMonthIncome, decimal CurrentMonthExpense,
    decimal PendingInvoices, int ActiveBudgets, decimal BudgetUtilization,
    decimal MonthlyGrowth);

public record FinancialTrendLineResource(DateTime Date, decimal Income, decimal Expense, decimal Net);
