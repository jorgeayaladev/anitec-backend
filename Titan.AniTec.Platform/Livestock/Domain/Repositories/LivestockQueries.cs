namespace Titan.AniTec.Platform.Livestock.Domain.Repositories;

public record GetAllAnimalsQuery(int UserId);
public record GetAnimalByCodeQuery(int UserId, string Code);
public record GetAnimalsByLocationQuery(int UserId, int LocationId);
public record GetAnimalByIdQuery(int UserId, int AnimalId);
public record SearchAnimalsQuery(int UserId, string Term);
public record FilterAnimalsQuery(int UserId, int? BreedId, int? AgeMin, int? AgeMax, string? Sex, string? Status);
public record GetAnimalsByBreedQuery(int UserId, int BreedId);
public record GetAnimalsBySexQuery(int UserId, string Sex);
public record GetActiveAnimalsQuery(int UserId);
public record GetInactiveAnimalsQuery(int UserId);
public record GetAnimalsByAgeRangeQuery(int UserId, int? AgeMin, int? AgeMax);
public record GetAnimalsByWeightRangeQuery(int UserId, double? WeightMin, double? WeightMax);
public record GetAnimalGenealogyQuery(int UserId, int AnimalId);
public record GetAnimalAncestorsQuery(int UserId, int AnimalId);
public record GetAnimalDescendantsQuery(int UserId, int AnimalId);
public record GetAnimalSiblingsQuery(int UserId, int AnimalId);
public record CheckInbreedingQuery(int UserId, int AnimalId1, int AnimalId2);

public record GetAllBreedsQuery;
public record GetBreedByIdQuery(int BreedId);
public record SearchBreedsQuery(string Term);

public record GetAllBirthsQuery(int UserId);
public record GetBirthByIdQuery(int UserId, int BirthId);
public record GetBirthsByDateRangeQuery(int UserId, DateTime Start, DateTime End);
public record GetBirthsByMotherQuery(int UserId, int MotherId);
public record GetBirthsByFatherQuery(int UserId, int FatherId);

public record GetAllMatingsQuery(int UserId);
public record GetMatingByIdQuery(int UserId, int MatingId);
public record GetMatingsByFemaleQuery(int UserId, int FemaleId);
public record GetMatingsByMaleQuery(int UserId, int MaleId);
public record GetMatingsByDateRangeQuery(int UserId, DateTime Start, DateTime End);
public record GetPendingMatingsQuery(int UserId);

public record GetAllWeaningsQuery(int UserId);
public record GetWeaningByIdQuery(int UserId, int WeaningId);
public record GetWeaningsByCalfQuery(int UserId, int CalfId);
public record GetWeaningsByMotherQuery(int UserId, int MotherId);
