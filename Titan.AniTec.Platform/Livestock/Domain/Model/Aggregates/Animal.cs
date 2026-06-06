using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Livestock.Domain.Model.Aggregates;

public class Animal : IAuditableEntity
{
    public Animal(int farmId, string code, string species, string sex, DateTime dateOfBirth)
    {
        FarmId = farmId;
        Code = code;
        Species = species;
        Sex = sex;
        DateOfBirth = dateOfBirth;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int? BreedId { get; private set; }
    public string Code { get; private set; }
    public string? Name { get; private set; }
    public string Species { get; private set; }
    public string Sex { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public double? Weight { get; private set; }
    public string? Color { get; private set; }
    public string? Notes { get; private set; }
    public string Status { get; private set; } = "active";
    public DateTime? PurchaseDate { get; private set; }
    public decimal? PurchasePrice { get; private set; }
    public int? MotherId { get; private set; }
    public int? FatherId { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Animal UpdateDetails(string code, string species, string sex, DateTime dateOfBirth,
        int? breedId, string? name, double? weight, string? color, string? notes,
        string status, DateTime? purchaseDate, decimal? purchasePrice)
    {
        Code = code;
        Species = species;
        Sex = sex;
        DateOfBirth = dateOfBirth;
        BreedId = breedId;
        Name = name;
        Weight = weight;
        Color = color;
        Notes = notes;
        Status = status;
        PurchaseDate = purchaseDate;
        PurchasePrice = purchasePrice;
        return this;
    }

    public Animal SetParents(int? motherId, int? fatherId)
    {
        MotherId = motherId;
        FatherId = fatherId;
        return this;
    }
}
