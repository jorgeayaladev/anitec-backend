using Titan.AniTec.Platform.Analytics.Domain.Model.Aggregates;

namespace Titan.AniTec.Platform.Analytics.Domain.Repositories;

public interface IScheduledReportRepository
{
    Task<ScheduledReport?> FindByIdAsync(int id);
    Task<IReadOnlyCollection<ScheduledReport>> FindByFarmIdAsync(int farmId);
    Task AddAsync(ScheduledReport report);
    void Remove(ScheduledReport report);
}
