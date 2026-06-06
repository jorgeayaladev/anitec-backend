using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Device.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Device.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Titan.AniTec.Platform.Device.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class DeviceTypeRepository(AppDbContext context)
    : BaseRepository<DeviceType>(context), IDeviceTypeRepository
{
    public async Task<IReadOnlyCollection<DeviceType>> FindAllAsync()
        => await Context.Set<DeviceType>()
            .OrderBy(t => t.Category).ThenBy(t => t.Name)
            .ToListAsync();

    public async Task<DeviceType?> FindByNameAsync(string name)
        => await Context.Set<DeviceType>()
            .FirstOrDefaultAsync(t => t.Name == name);

    public async Task<IReadOnlyCollection<DeviceType>> FindByCategoryAsync(string category)
        => await Context.Set<DeviceType>()
            .Where(t => t.Category == category)
            .OrderBy(t => t.Name)
            .ToListAsync();
}

public class DeviceRepository(AppDbContext context)
    : BaseRepository<FarmDevice>(context), IDeviceRepository
{
    public async Task<IReadOnlyCollection<FarmDevice>> FindByFarmIdAsync(int farmId)
        => await Context.Set<FarmDevice>()
            .Where(d => d.FarmId == farmId)
            .OrderBy(d => d.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<FarmDevice>> FindByTypeAsync(int farmId, int deviceTypeId)
        => await Context.Set<FarmDevice>()
            .Where(d => d.FarmId == farmId && d.DeviceTypeId == deviceTypeId)
            .OrderBy(d => d.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<FarmDevice>> FindByStatusAsync(int farmId, string status)
        => await Context.Set<FarmDevice>()
            .Where(d => d.FarmId == farmId && d.Status == status)
            .OrderBy(d => d.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<FarmDevice>> FindByAnimalIdAsync(int farmId, int animalId)
        => await Context.Set<FarmDevice>()
            .Where(d => d.FarmId == farmId && d.CurrentAnimalId == animalId)
            .OrderBy(d => d.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<FarmDevice>> FindByLocationAsync(int farmId, int locationId)
        => await Context.Set<FarmDevice>()
            .Where(d => d.FarmId == farmId && d.CurrentLocationId == locationId)
            .OrderBy(d => d.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<FarmDevice>> FindUnassignedAsync(int farmId)
        => await Context.Set<FarmDevice>()
            .Where(d => d.FarmId == farmId && d.CurrentAnimalId == null)
            .OrderBy(d => d.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<FarmDevice>> SearchAsync(int farmId, string term)
        => await Context.Set<FarmDevice>()
            .Where(d => d.FarmId == farmId
                     && (d.Name.Contains(term) || d.SerialNumber.Contains(term)))
            .OrderBy(d => d.Name)
            .ToListAsync();

    public async Task<FarmDevice?> FindBySerialNumberAsync(int farmId, string serialNumber)
        => await Context.Set<FarmDevice>()
            .FirstOrDefaultAsync(d => d.FarmId == farmId && d.SerialNumber == serialNumber);
}

public class DeviceAssignmentRepository(AppDbContext context)
    : BaseRepository<DeviceAssignment>(context), IDeviceAssignmentRepository
{
    public async Task<DeviceAssignment?> FindActiveByDeviceIdAsync(int deviceId)
        => await Context.Set<DeviceAssignment>()
            .FirstOrDefaultAsync(a => a.DeviceId == deviceId && a.IsActive);

    public async Task<IReadOnlyCollection<DeviceAssignment>> FindHistoryByDeviceIdAsync(int deviceId)
        => await Context.Set<DeviceAssignment>()
            .Where(a => a.DeviceId == deviceId)
            .OrderByDescending(a => a.AssignedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<DeviceAssignment>> FindActiveByAnimalIdAsync(int animalId)
        => await Context.Set<DeviceAssignment>()
            .Where(a => a.AnimalId == animalId && a.IsActive)
            .ToListAsync();

    public async Task<IReadOnlyCollection<DeviceAssignment>> FindActiveByFarmIdAsync(int farmId)
        => await Context.Set<DeviceAssignment>()
            .Where(a => a.FarmId == farmId && a.IsActive)
            .ToListAsync();

    public async Task<DeviceAssignment?> FindActiveByDeviceAndAnimalAsync(int deviceId, int animalId)
        => await Context.Set<DeviceAssignment>()
            .FirstOrDefaultAsync(a => a.DeviceId == deviceId && a.AnimalId == animalId && a.IsActive);
}

public class DeviceAlertRepository(AppDbContext context)
    : BaseRepository<DeviceAlert>(context), IDeviceAlertRepository
{
    public async Task<IReadOnlyCollection<DeviceAlert>> FindByFarmIdAsync(int farmId)
        => await Context.Set<DeviceAlert>()
            .Where(a => a.FarmId == farmId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<DeviceAlert>> FindByDeviceIdAsync(int deviceId)
        => await Context.Set<DeviceAlert>()
            .Where(a => a.DeviceId == deviceId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<DeviceAlert>> FindUnresolvedByFarmIdAsync(int farmId)
        => await Context.Set<DeviceAlert>()
            .Where(a => a.FarmId == farmId && !a.IsResolved)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
}
