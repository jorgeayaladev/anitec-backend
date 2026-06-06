using Titan.AniTec.Platform.Livestock.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Livestock.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Livestock.Application.CommandServices;

public interface ILivestockCommandService
{
    Task<Result<Animal>> RegisterAnimalAsync(RegisterAnimalCommand command);
    Task<Result<Animal>> UpdateAnimalAsync(UpdateAnimalCommand command);
    Task<Result> DeleteAnimalAsync(DeleteAnimalCommand command);
    Task<Result<Breed>> RegisterBreedAsync(RegisterBreedCommand command);
    Task<Result<Breed>> UpdateBreedAsync(UpdateBreedCommand command);
    Task<Result> DeleteBreedAsync(DeleteBreedCommand command);
    Task<Result<Birth>> RegisterBirthAsync(RegisterBirthCommand command);
    Task<Result<Birth>> UpdateBirthAsync(UpdateBirthCommand command);
    Task<Result> DeleteBirthAsync(DeleteBirthCommand command);
    Task<Result<Mating>> RegisterMatingAsync(RegisterMatingCommand command);
    Task<Result<Mating>> UpdateMatingAsync(UpdateMatingCommand command);
    Task<Result> DeleteMatingAsync(DeleteMatingCommand command);
    Task<Result<Mating>> ConfirmMatingAsync(ConfirmMatingCommand command);
    Task<Result<Weaning>> RegisterWeaningAsync(RegisterWeaningCommand command);
    Task<Result<Weaning>> UpdateWeaningAsync(UpdateWeaningCommand command);
    Task<Result> DeleteWeaningAsync(DeleteWeaningCommand command);
    Task<Result> RegisterAnimalBatchAsync(RegisterAnimalBatchCommand command);
    Task<Result> UpdateAnimalBatchAsync(UpdateAnimalBatchCommand command);
}

public interface ILivestockQueryService
{
    Task<Result<IReadOnlyCollection<Animal>>> GetAllAnimalsAsync(GetAllAnimalsQuery query);
    Task<Result<Animal>> GetAnimalByIdAsync(GetAnimalByIdQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> SearchAnimalsAsync(SearchAnimalsQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> GetAnimalsByBreedAsync(GetAnimalsByBreedQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> GetActiveAnimalsAsync(GetActiveAnimalsQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> GetInactiveAnimalsAsync(GetInactiveAnimalsQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> GetAnimalsBySexAsync(GetAnimalsBySexQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> GetAnimalsByAgeRangeAsync(GetAnimalsByAgeRangeQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> GetAnimalsByWeightRangeAsync(GetAnimalsByWeightRangeQuery query);
    Task<Result<IReadOnlyCollection<Breed>>> GetAllBreedsAsync(GetAllBreedsQuery query);
    Task<Result<Breed>> GetBreedByIdAsync(GetBreedByIdQuery query);
    Task<Result<IReadOnlyCollection<Breed>>> SearchBreedsAsync(SearchBreedsQuery query);
    Task<Result<IReadOnlyCollection<Birth>>> GetAllBirthsAsync(GetAllBirthsQuery query);
    Task<Result<Birth>> GetBirthByIdAsync(GetBirthByIdQuery query);
    Task<Result<IReadOnlyCollection<Birth>>> GetBirthsByDateRangeAsync(GetBirthsByDateRangeQuery query);
    Task<Result<IReadOnlyCollection<Birth>>> GetBirthsByMotherAsync(GetBirthsByMotherQuery query);
    Task<Result<IReadOnlyCollection<Birth>>> GetBirthsByFatherAsync(GetBirthsByFatherQuery query);
    Task<Result<IReadOnlyCollection<Mating>>> GetAllMatingsAsync(GetAllMatingsQuery query);
    Task<Result<Mating>> GetMatingByIdAsync(GetMatingByIdQuery query);
    Task<Result<IReadOnlyCollection<Mating>>> GetMatingsByFemaleAsync(GetMatingsByFemaleQuery query);
    Task<Result<IReadOnlyCollection<Mating>>> GetMatingsByMaleAsync(GetMatingsByMaleQuery query);
    Task<Result<IReadOnlyCollection<Mating>>> GetMatingsByDateRangeAsync(GetMatingsByDateRangeQuery query);
    Task<Result<IReadOnlyCollection<Mating>>> GetPendingMatingsAsync(GetPendingMatingsQuery query);
    Task<Result<IReadOnlyCollection<Weaning>>> GetAllWeaningsAsync(GetAllWeaningsQuery query);
    Task<Result<Weaning>> GetWeaningByIdAsync(GetWeaningByIdQuery query);
    Task<Result<IReadOnlyCollection<Weaning>>> GetWeaningsByCalfAsync(GetWeaningsByCalfQuery query);
    Task<Result<IReadOnlyCollection<Weaning>>> GetWeaningsByMotherAsync(GetWeaningsByMotherQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> FilterAnimalsAsync(FilterAnimalsQuery query);
    Task<Result<Animal>> GetAnimalByCodeAsync(GetAnimalByCodeQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> GetAnimalsByLocationAsync(GetAnimalsByLocationQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> GetAnimalGenealogyAsync(GetAnimalGenealogyQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> GetAnimalAncestorsAsync(GetAnimalAncestorsQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> GetAnimalDescendantsAsync(GetAnimalDescendantsQuery query);
    Task<Result<IReadOnlyCollection<Animal>>> GetAnimalSiblingsAsync(GetAnimalSiblingsQuery query);
    Task<Result<bool>> CheckInbreedingAsync(CheckInbreedingQuery query);
}
