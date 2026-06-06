using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Livestock.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Livestock.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Titan.AniTec.Platform.Livestock.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AnimalRepository(AppDbContext context) : BaseRepository<Animal>(context), IAnimalRepository
{
    public async Task<IReadOnlyCollection<Animal>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Animal>()
            .Where(a => a.FarmId == farmId)
            .OrderBy(a => a.Code)
            .ToListAsync();

    public async Task<Animal?> FindByCodeAsync(string code)
        => await Context.Set<Animal>()
            .FirstOrDefaultAsync(a => a.Code == code);

    public async Task<IReadOnlyCollection<Animal>> FindByBreedIdAsync(int breedId)
        => await Context.Set<Animal>()
            .Where(a => a.BreedId == breedId)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Animal>> FindByMotherIdAsync(int motherId)
        => await Context.Set<Animal>()
            .Where(a => a.MotherId == motherId)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Animal>> FindByFatherIdAsync(int fatherId)
        => await Context.Set<Animal>()
            .Where(a => a.FatherId == fatherId)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Animal>> FindActiveByFarmIdAsync(int farmId)
        => await Context.Set<Animal>()
            .Where(a => a.FarmId == farmId && a.Status == "active")
            .OrderBy(a => a.Code)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Animal>> FindInactiveByFarmIdAsync(int farmId)
        => await Context.Set<Animal>()
            .Where(a => a.FarmId == farmId && a.Status != "active")
            .OrderBy(a => a.Code)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Animal>> FindBySexAsync(int farmId, string sex)
        => await Context.Set<Animal>()
            .Where(a => a.FarmId == farmId && a.Sex == sex)
            .OrderBy(a => a.Code)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Animal>> SearchAsync(int farmId, string term)
        => await Context.Set<Animal>()
            .Where(a => a.FarmId == farmId &&
                (a.Code.Contains(term) || a.Name!.Contains(term) || a.Color!.Contains(term)))
            .OrderBy(a => a.Code)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Animal>> FindByAgeRangeAsync(int farmId, int? ageMin, int? ageMax)
    {
        var query = Context.Set<Animal>().Where(a => a.FarmId == farmId);

        if (ageMin.HasValue)
        {
            var minDate = DateTime.UtcNow.AddYears(-ageMin.Value);
            query = query.Where(a => a.DateOfBirth <= minDate);
        }

        if (ageMax.HasValue)
        {
            var maxDate = DateTime.UtcNow.AddYears(-ageMax.Value);
            query = query.Where(a => a.DateOfBirth >= maxDate);
        }

        return await query.OrderBy(a => a.Code).ToListAsync();
    }

    public async Task<IReadOnlyCollection<Animal>> FindByWeightRangeAsync(int farmId, double? weightMin, double? weightMax)
    {
        var query = Context.Set<Animal>().Where(a => a.FarmId == farmId && a.Weight != null);

        if (weightMin.HasValue)
            query = query.Where(a => a.Weight >= weightMin.Value);

        if (weightMax.HasValue)
            query = query.Where(a => a.Weight <= weightMax.Value);

        return await query.OrderBy(a => a.Code).ToListAsync();
    }

    public async Task<Animal?> FindWithParentsAsync(int animalId)
        => await Context.Set<Animal>()
            .FirstOrDefaultAsync(a => a.Id == animalId);
}

public class BreedRepository(AppDbContext context) : BaseRepository<Breed>(context), IBreedRepository
{
    public async Task<IReadOnlyCollection<Breed>> SearchAsync(string term)
        => await Context.Set<Breed>()
            .Where(b => b.Name.Contains(term) || b.Species.Contains(term))
            .OrderBy(b => b.Name)
            .ToListAsync();

    public async Task<Breed?> FindByNameAndSpeciesAsync(string name, string species)
        => await Context.Set<Breed>()
            .FirstOrDefaultAsync(b => b.Name == name && b.Species == species);

    public async Task<IReadOnlyCollection<Breed>> FindBySpeciesAsync(string species)
        => await Context.Set<Breed>()
            .Where(b => b.Species == species)
            .OrderBy(b => b.Name)
            .ToListAsync();
}

public class BirthRepository(AppDbContext context) : BaseRepository<Birth>(context), IBirthRepository
{
    public async Task<IReadOnlyCollection<Birth>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Birth>()
            .Where(b => b.FarmId == farmId)
            .OrderByDescending(b => b.BirthDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Birth>> FindByMotherIdAsync(int motherId)
        => await Context.Set<Birth>()
            .Where(b => b.MotherId == motherId)
            .OrderByDescending(b => b.BirthDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Birth>> FindByFatherIdAsync(int fatherId)
        => await Context.Set<Birth>()
            .Where(b => b.FatherId == fatherId)
            .OrderByDescending(b => b.BirthDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Birth>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end)
        => await Context.Set<Birth>()
            .Where(b => b.FarmId == farmId && b.BirthDate >= start && b.BirthDate <= end)
            .OrderByDescending(b => b.BirthDate)
            .ToListAsync();
}

public class MatingRepository(AppDbContext context) : BaseRepository<Mating>(context), IMatingRepository
{
    public async Task<IReadOnlyCollection<Mating>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Mating>()
            .Where(m => m.FarmId == farmId)
            .OrderByDescending(m => m.MatingDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Mating>> FindByFemaleIdAsync(int femaleId)
        => await Context.Set<Mating>()
            .Where(m => m.FemaleId == femaleId)
            .OrderByDescending(m => m.MatingDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Mating>> FindByMaleIdAsync(int maleId)
        => await Context.Set<Mating>()
            .Where(m => m.MaleId == maleId)
            .OrderByDescending(m => m.MatingDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Mating>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end)
        => await Context.Set<Mating>()
            .Where(m => m.FarmId == farmId && m.MatingDate >= start && m.MatingDate <= end)
            .OrderByDescending(m => m.MatingDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Mating>> FindPendingByFarmIdAsync(int farmId)
        => await Context.Set<Mating>()
            .Where(m => m.FarmId == farmId && m.Result == null)
            .OrderByDescending(m => m.MatingDate)
            .ToListAsync();
}

public class WeaningRepository(AppDbContext context) : BaseRepository<Weaning>(context), IWeaningRepository
{
    public async Task<IReadOnlyCollection<Weaning>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Weaning>()
            .Where(w => w.FarmId == farmId)
            .OrderByDescending(w => w.WeaningDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Weaning>> FindByCalfIdAsync(int calfId)
        => await Context.Set<Weaning>()
            .Where(w => w.CalfId == calfId)
            .OrderByDescending(w => w.WeaningDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Weaning>> FindByMotherIdAsync(int motherId)
        => await Context.Set<Weaning>()
            .Where(w => w.MotherId == motherId)
            .OrderByDescending(w => w.WeaningDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Weaning>> FindUpcomingByFarmIdAsync(int farmId)
        => await Context.Set<Weaning>()
            .Where(w => w.FarmId == farmId && w.WeaningDate >= DateTime.UtcNow)
            .OrderBy(w => w.WeaningDate)
            .ToListAsync();
}
