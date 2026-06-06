using Titan.AniTec.Platform.Financial.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Financial.Domain.Repositories;
using Titan.AniTec.Platform.Financial.Interfaces.Resources;
using Titan.AniTec.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Financial.Interfaces.Assemblers;

public static class FinancialAssembler
{
    // Transaction mappings
    public static RegisterTransactionCommand ToCommand(int userId, CreateTransactionResource resource)
        => new(userId, resource.TransactionDate, resource.Type, resource.Category,
            resource.Description, resource.Amount, resource.AnimalId,
            resource.PaymentMethod, resource.Reference, resource.Notes);

    public static UpdateTransactionCommand ToCommand(int userId, int transactionId, UpdateTransactionResource resource)
        => new(userId, transactionId, resource.TransactionDate, resource.Type, resource.Category,
            resource.Description, resource.Amount, resource.AnimalId,
            resource.PaymentMethod, resource.Reference, resource.Notes);

    public static TransactionResource ToResource(Transaction entity)
        => new(entity.Id, entity.FarmId, entity.TransactionDate, entity.Type, entity.Category,
            entity.Description, entity.Amount, entity.AnimalId, entity.PaymentMethod,
            entity.Reference, entity.Notes);

    // Income mappings (transaction with type="income")
    public static RegisterTransactionCommand ToIncomeCommand(int userId, CreateIncomeResource resource)
        => new(userId, resource.TransactionDate, "income", resource.Category,
            resource.Description, resource.Amount, resource.AnimalId,
            resource.PaymentMethod, resource.Reference, resource.Notes);

    public static IncomeResource ToIncomeResource(Transaction entity)
        => new(entity.Id, entity.FarmId, entity.TransactionDate, entity.Category,
            entity.Description, entity.Amount, entity.AnimalId, entity.PaymentMethod,
            entity.Reference, entity.Notes);

    // Expense mappings (transaction with type="expense")
    public static RegisterTransactionCommand ToExpenseCommand(int userId, CreateExpenseResource resource)
        => new(userId, resource.TransactionDate, "expense", resource.Category,
            resource.Description, resource.Amount, resource.AnimalId,
            resource.PaymentMethod, resource.Reference, resource.Notes);

    public static ExpenseResource ToExpenseResource(Transaction entity)
        => new(entity.Id, entity.FarmId, entity.TransactionDate, entity.Category,
            entity.Description, entity.Amount, entity.AnimalId, entity.PaymentMethod,
            entity.Reference, entity.Notes);

    // Budget mappings
    public static RegisterBudgetCommand ToCommand(int userId, CreateBudgetResource resource)
        => new(userId, resource.Year, resource.Month, resource.Category, resource.BudgetType,
            resource.PlannedAmount, resource.Notes);

    public static UpdateBudgetCommand ToCommand(int userId, int budgetId, UpdateBudgetResource resource)
        => new(userId, budgetId, resource.Year, resource.Month, resource.Category, resource.BudgetType,
            resource.PlannedAmount, resource.Notes);

    public static BudgetResource ToResource(Budget entity)
        => new(entity.Id, entity.FarmId, entity.Year, entity.Month, entity.Category,
            entity.BudgetType, entity.PlannedAmount, entity.Notes);

    // Category mappings
    public static CreateCategoryCommand ToCommand(int userId, CreateFinancialCategoryResource resource)
        => new(userId, resource.Name, resource.Type, resource.Description);

    public static UpdateCategoryCommand ToCommand(int userId, int categoryId, UpdateFinancialCategoryResource resource)
        => new(userId, categoryId, resource.Name, resource.Type, resource.Description);

    public static FinancialCategoryResource ToResource(FinancialCategory entity)
        => new(entity.Id, entity.FarmId, entity.Name, entity.Type, entity.Description);

    // Report mappings
    public static FinancialReportSummaryResource ToResource(FinancialReportSummary entity)
        => new(entity.TotalIncome, entity.TotalExpense, entity.NetBalance,
            entity.TransactionCount, entity.IncomeCount, entity.ExpenseCount);

    public static IncomeStatementResource ToResource(IncomeStatement entity)
        => new(entity.TotalIncome, entity.TotalExpense, entity.NetIncome,
            entity.Lines.Select(l => new IncomeStatementLineResource(l.Category, l.Amount, l.Percentage)).ToList());

    public static CashFlowReportResource ToResource(CashFlowReport entity)
        => new(entity.OpeningBalance, entity.TotalInflows, entity.TotalOutflows, entity.ClosingBalance,
            entity.Lines.Select(l => new CashFlowLineResource(l.Date, l.Description, l.Type, l.Amount)).ToList());

    public static MonthlyComparisonReportResource ToResource(MonthlyComparisonReport entity)
        => new(entity.Lines.Select(l => new MonthlyComparisonLineResource(l.Month, l.Income, l.Expense, l.Net)).ToList());

    public static YearlyComparisonReportResource ToResource(YearlyComparisonReport entity)
        => new(entity.Lines.Select(l => new YearlyComparisonLineResource(l.Year, l.Income, l.Expense, l.Net)).ToList());

    public static ReportByAnimalResource ToResource(ReportByAnimal entity)
        => new(entity.TotalIncome, entity.TotalExpense, entity.Net, entity.AnimalId);

    public static ReportByCategoryLineResource ToResource(ReportByCategoryLine entity)
        => new(entity.Category, entity.Income, entity.Expense, entity.Net);

    public static ProfitabilityReportResource ToResource(ProfitabilityReport entity)
        => new(entity.TotalIncome, entity.TotalExpense, entity.NetProfit, entity.ProfitMargin, entity.CostPerAnimal);

    public static ProfitabilityByAnimalLineResource ToResource(ProfitabilityByAnimalLine entity)
        => new(entity.AnimalId, entity.Income, entity.Expense, entity.Profit);

    public static ProfitabilityByRaceLineResource ToResource(ProfitabilityByRaceLine entity)
        => new(entity.Race, entity.Income, entity.Expense, entity.Profit);

    public static TaxSummaryResource ToResource(TaxSummary entity)
        => new(entity.TotalIncome, entity.TotalExpense, entity.TaxableIncome, entity.EstimatedTax, entity.Year);

    public static DashboardIndicatorsResource ToResource(DashboardIndicators entity)
        => new(entity.CurrentMonthIncome, entity.CurrentMonthExpense, entity.PendingInvoices,
            entity.ActiveBudgets, entity.BudgetUtilization, entity.MonthlyGrowth);

    public static FinancialTrendLineResource ToResource(FinancialTrendLine entity)
        => new(entity.Date, entity.Income, entity.Expense, entity.Net);
}

public static class FinancialActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Data);

        return result.Error switch
        {
            Domain.Model.FinancialError.TransactionNotFound => new NotFoundResult(),
            Domain.Model.FinancialError.BudgetNotFound => new NotFoundResult(),
            Domain.Model.FinancialError.CategoryNotFound => new NotFoundResult(),
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
            Domain.Model.FinancialError.TransactionNotFound => new NotFoundResult(),
            Domain.Model.FinancialError.BudgetNotFound => new NotFoundResult(),
            Domain.Model.FinancialError.CategoryNotFound => new NotFoundResult(),
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

    public static IActionResult ToFileResult(Result<byte[]> result, string contentType, string fileName)
    {
        if (result.IsSuccess && result.Value is not null)
            return new FileContentResult(result.Value, contentType) { FileDownloadName = fileName };

        return ToActionResult(result);
    }
}
