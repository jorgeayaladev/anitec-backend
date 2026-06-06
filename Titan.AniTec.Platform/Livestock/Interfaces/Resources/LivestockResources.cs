namespace Titan.AniTec.Platform.Livestock.Interfaces.Resources;

public record AnimalResource(
    int Id, int FarmId, string Code, string Species, string Sex, DateTime DateOfBirth,
    int? BreedId, string? Name, double? Weight, string? Color, string? Notes,
    string Status, DateTime? PurchaseDate, decimal? PurchasePrice,
    int? MotherId, int? FatherId);

public record CreateAnimalResource(
    string Code, string Species, string Sex, DateTime DateOfBirth,
    int? BreedId, string? Name, double? Weight, string? Color, string? Notes,
    int? MotherId, int? FatherId);

public record UpdateAnimalResource(
    string Code, string Species, string Sex, DateTime DateOfBirth,
    int? BreedId, string? Name, double? Weight, string? Color, string? Notes,
    string Status, DateTime? PurchaseDate, decimal? PurchasePrice,
    int? MotherId, int? FatherId);

public record BreedResource(int Id, string Name, string Species, string? Description);

public record CreateBreedResource(string Name, string Species, string? Description);

public record UpdateBreedResource(string Name, string Species, string? Description);

public record BirthResource(
    int Id, int FarmId, int MotherId, DateTime BirthDate, int OffspringCount, int? FatherId, string? Notes);

public record CreateBirthResource(int MotherId, DateTime BirthDate, int OffspringCount, int? FatherId, string? Notes);

public record UpdateBirthResource(DateTime BirthDate, int OffspringCount, int? FatherId, string? Notes);

public record MatingResource(
    int Id, int FarmId, int FemaleId, int MaleId, DateTime MatingDate, string? Result, string? Notes, DateTime? ConfirmedAt);

public record CreateMatingResource(int FemaleId, int MaleId, DateTime MatingDate, string? Notes);

public record UpdateMatingResource(DateTime MatingDate, int MaleId, string? Notes);

public record ConfirmMatingResource(string Result);

public record WeaningResource(
    int Id, int FarmId, int CalfId, int MotherId, DateTime WeaningDate, double? Weight, string? Notes);

public record CreateWeaningResource(int CalfId, int MotherId, DateTime WeaningDate, double? Weight, string? Notes);

public record UpdateWeaningResource(DateTime WeaningDate, double? Weight, string? Notes);

public record BatchUpdateAnimalItemResource(
    int AnimalId, string Code, string Species, string Sex, DateTime DateOfBirth,
    int? BreedId, string? Name, double? Weight, string? Color, string? Notes,
    string Status, DateTime? PurchaseDate, decimal? PurchasePrice,
    int? MotherId, int? FatherId);
