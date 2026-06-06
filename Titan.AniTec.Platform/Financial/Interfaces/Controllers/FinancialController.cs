using Titan.AniTec.Platform.Financial.Application.CommandServices;
using Titan.AniTec.Platform.Financial.Domain.Repositories;
using Titan.AniTec.Platform.Financial.Interfaces.Assemblers;
using Titan.AniTec.Platform.Financial.Interfaces.Resources;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Financial.Interfaces.Controllers;

[ApiController]
[Route("api/finance")]
[Authorize]
public class FinancialController(
    IFinancialQueryService queryService,
    IFinancialCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    // ========================================================================
    // INCOMES
    // ========================================================================

    [HttpGet("incomes")]
    public async Task<IActionResult> GetAllIncomes()
    {
        var query = new GetTransactionsByTypeQuery(CurrentUserId, "income");
        var result = await queryService.GetTransactionsByTypeAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToIncomeResource).ToList()));
    }

    [HttpGet("incomes/{incomeId:int}")]
    public async Task<IActionResult> GetIncomeById(int incomeId)
    {
        var query = new GetTransactionByIdQuery(CurrentUserId, incomeId);
        var result = await queryService.GetTransactionByIdAsync(query);
        if (!result.IsSuccess)
            return FinancialActionResultAssembler.ToActionResult(result);
        if (result.Value!.Type != "income")
            return NotFound();
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToIncomeResource));
    }

    [HttpPost("incomes")]
    public async Task<IActionResult> CreateIncome([FromBody] CreateIncomeResource resource)
    {
        var command = FinancialAssembler.ToIncomeCommand(CurrentUserId, resource);
        var result = await commandService.RegisterTransactionAsync(command);
        return FinancialActionResultAssembler.ToCreatedActionResult(result.Map(FinancialAssembler.ToIncomeResource));
    }

    [HttpPut("incomes/{incomeId:int}")]
    public async Task<IActionResult> UpdateIncome(int incomeId, [FromBody] UpdateTransactionResource resource)
    {
        var query = new GetTransactionByIdQuery(CurrentUserId, incomeId);
        var check = await queryService.GetTransactionByIdAsync(query);
        if (!check.IsSuccess || check.Value!.Type != "income")
            return NotFound();
        var command = FinancialAssembler.ToCommand(CurrentUserId, incomeId, resource with { Type = "income" });
        var result = await commandService.UpdateTransactionAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToIncomeResource));
    }

    [HttpDelete("incomes/{incomeId:int}")]
    public async Task<IActionResult> DeleteIncome(int incomeId)
    {
        var command = new DeleteTransactionCommand(CurrentUserId, incomeId);
        var result = await commandService.DeleteTransactionAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("incomes/by-category/{category}")]
    public async Task<IActionResult> GetIncomesByCategory(string category)
    {
        var query = new GetTransactionsByCategoryQuery(CurrentUserId, category);
        var result = await queryService.GetTransactionsByCategoryAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Where(t => t.Type == "income")
                .Select(FinancialAssembler.ToIncomeResource).ToList()));
    }

    [HttpGet("incomes/by-date-range")]
    public async Task<IActionResult> GetIncomesByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var query = new GetTransactionsByDateRangeQuery(CurrentUserId, start, end);
        var result = await queryService.GetTransactionsByDateRangeAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Where(t => t.Type == "income")
                .Select(FinancialAssembler.ToIncomeResource).ToList()));
    }

    [HttpGet("incomes/by-animal/{animalId:int}")]
    public async Task<IActionResult> GetIncomesByAnimal(int animalId)
    {
        var query = new GetTransactionsByAnimalQuery(CurrentUserId, animalId);
        var result = await queryService.GetTransactionsByAnimalAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Where(t => t.Type == "income")
                .Select(FinancialAssembler.ToIncomeResource).ToList()));
    }

    [HttpGet("incomes/by-source/{source}")]
    public async Task<IActionResult> GetIncomesBySource(string source)
    {
        return Ok(new List<IncomeResource>());
    }

    [HttpGet("incomes/recurrent")]
    public async Task<IActionResult> GetRecurrentIncomes()
    {
        return Ok(new List<IncomeResource>());
    }

    [HttpPost("incomes/batch")]
    public async Task<IActionResult> BatchCreateIncomes([FromBody] IReadOnlyCollection<CreateIncomeResource> resources)
    {
        var commands = resources.Select(r => FinancialAssembler.ToIncomeCommand(CurrentUserId, r)).ToList();
        var command = new BatchCreateTransactionsCommand(CurrentUserId, commands);
        var result = await commandService.BatchCreateTransactionsAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result);
    }

    // ========================================================================
    // EXPENSES
    // ========================================================================

    [HttpGet("expenses")]
    public async Task<IActionResult> GetAllExpenses()
    {
        var query = new GetTransactionsByTypeQuery(CurrentUserId, "expense");
        var result = await queryService.GetTransactionsByTypeAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToExpenseResource).ToList()));
    }

    [HttpGet("expenses/{expenseId:int}")]
    public async Task<IActionResult> GetExpenseById(int expenseId)
    {
        var query = new GetTransactionByIdQuery(CurrentUserId, expenseId);
        var result = await queryService.GetTransactionByIdAsync(query);
        if (!result.IsSuccess)
            return FinancialActionResultAssembler.ToActionResult(result);
        if (result.Value!.Type != "expense")
            return NotFound();
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToExpenseResource));
    }

    [HttpPost("expenses")]
    public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseResource resource)
    {
        var command = FinancialAssembler.ToExpenseCommand(CurrentUserId, resource);
        var result = await commandService.RegisterTransactionAsync(command);
        return FinancialActionResultAssembler.ToCreatedActionResult(result.Map(FinancialAssembler.ToExpenseResource));
    }

    [HttpPut("expenses/{expenseId:int}")]
    public async Task<IActionResult> UpdateExpense(int expenseId, [FromBody] UpdateTransactionResource resource)
    {
        var query = new GetTransactionByIdQuery(CurrentUserId, expenseId);
        var check = await queryService.GetTransactionByIdAsync(query);
        if (!check.IsSuccess || check.Value!.Type != "expense")
            return NotFound();
        var command = FinancialAssembler.ToCommand(CurrentUserId, expenseId, resource with { Type = "expense" });
        var result = await commandService.UpdateTransactionAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToExpenseResource));
    }

    [HttpDelete("expenses/{expenseId:int}")]
    public async Task<IActionResult> DeleteExpense(int expenseId)
    {
        var command = new DeleteTransactionCommand(CurrentUserId, expenseId);
        var result = await commandService.DeleteTransactionAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("expenses/by-category/{category}")]
    public async Task<IActionResult> GetExpensesByCategory(string category)
    {
        var query = new GetTransactionsByCategoryQuery(CurrentUserId, category);
        var result = await queryService.GetTransactionsByCategoryAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Where(t => t.Type == "expense")
                .Select(FinancialAssembler.ToExpenseResource).ToList()));
    }

    [HttpGet("expenses/by-date-range")]
    public async Task<IActionResult> GetExpensesByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var query = new GetTransactionsByDateRangeQuery(CurrentUserId, start, end);
        var result = await queryService.GetTransactionsByDateRangeAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Where(t => t.Type == "expense")
                .Select(FinancialAssembler.ToExpenseResource).ToList()));
    }

    [HttpGet("expenses/by-animal/{animalId:int}")]
    public async Task<IActionResult> GetExpensesByAnimal(int animalId)
    {
        var query = new GetTransactionsByAnimalQuery(CurrentUserId, animalId);
        var result = await queryService.GetTransactionsByAnimalAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Where(t => t.Type == "expense")
                .Select(FinancialAssembler.ToExpenseResource).ToList()));
    }

    [HttpGet("expenses/by-type/{type}")]
    public async Task<IActionResult> GetExpensesByType(string type)
    {
        var query = new GetTransactionsByTypeQuery(CurrentUserId, "expense");
        var result = await queryService.GetTransactionsByTypeAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Where(t => t.Category == type)
                .Select(FinancialAssembler.ToExpenseResource).ToList()));
    }

    [HttpGet("expenses/recurrent")]
    public async Task<IActionResult> GetRecurrentExpenses()
    {
        return Ok(new List<ExpenseResource>());
    }

    [HttpPost("expenses/batch")]
    public async Task<IActionResult> BatchCreateExpenses([FromBody] IReadOnlyCollection<CreateExpenseResource> resources)
    {
        var commands = resources.Select(r => FinancialAssembler.ToExpenseCommand(CurrentUserId, r)).ToList();
        var command = new BatchCreateTransactionsCommand(CurrentUserId, commands);
        var result = await commandService.BatchCreateTransactionsAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result);
    }

    // ========================================================================
    // TRANSACTIONS (generic, for backward compatibility)
    // ========================================================================

    [HttpGet("transactions")]
    public async Task<IActionResult> GetAllTransactions()
    {
        var query = new GetAllTransactionsQuery(CurrentUserId);
        var result = await queryService.GetAllTransactionsAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToResource).ToList()));
    }

    [HttpGet("transactions/{transactionId:int}")]
    public async Task<IActionResult> GetTransactionById(int transactionId)
    {
        var query = new GetTransactionByIdQuery(CurrentUserId, transactionId);
        var result = await queryService.GetTransactionByIdAsync(query);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpPost("transactions")]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionResource resource)
    {
        var command = FinancialAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterTransactionAsync(command);
        return FinancialActionResultAssembler.ToCreatedActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpPut("transactions/{transactionId:int}")]
    public async Task<IActionResult> UpdateTransaction(int transactionId, [FromBody] UpdateTransactionResource resource)
    {
        var command = FinancialAssembler.ToCommand(CurrentUserId, transactionId, resource);
        var result = await commandService.UpdateTransactionAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpDelete("transactions/{transactionId:int}")]
    public async Task<IActionResult> DeleteTransaction(int transactionId)
    {
        var command = new DeleteTransactionCommand(CurrentUserId, transactionId);
        var result = await commandService.DeleteTransactionAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result);
    }

    [HttpPost("transactions/batch")]
    public async Task<IActionResult> BatchCreateTransactions([FromBody] IReadOnlyCollection<CreateTransactionResource> resources)
    {
        var commands = resources.Select(r => FinancialAssembler.ToCommand(CurrentUserId, r)).ToList();
        var command = new BatchCreateTransactionsCommand(CurrentUserId, commands);
        var result = await commandService.BatchCreateTransactionsAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result);
    }

    // ========================================================================
    // CATEGORIES
    // ========================================================================

    [HttpGet("categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var query = new GetAllCategoriesQuery(CurrentUserId);
        var result = await queryService.GetAllCategoriesAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToResource).ToList()));
    }

    [HttpGet("categories/{categoryId:int}")]
    public async Task<IActionResult> GetCategoryById(int categoryId)
    {
        var query = new GetCategoryByIdQuery(CurrentUserId, categoryId);
        var result = await queryService.GetCategoryByIdAsync(query);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateFinancialCategoryResource resource)
    {
        var command = FinancialAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.CreateCategoryAsync(command);
        return FinancialActionResultAssembler.ToCreatedActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpPut("categories/{categoryId:int}")]
    public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] UpdateFinancialCategoryResource resource)
    {
        var command = FinancialAssembler.ToCommand(CurrentUserId, categoryId, resource);
        var result = await commandService.UpdateCategoryAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpDelete("categories/{categoryId:int}")]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        var command = new DeleteCategoryCommand(CurrentUserId, categoryId);
        var result = await commandService.DeleteCategoryAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("categories/income")]
    public async Task<IActionResult> GetIncomeCategories()
    {
        var query = new GetCategoriesByTypeQuery(CurrentUserId, "income");
        var result = await queryService.GetCategoriesByTypeAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToResource).ToList()));
    }

    [HttpGet("categories/expense")]
    public async Task<IActionResult> GetExpenseCategories()
    {
        var query = new GetCategoriesByTypeQuery(CurrentUserId, "expense");
        var result = await queryService.GetCategoriesByTypeAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToResource).ToList()));
    }

    // ========================================================================
    // REPORTS
    // ========================================================================

    [HttpGet("reports/summary")]
    public async Task<IActionResult> GetReportSummary()
    {
        var query = new GetFinancialReportSummaryQuery(CurrentUserId);
        var result = await queryService.GetReportSummaryAsync(query);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpGet("reports/income-statement")]
    public async Task<IActionResult> GetIncomeStatement([FromQuery] string? period)
    {
        var query = new GetIncomeStatementQuery(CurrentUserId, period ?? "monthly");
        var result = await queryService.GetIncomeStatementAsync(query);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpGet("reports/cash-flow")]
    public async Task<IActionResult> GetCashFlow([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var query = new GetCashFlowQuery(CurrentUserId, start, end);
        var result = await queryService.GetCashFlowAsync(query);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpGet("reports/monthly-comparison")]
    public async Task<IActionResult> GetMonthlyComparison([FromQuery] int year)
    {
        var query = new GetMonthlyComparisonQuery(CurrentUserId, year);
        var result = await queryService.GetMonthlyComparisonAsync(query);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpGet("reports/yearly-comparison")]
    public async Task<IActionResult> GetYearlyComparison()
    {
        var query = new GetYearlyComparisonQuery(CurrentUserId);
        var result = await queryService.GetYearlyComparisonAsync(query);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpGet("reports/by-animal/{animalId:int}")]
    public async Task<IActionResult> GetReportByAnimal(int animalId)
    {
        var query = new GetReportByAnimalQuery(CurrentUserId, animalId);
        var result = await queryService.GetReportByAnimalAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToResource).ToList()));
    }

    [HttpGet("reports/by-category")]
    public async Task<IActionResult> GetReportByCategory()
    {
        var query = new GetReportByCategoryQuery(CurrentUserId);
        var result = await queryService.GetReportByCategoryAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToResource).ToList()));
    }

    [HttpGet("reports/profitability")]
    public async Task<IActionResult> GetProfitability()
    {
        var query = new GetProfitabilityQuery(CurrentUserId);
        var result = await queryService.GetProfitabilityAsync(query);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpGet("reports/profitability/by-animal")]
    public async Task<IActionResult> GetProfitabilityByAnimal()
    {
        var query = new GetProfitabilityByAnimalQuery(CurrentUserId);
        var result = await queryService.GetProfitabilityByAnimalAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToResource).ToList()));
    }

    [HttpGet("reports/profitability/by-race")]
    public async Task<IActionResult> GetProfitabilityByRace()
    {
        var query = new GetProfitabilityByRaceQuery(CurrentUserId);
        var result = await queryService.GetProfitabilityByRaceAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToResource).ToList()));
    }

    [HttpGet("reports/tax-summary")]
    public async Task<IActionResult> GetTaxSummary([FromQuery] int year)
    {
        var query = new GetTaxSummaryQuery(CurrentUserId, year);
        var result = await queryService.GetTaxSummaryAsync(query);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpGet("reports/export/{reportType}")]
    public async Task<IActionResult> ExportReport(string reportType, [FromQuery] string format = "pdf")
    {
        var query = new GetExportReportQuery(CurrentUserId, reportType, format);
        var result = await queryService.GetExportReportAsync(query);
        if (result.IsSuccess && format == "csv")
            return FinancialActionResultAssembler.ToFileResult(result, "text/csv", $"report-{reportType}.csv");
        if (result.IsSuccess && format == "excel")
            return FinancialActionResultAssembler.ToFileResult(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"report-{reportType}.xlsx");
        return FinancialActionResultAssembler.ToFileResult(result, "application/pdf", $"report-{reportType}.pdf");
    }

    [HttpGet("reports/dashboard")]
    public async Task<IActionResult> GetDashboardIndicators()
    {
        var query = new GetDashboardIndicatorsQuery(CurrentUserId);
        var result = await queryService.GetDashboardIndicatorsAsync(query);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpGet("reports/trends")]
    public async Task<IActionResult> GetFinancialTrends()
    {
        var query = new GetFinancialTrendsQuery(CurrentUserId);
        var result = await queryService.GetFinancialTrendsAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToResource).ToList()));
    }

    // ========================================================================
    // BUDGETS
    // ========================================================================

    [HttpGet("budgets")]
    public async Task<IActionResult> GetAllBudgets()
    {
        var query = new GetAllBudgetsQuery(CurrentUserId);
        var result = await queryService.GetAllBudgetsAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToResource).ToList()));
    }

    [HttpGet("budgets/{budgetId:int}")]
    public async Task<IActionResult> GetBudgetById(int budgetId)
    {
        var query = new GetBudgetByIdQuery(CurrentUserId, budgetId);
        var result = await queryService.GetBudgetByIdAsync(query);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpPost("budgets")]
    public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetResource resource)
    {
        var command = FinancialAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterBudgetAsync(command);
        return FinancialActionResultAssembler.ToCreatedActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpPut("budgets/{budgetId:int}")]
    public async Task<IActionResult> UpdateBudget(int budgetId, [FromBody] UpdateBudgetResource resource)
    {
        var command = FinancialAssembler.ToCommand(CurrentUserId, budgetId, resource);
        var result = await commandService.UpdateBudgetAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }

    [HttpDelete("budgets/{budgetId:int}")]
    public async Task<IActionResult> DeleteBudget(int budgetId)
    {
        var command = new DeleteBudgetCommand(CurrentUserId, budgetId);
        var result = await commandService.DeleteBudgetAsync(command);
        return FinancialActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("budgets/current")]
    public async Task<IActionResult> GetCurrentBudget()
    {
        var query = new GetCurrentBudgetQuery(CurrentUserId);
        var result = await queryService.GetCurrentBudgetAsync(query);
        return FinancialActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(FinancialAssembler.ToResource).ToList()));
    }

    [HttpGet("budgets/{budgetId:int}/vs-actual")]
    public async Task<IActionResult> GetBudgetVsActual(int budgetId)
    {
        var query = new GetBudgetVsActualQuery(CurrentUserId, budgetId);
        var result = await queryService.GetBudgetVsActualAsync(query);
        return FinancialActionResultAssembler.ToActionResult(result.Map(FinancialAssembler.ToResource));
    }
}
