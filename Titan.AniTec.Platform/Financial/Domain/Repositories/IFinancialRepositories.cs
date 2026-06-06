using Titan.AniTec.Platform.Financial.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Financial.Domain.Repositories;

public interface ITransactionRepository : IBaseRepository<Transaction>
{
    Task<IReadOnlyCollection<Transaction>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Transaction>> FindByTypeAsync(int farmId, string type);
    Task<IReadOnlyCollection<Transaction>> FindByCategoryAsync(int farmId, string category);
    Task<IReadOnlyCollection<Transaction>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end);
    Task<IReadOnlyCollection<Transaction>> FindByAnimalIdAsync(int farmId, int animalId);
    Task<decimal> GetTotalByTypeAndPeriodAsync(int farmId, string type, int year, int? month);
}

public interface IBudgetRepository : IBaseRepository<Budget>
{
    Task<IReadOnlyCollection<Budget>> FindByFarmIdAsync(int farmId);
    Task<Budget?> FindByYearMonthCategoryAsync(int farmId, int year, int month, string category, string budgetType);
    Task<IReadOnlyCollection<Budget>> FindByYearAsync(int farmId, int year);
    Task<IReadOnlyCollection<Budget>> FindByMonthAsync(int farmId, int year, int month);
}

public interface IFinancialCategoryRepository : IBaseRepository<FinancialCategory>
{
    Task<IReadOnlyCollection<FinancialCategory>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<FinancialCategory>> FindByTypeAsync(int farmId, string type);
}
