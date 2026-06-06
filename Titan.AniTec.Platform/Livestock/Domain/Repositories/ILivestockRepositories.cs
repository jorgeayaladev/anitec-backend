using Titan.AniTec.Platform.Livestock.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Livestock.Domain.Repositories;

public interface IAnimalRepository : IBaseRepository<Animal>
{
    Task<IReadOnlyCollection<Animal>> FindByFarmIdAsync(int farmId);
    Task<Animal?> FindByCodeAsync(string code);
    Task<IReadOnlyCollection<Animal>> FindByBreedIdAsync(int breedId);
    Task<IReadOnlyCollection<Animal>> FindByMotherIdAsync(int motherId);
    Task<IReadOnlyCollection<Animal>> FindByFatherIdAsync(int fatherId);
    Task<IReadOnlyCollection<Animal>> FindActiveByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Animal>> FindInactiveByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Animal>> FindBySexAsync(int farmId, string sex);
    Task<IReadOnlyCollection<Animal>> SearchAsync(int farmId, string term);
    Task<IReadOnlyCollection<Animal>> FindByAgeRangeAsync(int farmId, int? ageMin, int? ageMax);
    Task<IReadOnlyCollection<Animal>> FindByWeightRangeAsync(int farmId, double? weightMin, double? weightMax);
    Task<Animal?> FindWithParentsAsync(int animalId);
}

public interface IBreedRepository : IBaseRepository<Breed>
{
    Task<IReadOnlyCollection<Breed>> SearchAsync(string term);
    Task<Breed?> FindByNameAndSpeciesAsync(string name, string species);
    Task<IReadOnlyCollection<Breed>> FindBySpeciesAsync(string species);
}

public interface IBirthRepository : IBaseRepository<Birth>
{
    Task<IReadOnlyCollection<Birth>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Birth>> FindByMotherIdAsync(int motherId);
    Task<IReadOnlyCollection<Birth>> FindByFatherIdAsync(int fatherId);
    Task<IReadOnlyCollection<Birth>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end);
}

public interface IMatingRepository : IBaseRepository<Mating>
{
    Task<IReadOnlyCollection<Mating>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Mating>> FindByFemaleIdAsync(int femaleId);
    Task<IReadOnlyCollection<Mating>> FindByMaleIdAsync(int maleId);
    Task<IReadOnlyCollection<Mating>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end);
    Task<IReadOnlyCollection<Mating>> FindPendingByFarmIdAsync(int farmId);
}

public interface IWeaningRepository : IBaseRepository<Weaning>
{
    Task<IReadOnlyCollection<Weaning>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Weaning>> FindByCalfIdAsync(int calfId);
    Task<IReadOnlyCollection<Weaning>> FindByMotherIdAsync(int motherId);
    Task<IReadOnlyCollection<Weaning>> FindUpcomingByFarmIdAsync(int farmId);
}
