using Titan.AniTec.Platform.Financial.Domain.Model;
using Titan.AniTec.Platform.Financial.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Financial.Domain.Repositories;
using Titan.AniTec.Platform.Financial.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Financial.Application.Internal.QueryServices;

public class FinancialQueryService(
    ITransactionRepository transactionRepository,
    IBudgetRepository budgetRepository,
    IFinancialCategoryRepository categoryRepository) : IFinancialQueryService
{
    public async Task<Result<IReadOnlyCollection<Transaction>>> GetAllTransactionsAsync(GetAllTransactionsQuery query)
    {
        try
        {
            var items = await transactionRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Transaction>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Transaction>>.Failure(FinancialError.TransactionNotFound);
        }
    }

    public async Task<Result<Transaction>> GetTransactionByIdAsync(GetTransactionByIdQuery query)
    {
        try
        {
            var item = await transactionRepository.FindByIdAsync(query.TransactionId);
            return item != null
                ? Result<Transaction>.Success(item)
                : Result<Transaction>.Failure(FinancialError.TransactionNotFound);
        }
        catch (Exception)
        {
            return Result<Transaction>.Failure(FinancialError.TransactionNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Transaction>>> GetTransactionsByTypeAsync(GetTransactionsByTypeQuery query)
    {
        try
        {
            var items = await transactionRepository.FindByTypeAsync(query.UserId, query.Type);
            return Result<IReadOnlyCollection<Transaction>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Transaction>>.Failure(FinancialError.TransactionNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Transaction>>> GetTransactionsByCategoryAsync(GetTransactionsByCategoryQuery query)
    {
        try
        {
            var items = await transactionRepository.FindByCategoryAsync(query.UserId, query.Category);
            return Result<IReadOnlyCollection<Transaction>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Transaction>>.Failure(FinancialError.TransactionNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Transaction>>> GetTransactionsByDateRangeAsync(GetTransactionsByDateRangeQuery query)
    {
        try
        {
            var items = await transactionRepository.FindByDateRangeAsync(query.UserId, query.Start, query.End);
            return Result<IReadOnlyCollection<Transaction>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Transaction>>.Failure(FinancialError.TransactionNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Transaction>>> GetTransactionsByAnimalAsync(GetTransactionsByAnimalQuery query)
    {
        try
        {
            var items = await transactionRepository.FindByAnimalIdAsync(query.UserId, query.AnimalId);
            return Result<IReadOnlyCollection<Transaction>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Transaction>>.Failure(FinancialError.TransactionNotFound);
        }
    }

    public async Task<Result<decimal>> GetIncomeSummaryAsync(GetIncomeSummaryQuery query)
    {
        try
        {
            var total = await transactionRepository.GetTotalByTypeAndPeriodAsync(
                query.UserId, "income", query.Year, query.Month);
            return Result<decimal>.Success(total);
        }
        catch (Exception)
        {
            return Result<decimal>.Failure(FinancialError.TransactionNotFound);
        }
    }

    public async Task<Result<decimal>> GetExpenseSummaryAsync(GetExpenseSummaryQuery query)
    {
        try
        {
            var total = await transactionRepository.GetTotalByTypeAndPeriodAsync(
                query.UserId, "expense", query.Year, query.Month);
            return Result<decimal>.Success(total);
        }
        catch (Exception)
        {
            return Result<decimal>.Failure(FinancialError.TransactionNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Budget>>> GetAllBudgetsAsync(GetAllBudgetsQuery query)
    {
        try
        {
            var items = await budgetRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Budget>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Budget>>.Failure(FinancialError.BudgetNotFound);
        }
    }

    public async Task<Result<Budget>> GetBudgetByIdAsync(GetBudgetByIdQuery query)
    {
        try
        {
            var item = await budgetRepository.FindByIdAsync(query.BudgetId);
            return item != null
                ? Result<Budget>.Success(item)
                : Result<Budget>.Failure(FinancialError.BudgetNotFound);
        }
        catch (Exception)
        {
            return Result<Budget>.Failure(FinancialError.BudgetNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Budget>>> GetBudgetsByYearAsync(GetBudgetsByYearQuery query)
    {
        try
        {
            var items = await budgetRepository.FindByYearAsync(query.UserId, query.Year);
            return Result<IReadOnlyCollection<Budget>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Budget>>.Failure(FinancialError.BudgetNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Budget>>> GetBudgetsByMonthAsync(GetBudgetsByMonthQuery query)
    {
        try
        {
            var items = await budgetRepository.FindByMonthAsync(query.UserId, query.Year, query.Month);
            return Result<IReadOnlyCollection<Budget>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Budget>>.Failure(FinancialError.BudgetNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Budget>>> GetCurrentBudgetAsync(GetCurrentBudgetQuery query)
    {
        try
        {
            var now = DateTime.UtcNow;
            var items = await budgetRepository.FindByMonthAsync(query.UserId, now.Year, now.Month);
            return Result<IReadOnlyCollection<Budget>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Budget>>.Failure(FinancialError.BudgetNotFound);
        }
    }

    // Categories
    public async Task<Result<IReadOnlyCollection<FinancialCategory>>> GetAllCategoriesAsync(GetAllCategoriesQuery query)
    {
        try
        {
            var items = await categoryRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<FinancialCategory>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FinancialCategory>>.Failure(FinancialError.CategoryNotFound);
        }
    }

    public async Task<Result<FinancialCategory>> GetCategoryByIdAsync(GetCategoryByIdQuery query)
    {
        try
        {
            var item = await categoryRepository.FindByIdAsync(query.CategoryId);
            return item != null
                ? Result<FinancialCategory>.Success(item)
                : Result<FinancialCategory>.Failure(FinancialError.CategoryNotFound);
        }
        catch (Exception)
        {
            return Result<FinancialCategory>.Failure(FinancialError.CategoryNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<FinancialCategory>>> GetCategoriesByTypeAsync(GetCategoriesByTypeQuery query)
    {
        try
        {
            var items = await categoryRepository.FindByTypeAsync(query.UserId, query.Type);
            return Result<IReadOnlyCollection<FinancialCategory>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FinancialCategory>>.Failure(FinancialError.CategoryNotFound);
        }
    }

    // Reports
    public async Task<Result<FinancialReportSummary>> GetReportSummaryAsync(GetFinancialReportSummaryQuery query)
    {
        try
        {
            var transactions = await transactionRepository.FindByFarmIdAsync(query.UserId);
            var incomes = transactions.Where(t => t.Type == "income").ToList();
            var expenses = transactions.Where(t => t.Type == "expense").ToList();
            var totalIncome = incomes.Sum(t => t.Amount);
            var totalExpense = expenses.Sum(t => t.Amount);
            var result = new FinancialReportSummary(totalIncome, totalExpense, totalIncome - totalExpense,
                transactions.Count, incomes.Count, expenses.Count);
            return Result<FinancialReportSummary>.Success(result);
        }
        catch (Exception)
        {
            return Result<FinancialReportSummary>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<IncomeStatement>> GetIncomeStatementAsync(GetIncomeStatementQuery query)
    {
        try
        {
            var transactions = await transactionRepository.FindByFarmIdAsync(query.UserId);
            var totalIncome = transactions.Where(t => t.Type == "income").Sum(t => t.Amount);
            var totalExpense = transactions.Where(t => t.Type == "expense").Sum(t => t.Amount);
            var incomeLines = transactions.Where(t => t.Type == "income")
                .GroupBy(t => t.Category)
                .Select(g => new IncomeStatementLine(g.Key, g.Sum(t => t.Amount),
                    totalIncome > 0 ? Math.Round(g.Sum(t => t.Amount) / totalIncome * 100, 2) : 0))
                .ToList();
            var result = new IncomeStatement(totalIncome, totalExpense, totalIncome - totalExpense, incomeLines);
            return Result<IncomeStatement>.Success(result);
        }
        catch (Exception)
        {
            return Result<IncomeStatement>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<CashFlowReport>> GetCashFlowAsync(GetCashFlowQuery query)
    {
        try
        {
            var transactions = await transactionRepository.FindByDateRangeAsync(query.UserId, query.Start, query.End);
            var totalInflows = transactions.Where(t => t.Type == "income").Sum(t => t.Amount);
            var totalOutflows = transactions.Where(t => t.Type == "expense").Sum(t => t.Amount);
            var lines = transactions.OrderBy(t => t.TransactionDate)
                .Select(t => new CashFlowLine(t.TransactionDate, t.Description, t.Type, t.Amount))
                .ToList();
            var prevTransactions = await transactionRepository.FindByDateRangeAsync(
                query.UserId, query.Start.AddMonths(-1), query.Start.AddDays(-1));
            var openingBalance = prevTransactions.Where(t => t.Type == "income").Sum(t => t.Amount)
                - prevTransactions.Where(t => t.Type == "expense").Sum(t => t.Amount);
            var result = new CashFlowReport(openingBalance, totalInflows, totalOutflows,
                openingBalance + totalInflows - totalOutflows, lines);
            return Result<CashFlowReport>.Success(result);
        }
        catch (Exception)
        {
            return Result<CashFlowReport>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<MonthlyComparisonReport>> GetMonthlyComparisonAsync(GetMonthlyComparisonQuery query)
    {
        try
        {
            var transactions = await transactionRepository.FindByFarmIdAsync(query.UserId);
            var lines = Enumerable.Range(1, 12).Select(month =>
            {
                var monthTx = transactions.Where(t => t.TransactionDate.Year == query.Year && t.TransactionDate.Month == month);
                var income = monthTx.Where(t => t.Type == "income").Sum(t => t.Amount);
                var expense = monthTx.Where(t => t.Type == "expense").Sum(t => t.Amount);
                return new MonthlyComparisonLine(month, income, expense, income - expense);
            }).ToList();
            return Result<MonthlyComparisonReport>.Success(new MonthlyComparisonReport(lines));
        }
        catch (Exception)
        {
            return Result<MonthlyComparisonReport>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<YearlyComparisonReport>> GetYearlyComparisonAsync(GetYearlyComparisonQuery query)
    {
        try
        {
            var transactions = await transactionRepository.FindByFarmIdAsync(query.UserId);
            var years = transactions.Select(t => t.TransactionDate.Year).Distinct().OrderBy(y => y).ToList();
            var lines = years.Select(year =>
            {
                var yearTx = transactions.Where(t => t.TransactionDate.Year == year);
                var income = yearTx.Where(t => t.Type == "income").Sum(t => t.Amount);
                var expense = yearTx.Where(t => t.Type == "expense").Sum(t => t.Amount);
                return new YearlyComparisonLine(year, income, expense, income - expense);
            }).ToList();
            return Result<YearlyComparisonReport>.Success(new YearlyComparisonReport(lines));
        }
        catch (Exception)
        {
            return Result<YearlyComparisonReport>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<IReadOnlyCollection<ReportByAnimal>>> GetReportByAnimalAsync(GetReportByAnimalQuery query)
    {
        try
        {
            var transactions = await transactionRepository.FindByAnimalIdAsync(query.UserId, query.AnimalId);
            var income = transactions.Where(t => t.Type == "income").Sum(t => t.Amount);
            var expense = transactions.Where(t => t.Type == "expense").Sum(t => t.Amount);
            var result = new List<ReportByAnimal> { new(income, expense, income - expense, query.AnimalId) };
            return Result<IReadOnlyCollection<ReportByAnimal>>.Success(result);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<ReportByAnimal>>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<IReadOnlyCollection<ReportByCategoryLine>>> GetReportByCategoryAsync(GetReportByCategoryQuery query)
    {
        try
        {
            var transactions = await transactionRepository.FindByFarmIdAsync(query.UserId);
            var lines = transactions.GroupBy(t => t.Category).Select(g =>
            {
                var income = g.Where(t => t.Type == "income").Sum(t => t.Amount);
                var expense = g.Where(t => t.Type == "expense").Sum(t => t.Amount);
                return new ReportByCategoryLine(g.Key, income, expense, income - expense);
            }).ToList();
            return Result<IReadOnlyCollection<ReportByCategoryLine>>.Success(lines);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<ReportByCategoryLine>>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<ProfitabilityReport>> GetProfitabilityAsync(GetProfitabilityQuery query)
    {
        try
        {
            var transactions = await transactionRepository.FindByFarmIdAsync(query.UserId);
            var income = transactions.Where(t => t.Type == "income").Sum(t => t.Amount);
            var expense = transactions.Where(t => t.Type == "expense").Sum(t => t.Amount);
            var profit = income - expense;
            var margin = income > 0 ? Math.Round(profit / income * 100, 2) : 0;
            var animalIds = transactions.Where(t => t.AnimalId.HasValue).Select(t => t.AnimalId!.Value).Distinct().Count();
            var costPerAnimal = animalIds > 0 ? Math.Round(expense / animalIds, 2) : 0;
            var result = new ProfitabilityReport(income, expense, profit, margin, costPerAnimal);
            return Result<ProfitabilityReport>.Success(result);
        }
        catch (Exception)
        {
            return Result<ProfitabilityReport>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<IReadOnlyCollection<ProfitabilityByAnimalLine>>> GetProfitabilityByAnimalAsync(GetProfitabilityByAnimalQuery query)
    {
        try
        {
            var transactions = await transactionRepository.FindByFarmIdAsync(query.UserId);
            var lines = transactions.Where(t => t.AnimalId.HasValue)
                .GroupBy(t => t.AnimalId!.Value)
                .Select(g =>
                {
                    var income = g.Where(t => t.Type == "income").Sum(t => t.Amount);
                    var expense = g.Where(t => t.Type == "expense").Sum(t => t.Amount);
                    return new ProfitabilityByAnimalLine(g.Key, income, expense, income - expense);
                }).ToList();
            return Result<IReadOnlyCollection<ProfitabilityByAnimalLine>>.Success(lines);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<ProfitabilityByAnimalLine>>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<IReadOnlyCollection<ProfitabilityByRaceLine>>> GetProfitabilityByRaceAsync(GetProfitabilityByRaceQuery query)
    {
        try
        {
            return Result<IReadOnlyCollection<ProfitabilityByRaceLine>>.Success(new List<ProfitabilityByRaceLine>());
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<ProfitabilityByRaceLine>>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<TaxSummary>> GetTaxSummaryAsync(GetTaxSummaryQuery query)
    {
        try
        {
            var transactions = await transactionRepository.FindByFarmIdAsync(query.UserId);
            var yearTx = transactions.Where(t => t.TransactionDate.Year == query.Year);
            var income = yearTx.Where(t => t.Type == "income").Sum(t => t.Amount);
            var expense = yearTx.Where(t => t.Type == "expense").Sum(t => t.Amount);
            var taxable = Math.Max(0, income - expense);
            var tax = Math.Round(taxable * 0.15m, 2);
            var result = new TaxSummary(income, expense, taxable, tax, query.Year);
            return Result<TaxSummary>.Success(result);
        }
        catch (Exception)
        {
            return Result<TaxSummary>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<byte[]>> GetExportReportAsync(GetExportReportQuery query)
    {
        try
        {
            var summary = await GetReportSummaryAsync(new GetFinancialReportSummaryQuery(query.UserId));
            if (!summary.IsSuccess)
                return Result<byte[]>.Failure(FinancialError.ReportDataNotAvailable);

            var csv = $"TotalIncome,TotalExpense,NetBalance\n{summary.Value.TotalIncome},{summary.Value.TotalExpense},{summary.Value.NetBalance}\n";
            var bytes = System.Text.Encoding.UTF8.GetBytes(csv);
            return Result<byte[]>.Success(bytes);
        }
        catch (Exception)
        {
            return Result<byte[]>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<DashboardIndicators>> GetDashboardIndicatorsAsync(GetDashboardIndicatorsQuery query)
    {
        try
        {
            var now = DateTime.UtcNow;
            var transactions = await transactionRepository.FindByFarmIdAsync(query.UserId);
            var currentMonthTx = transactions.Where(t =>
                t.TransactionDate.Year == now.Year && t.TransactionDate.Month == now.Month);
            var income = currentMonthTx.Where(t => t.Type == "income").Sum(t => t.Amount);
            var expense = currentMonthTx.Where(t => t.Type == "expense").Sum(t => t.Amount);
            var budgets = await budgetRepository.FindByMonthAsync(query.UserId, now.Year, now.Month);
            var budgetUtil = budgets.Sum(b => b.PlannedAmount) > 0
                ? Math.Round(expense / budgets.Sum(b => b.PlannedAmount) * 100, 2) : 0;
            var prevMonth = now.AddMonths(-1);
            var prevIncome = transactions.Where(t =>
                t.TransactionDate.Year == prevMonth.Year && t.TransactionDate.Month == prevMonth.Month
                && t.Type == "income").Sum(t => t.Amount);
            var growth = prevIncome > 0 ? Math.Round((income - prevIncome) / prevIncome * 100, 2) : 0;
            var result = new DashboardIndicators(income, expense, 0, budgets.Count, budgetUtil, growth);
            return Result<DashboardIndicators>.Success(result);
        }
        catch (Exception)
        {
            return Result<DashboardIndicators>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<IReadOnlyCollection<FinancialTrendLine>>> GetFinancialTrendsAsync(GetFinancialTrendsQuery query)
    {
        try
        {
            var transactions = await transactionRepository.FindByFarmIdAsync(query.UserId);
            var lines = transactions.GroupBy(t => t.TransactionDate.Date)
                .OrderBy(g => g.Key)
                .Select(g =>
                {
                    var income = g.Where(t => t.Type == "income").Sum(t => t.Amount);
                    var expense = g.Where(t => t.Type == "expense").Sum(t => t.Amount);
                    return new FinancialTrendLine(g.Key, income, expense, income - expense);
                }).ToList();
            return Result<IReadOnlyCollection<FinancialTrendLine>>.Success(lines);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FinancialTrendLine>>.Failure(FinancialError.ReportDataNotAvailable);
        }
    }

    public async Task<Result<BudgetVsActual>> GetBudgetVsActualAsync(GetBudgetVsActualQuery query)
    {
        try
        {
            var budget = await budgetRepository.FindByIdAsync(query.BudgetId);
            if (budget == null)
                return Result<BudgetVsActual>.Failure(FinancialError.BudgetNotFound);

            var transactions = await transactionRepository.FindByDateRangeAsync(
                query.UserId,
                new DateTime(budget.Year, budget.Month, 1),
                new DateTime(budget.Year, budget.Month, DateTime.DaysInMonth(budget.Year, budget.Month)));

            var actualAmount = transactions
                .Where(t => t.Type.Equals(budget.BudgetType, StringComparison.OrdinalIgnoreCase)
                            && t.Category.Equals(budget.Category, StringComparison.OrdinalIgnoreCase))
                .Sum(t => t.Amount);

            var result = new BudgetVsActual(
                budget.Id, budget.Category, budget.BudgetType,
                budget.PlannedAmount, actualAmount, actualAmount - budget.PlannedAmount,
                budget.Year, budget.Month);

            return Result<BudgetVsActual>.Success(result);
        }
        catch (Exception)
        {
            return Result<BudgetVsActual>.Failure(FinancialError.BudgetNotFound);
        }
    }
}
