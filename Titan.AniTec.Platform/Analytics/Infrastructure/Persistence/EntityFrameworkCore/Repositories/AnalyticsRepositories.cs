using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Analytics.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Analytics.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace Titan.AniTec.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ScheduledReportRepository(AppDbContext context) : IScheduledReportRepository
{
    public async Task<ScheduledReport?> FindByIdAsync(int id) =>
        await context.Set<ScheduledReport>().FindAsync(id);

    public async Task<IReadOnlyCollection<ScheduledReport>> FindByFarmIdAsync(int farmId) =>
        await context.Set<ScheduledReport>().Where(r => r.FarmId == farmId).ToListAsync();

    public async Task AddAsync(ScheduledReport report) =>
        await context.Set<ScheduledReport>().AddAsync(report);

    public void Remove(ScheduledReport report) =>
        context.Set<ScheduledReport>().Remove(report);
}
