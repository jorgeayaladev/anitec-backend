using Titan.AniTec.Platform.Livestock.Domain.Model;
using Titan.AniTec.Platform.Livestock.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Livestock.Domain.Repositories;
using Titan.AniTec.Platform.Livestock.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Livestock.Application.Internal.QueryServices;

public class LivestockQueryService(
    IAnimalRepository animalRepository,
    IBreedRepository breedRepository,
    IBirthRepository birthRepository,
    IMatingRepository matingRepository,
    IWeaningRepository weaningRepository) : ILivestockQueryService
{
    public async Task<Result<IReadOnlyCollection<Animal>>> GetAllAnimalsAsync(GetAllAnimalsQuery query)
    {
        try
        {
            var animals = await animalRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Animal>>.Success(animals);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<Animal>> GetAnimalByIdAsync(GetAnimalByIdQuery query)
    {
        try
        {
            var animal = await animalRepository.FindWithParentsAsync(query.AnimalId);
            return animal != null
                ? Result<Animal>.Success(animal)
                : Result<Animal>.Failure(LivestockError.AnimalNotFound);
        }
        catch (Exception)
        {
            return Result<Animal>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> SearchAnimalsAsync(SearchAnimalsQuery query)
    {
        try
        {
            var animals = await animalRepository.SearchAsync(query.UserId, query.Term);
            return Result<IReadOnlyCollection<Animal>>.Success(animals);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> GetAnimalsByBreedAsync(GetAnimalsByBreedQuery query)
    {
        try
        {
            var animals = await animalRepository.FindByBreedIdAsync(query.BreedId);
            return Result<IReadOnlyCollection<Animal>>.Success(animals);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> GetActiveAnimalsAsync(GetActiveAnimalsQuery query)
    {
        try
        {
            var animals = await animalRepository.FindActiveByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Animal>>.Success(animals);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> GetInactiveAnimalsAsync(GetInactiveAnimalsQuery query)
    {
        try
        {
            var animals = await animalRepository.FindInactiveByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Animal>>.Success(animals);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> GetAnimalsBySexAsync(GetAnimalsBySexQuery query)
    {
        try
        {
            var animals = await animalRepository.FindBySexAsync(query.UserId, query.Sex);
            return Result<IReadOnlyCollection<Animal>>.Success(animals);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> GetAnimalsByAgeRangeAsync(GetAnimalsByAgeRangeQuery query)
    {
        try
        {
            var animals = await animalRepository.FindByAgeRangeAsync(query.UserId, query.AgeMin, query.AgeMax);
            return Result<IReadOnlyCollection<Animal>>.Success(animals);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> GetAnimalsByWeightRangeAsync(GetAnimalsByWeightRangeQuery query)
    {
        try
        {
            var animals = await animalRepository.FindByWeightRangeAsync(query.UserId, query.WeightMin, query.WeightMax);
            return Result<IReadOnlyCollection<Animal>>.Success(animals);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Breed>>> GetAllBreedsAsync(GetAllBreedsQuery query)
    {
        try
        {
            var breeds = await breedRepository.ListAsync();
            return Result<IReadOnlyCollection<Breed>>.Success(breeds.ToList());
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Breed>>.Failure(LivestockError.BreedNotFound);
        }
    }

    public async Task<Result<Breed>> GetBreedByIdAsync(GetBreedByIdQuery query)
    {
        try
        {
            var breed = await breedRepository.FindByIdAsync(query.BreedId);
            return breed != null
                ? Result<Breed>.Success(breed)
                : Result<Breed>.Failure(LivestockError.BreedNotFound);
        }
        catch (Exception)
        {
            return Result<Breed>.Failure(LivestockError.BreedNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Breed>>> SearchBreedsAsync(SearchBreedsQuery query)
    {
        try
        {
            var breeds = await breedRepository.SearchAsync(query.Term);
            return Result<IReadOnlyCollection<Breed>>.Success(breeds);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Breed>>.Failure(LivestockError.BreedNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Birth>>> GetAllBirthsAsync(GetAllBirthsQuery query)
    {
        try
        {
            var births = await birthRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Birth>>.Success(births);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Birth>>.Failure(LivestockError.BirthNotFound);
        }
    }

    public async Task<Result<Birth>> GetBirthByIdAsync(GetBirthByIdQuery query)
    {
        try
        {
            var birth = await birthRepository.FindByIdAsync(query.BirthId);
            return birth != null
                ? Result<Birth>.Success(birth)
                : Result<Birth>.Failure(LivestockError.BirthNotFound);
        }
        catch (Exception)
        {
            return Result<Birth>.Failure(LivestockError.BirthNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Birth>>> GetBirthsByDateRangeAsync(GetBirthsByDateRangeQuery query)
    {
        try
        {
            var births = await birthRepository.FindByDateRangeAsync(query.UserId, query.Start, query.End);
            return Result<IReadOnlyCollection<Birth>>.Success(births);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Birth>>.Failure(LivestockError.BirthNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Birth>>> GetBirthsByMotherAsync(GetBirthsByMotherQuery query)
    {
        try
        {
            var births = await birthRepository.FindByMotherIdAsync(query.MotherId);
            return Result<IReadOnlyCollection<Birth>>.Success(births);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Birth>>.Failure(LivestockError.BirthNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Birth>>> GetBirthsByFatherAsync(GetBirthsByFatherQuery query)
    {
        try
        {
            var births = await birthRepository.FindByFatherIdAsync(query.FatherId);
            return Result<IReadOnlyCollection<Birth>>.Success(births);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Birth>>.Failure(LivestockError.BirthNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Mating>>> GetAllMatingsAsync(GetAllMatingsQuery query)
    {
        try
        {
            var matings = await matingRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Mating>>.Success(matings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Mating>>.Failure(LivestockError.MatingNotFound);
        }
    }

    public async Task<Result<Mating>> GetMatingByIdAsync(GetMatingByIdQuery query)
    {
        try
        {
            var mating = await matingRepository.FindByIdAsync(query.MatingId);
            return mating != null
                ? Result<Mating>.Success(mating)
                : Result<Mating>.Failure(LivestockError.MatingNotFound);
        }
        catch (Exception)
        {
            return Result<Mating>.Failure(LivestockError.MatingNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Mating>>> GetMatingsByFemaleAsync(GetMatingsByFemaleQuery query)
    {
        try
        {
            var matings = await matingRepository.FindByFemaleIdAsync(query.FemaleId);
            return Result<IReadOnlyCollection<Mating>>.Success(matings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Mating>>.Failure(LivestockError.MatingNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Mating>>> GetMatingsByMaleAsync(GetMatingsByMaleQuery query)
    {
        try
        {
            var matings = await matingRepository.FindByMaleIdAsync(query.MaleId);
            return Result<IReadOnlyCollection<Mating>>.Success(matings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Mating>>.Failure(LivestockError.MatingNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Mating>>> GetMatingsByDateRangeAsync(GetMatingsByDateRangeQuery query)
    {
        try
        {
            var matings = await matingRepository.FindByDateRangeAsync(query.UserId, query.Start, query.End);
            return Result<IReadOnlyCollection<Mating>>.Success(matings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Mating>>.Failure(LivestockError.MatingNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Mating>>> GetPendingMatingsAsync(GetPendingMatingsQuery query)
    {
        try
        {
            var matings = await matingRepository.FindPendingByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Mating>>.Success(matings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Mating>>.Failure(LivestockError.MatingNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Weaning>>> GetAllWeaningsAsync(GetAllWeaningsQuery query)
    {
        try
        {
            var weanings = await weaningRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Weaning>>.Success(weanings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Weaning>>.Failure(LivestockError.WeaningNotFound);
        }
    }

    public async Task<Result<Weaning>> GetWeaningByIdAsync(GetWeaningByIdQuery query)
    {
        try
        {
            var weaning = await weaningRepository.FindByIdAsync(query.WeaningId);
            return weaning != null
                ? Result<Weaning>.Success(weaning)
                : Result<Weaning>.Failure(LivestockError.WeaningNotFound);
        }
        catch (Exception)
        {
            return Result<Weaning>.Failure(LivestockError.WeaningNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Weaning>>> GetWeaningsByCalfAsync(GetWeaningsByCalfQuery query)
    {
        try
        {
            var weanings = await weaningRepository.FindByCalfIdAsync(query.CalfId);
            return Result<IReadOnlyCollection<Weaning>>.Success(weanings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Weaning>>.Failure(LivestockError.WeaningNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Weaning>>> GetWeaningsByMotherAsync(GetWeaningsByMotherQuery query)
    {
        try
        {
            var weanings = await weaningRepository.FindByMotherIdAsync(query.MotherId);
            return Result<IReadOnlyCollection<Weaning>>.Success(weanings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Weaning>>.Failure(LivestockError.WeaningNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> FilterAnimalsAsync(FilterAnimalsQuery query)
    {
        try
        {
            var animals = await animalRepository.FindByFarmIdAsync(query.UserId);
            var filtered = animals.AsEnumerable();

            if (query.BreedId.HasValue)
                filtered = filtered.Where(a => a.BreedId == query.BreedId);
            if (query.AgeMin.HasValue)
                filtered = filtered.Where(a => a.DateOfBirth <= DateTime.Today.AddYears(-query.AgeMin.Value));
            if (query.AgeMax.HasValue)
                filtered = filtered.Where(a => a.DateOfBirth >= DateTime.Today.AddYears(-query.AgeMax.Value));
            if (!string.IsNullOrEmpty(query.Sex))
                filtered = filtered.Where(a => a.Sex.Equals(query.Sex, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(query.Status))
                filtered = filtered.Where(a => a.Status.Equals(query.Status, StringComparison.OrdinalIgnoreCase));

            return Result<IReadOnlyCollection<Animal>>.Success(filtered.ToList());
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<Animal>> GetAnimalByCodeAsync(GetAnimalByCodeQuery query)
    {
        try
        {
            var animal = await animalRepository.FindByCodeAsync(query.Code);
            return animal != null
                ? Result<Animal>.Success(animal)
                : Result<Animal>.Failure(LivestockError.AnimalNotFound);
        }
        catch (Exception)
        {
            return Result<Animal>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> GetAnimalsByLocationAsync(GetAnimalsByLocationQuery query)
    {
        try
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> GetAnimalGenealogyAsync(GetAnimalGenealogyQuery query)
    {
        try
        {
            var animal = await animalRepository.FindWithParentsAsync(query.AnimalId);
            if (animal == null)
                return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);

            var genealogy = new List<Animal> { animal };
            if (animal.MotherId.HasValue)
            {
                var mother = await animalRepository.FindByIdAsync(animal.MotherId.Value);
                if (mother != null) genealogy.Add(mother);
            }
            if (animal.FatherId.HasValue)
            {
                var father = await animalRepository.FindByIdAsync(animal.FatherId.Value);
                if (father != null) genealogy.Add(father);
            }

            return Result<IReadOnlyCollection<Animal>>.Success(genealogy);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> GetAnimalAncestorsAsync(GetAnimalAncestorsQuery query)
    {
        try
        {
            var animal = await animalRepository.FindWithParentsAsync(query.AnimalId);
            if (animal == null)
                return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);

            var ancestors = new List<Animal>();
            if (animal.MotherId.HasValue)
            {
                var mother = await animalRepository.FindByIdAsync(animal.MotherId.Value);
                if (mother != null) ancestors.Add(mother);
            }
            if (animal.FatherId.HasValue)
            {
                var father = await animalRepository.FindByIdAsync(animal.FatherId.Value);
                if (father != null) ancestors.Add(father);
            }

            return Result<IReadOnlyCollection<Animal>>.Success(ancestors);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> GetAnimalDescendantsAsync(GetAnimalDescendantsQuery query)
    {
        try
        {
            var descendants = await animalRepository.FindByMotherIdAsync(query.AnimalId);
            var fatherDescendants = await animalRepository.FindByFatherIdAsync(query.AnimalId);
            var all = descendants.Concat(fatherDescendants).Distinct().ToList();
            return Result<IReadOnlyCollection<Animal>>.Success(all);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Animal>>> GetAnimalSiblingsAsync(GetAnimalSiblingsQuery query)
    {
        try
        {
            var animal = await animalRepository.FindWithParentsAsync(query.AnimalId);
            if (animal == null)
                return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);

            var siblings = new List<Animal>();
            if (animal.MotherId.HasValue)
            {
                var motherSiblings = await animalRepository.FindByMotherIdAsync(animal.MotherId.Value);
                siblings.AddRange(motherSiblings.Where(s => s.Id != query.AnimalId));
            }
            if (animal.FatherId.HasValue)
            {
                var fatherSiblings = await animalRepository.FindByFatherIdAsync(animal.FatherId.Value);
                siblings.AddRange(fatherSiblings.Where(s => s.Id != query.AnimalId));
            }

            return Result<IReadOnlyCollection<Animal>>.Success(siblings.Distinct().ToList());
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Animal>>.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<bool>> CheckInbreedingAsync(CheckInbreedingQuery query)
    {
        try
        {
            var animal1 = await animalRepository.FindWithParentsAsync(query.AnimalId1);
            var animal2 = await animalRepository.FindWithParentsAsync(query.AnimalId2);
            if (animal1 == null || animal2 == null)
                return Result<bool>.Failure(LivestockError.AnimalNotFound);

            var inbreeding = (animal1.MotherId.HasValue && animal2.MotherId.HasValue &&
                              animal1.MotherId.Value == animal2.MotherId.Value) ||
                             (animal1.FatherId.HasValue && animal2.FatherId.HasValue &&
                              animal1.FatherId.Value == animal2.FatherId.Value);
            return Result<bool>.Success(inbreeding);
        }
        catch (Exception)
        {
            return Result<bool>.Failure(LivestockError.AnimalNotFound);
        }
    }
}
