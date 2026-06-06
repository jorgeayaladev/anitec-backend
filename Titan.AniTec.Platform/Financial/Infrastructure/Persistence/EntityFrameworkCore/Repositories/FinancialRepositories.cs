using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Financial.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Financial.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Titan.AniTec.Platform.Financial.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class TransactionRepository(AppDbContext context) : BaseRepository<Transaction>(context), ITransactionRepository
{
    public async Task<IReadOnlyCollection<Transaction>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Transaction>()
            .Where(t => t.FarmId == farmId)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Transaction>> FindByTypeAsync(int farmId, string type)
        => await Context.Set<Transaction>()
            .Where(t => t.FarmId == farmId && t.Type == type)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Transaction>> FindByCategoryAsync(int farmId, string category)
        => await Context.Set<Transaction>()
            .Where(t => t.FarmId == farmId && t.Category == category)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Transaction>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end)
        => await Context.Set<Transaction>()
            .Where(t => t.FarmId == farmId && t.TransactionDate >= start && t.TransactionDate <= end)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Transaction>> FindByAnimalIdAsync(int farmId, int animalId)
        => await Context.Set<Transaction>()
            .Where(t => t.FarmId == farmId && t.AnimalId == animalId)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();

    public async Task<decimal> GetTotalByTypeAndPeriodAsync(int farmId, string type, int year, int? month)
    {
        var query = Context.Set<Transaction>()
            .Where(t => t.FarmId == farmId && t.Type == type && t.TransactionDate.Year == year);

        if (month.HasValue)
            query = query.Where(t => t.TransactionDate.Month == month.Value);

        return await query.SumAsync(t => t.Amount);
    }
}

public class FinancialCategoryRepository(AppDbContext context) : BaseRepository<FinancialCategory>(context), IFinancialCategoryRepository
{
    public async Task<IReadOnlyCollection<FinancialCategory>> FindByFarmIdAsync(int farmId)
        => await Context.Set<FinancialCategory>()
            .Where(c => c.FarmId == farmId)
            .OrderBy(c => c.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<FinancialCategory>> FindByTypeAsync(int farmId, string type)
        => await Context.Set<FinancialCategory>()
            .Where(c => c.FarmId == farmId && c.Type == type)
            .OrderBy(c => c.Name)
            .ToListAsync();
}

public class BudgetRepository(AppDbContext context) : BaseRepository<Budget>(context), IBudgetRepository
{
    public async Task<IReadOnlyCollection<Budget>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Budget>()
            .Where(b => b.FarmId == farmId)
            .OrderBy(b => b.Year).ThenBy(b => b.Month).ThenBy(b => b.Category)
            .ToListAsync();

    public async Task<Budget?> FindByYearMonthCategoryAsync(int farmId, int year, int month, string category, string budgetType)
        => await Context.Set<Budget>()
            .FirstOrDefaultAsync(b => b.FarmId == farmId && b.Year == year && b.Month == month
                && b.Category == category && b.BudgetType == budgetType);

    public async Task<IReadOnlyCollection<Budget>> FindByYearAsync(int farmId, int year)
        => await Context.Set<Budget>()
            .Where(b => b.FarmId == farmId && b.Year == year)
            .OrderBy(b => b.Month).ThenBy(b => b.Category)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Budget>> FindByMonthAsync(int farmId, int year, int month)
        => await Context.Set<Budget>()
            .Where(b => b.FarmId == farmId && b.Year == year && b.Month == month)
            .OrderBy(b => b.Category)
            .ToListAsync();
}
