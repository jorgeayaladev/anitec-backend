using Titan.AniTec.Platform.Device.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Device.Domain.Repositories;

public interface IDeviceTypeRepository : IBaseRepository<DeviceType>
{
    Task<IReadOnlyCollection<DeviceType>> FindAllAsync();
    Task<DeviceType?> FindByNameAsync(string name);
    Task<IReadOnlyCollection<DeviceType>> FindByCategoryAsync(string category);
}

public interface IDeviceRepository : IBaseRepository<FarmDevice>
{
    Task<IReadOnlyCollection<FarmDevice>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<FarmDevice>> FindByTypeAsync(int farmId, int deviceTypeId);
    Task<IReadOnlyCollection<FarmDevice>> FindByStatusAsync(int farmId, string status);
    Task<IReadOnlyCollection<FarmDevice>> FindByAnimalIdAsync(int farmId, int animalId);
    Task<IReadOnlyCollection<FarmDevice>> FindByLocationAsync(int farmId, int locationId);
    Task<IReadOnlyCollection<FarmDevice>> FindUnassignedAsync(int farmId);
    Task<IReadOnlyCollection<FarmDevice>> SearchAsync(int farmId, string term);
    Task<FarmDevice?> FindBySerialNumberAsync(int farmId, string serialNumber);
}

public interface IDeviceAssignmentRepository : IBaseRepository<DeviceAssignment>
{
    Task<DeviceAssignment?> FindActiveByDeviceIdAsync(int deviceId);
    Task<IReadOnlyCollection<DeviceAssignment>> FindHistoryByDeviceIdAsync(int deviceId);
    Task<IReadOnlyCollection<DeviceAssignment>> FindActiveByAnimalIdAsync(int animalId);
    Task<IReadOnlyCollection<DeviceAssignment>> FindActiveByFarmIdAsync(int farmId);
    Task<DeviceAssignment?> FindActiveByDeviceAndAnimalAsync(int deviceId, int animalId);
}

public interface IDeviceAlertRepository : IBaseRepository<DeviceAlert>
{
    Task<IReadOnlyCollection<DeviceAlert>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<DeviceAlert>> FindByDeviceIdAsync(int deviceId);
    Task<IReadOnlyCollection<DeviceAlert>> FindUnresolvedByFarmIdAsync(int farmId);
}
