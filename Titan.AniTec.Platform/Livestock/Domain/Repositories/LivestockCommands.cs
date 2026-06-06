namespace Titan.AniTec.Platform.Livestock.Domain.Repositories;

public record RegisterAnimalCommand(int UserId, string Code, string Species, string Sex, DateTime DateOfBirth,
    int? BreedId, string? Name, double? Weight, string? Color, string? Notes,
    int? MotherId, int? FatherId);

public record UpdateAnimalCommand(int UserId, int AnimalId, string Code, string Species, string Sex, DateTime DateOfBirth,
    int? BreedId, string? Name, double? Weight, string? Color, string? Notes,
    string Status, DateTime? PurchaseDate, decimal? PurchasePrice,
    int? MotherId, int? FatherId);

public record DeleteAnimalCommand(int UserId, int AnimalId);
public record RegisterAnimalBatchCommand(int UserId, IReadOnlyCollection<RegisterAnimalCommand> Animals);
public record UpdateAnimalBatchCommand(int UserId, IReadOnlyCollection<UpdateAnimalCommand> Animals);

public record RegisterBreedCommand(string Name, string Species, string? Description);
public record UpdateBreedCommand(int BreedId, string Name, string Species, string? Description);
public record DeleteBreedCommand(int BreedId);

public record RegisterBirthCommand(int UserId, int MotherId, DateTime BirthDate, int OffspringCount, int? FatherId, string? Notes);
public record UpdateBirthCommand(int UserId, int BirthId, DateTime BirthDate, int OffspringCount, int? FatherId, string? Notes);
public record DeleteBirthCommand(int UserId, int BirthId);

public record RegisterMatingCommand(int UserId, int FemaleId, int MaleId, DateTime MatingDate, string? Notes);
public record UpdateMatingCommand(int UserId, int MatingId, DateTime MatingDate, int MaleId, string? Notes);
public record DeleteMatingCommand(int UserId, int MatingId);
public record ConfirmMatingCommand(int UserId, int MatingId, string Result);

public record RegisterWeaningCommand(int UserId, int CalfId, int MotherId, DateTime WeaningDate, double? Weight, string? Notes);
public record UpdateWeaningCommand(int UserId, int WeaningId, DateTime WeaningDate, double? Weight, string? Notes);
public record DeleteWeaningCommand(int UserId, int WeaningId);
