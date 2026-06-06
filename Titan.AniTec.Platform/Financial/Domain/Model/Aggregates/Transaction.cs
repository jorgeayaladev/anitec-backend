using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Financial.Domain.Model.Aggregates;

public class Transaction : IAuditableEntity
{
    public Transaction(int farmId, DateTime transactionDate, string type, string category,
        string description, decimal amount, int? animalId, string? paymentMethod,
        string? reference, string? notes)
    {
        FarmId = farmId;
        TransactionDate = transactionDate;
        Type = type;
        Category = category;
        Description = description;
        Amount = amount;
        AnimalId = animalId;
        PaymentMethod = paymentMethod;
        Reference = reference;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public string Type { get; private set; }
    public string Category { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public int? AnimalId { get; private set; }
    public string? PaymentMethod { get; private set; }
    public string? Reference { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Transaction UpdateDetails(DateTime transactionDate, string type, string category,
        string description, decimal amount, int? animalId, string? paymentMethod,
        string? reference, string? notes)
    {
        TransactionDate = transactionDate;
        Type = type;
        Category = category;
        Description = description;
        Amount = amount;
        AnimalId = animalId;
        PaymentMethod = paymentMethod;
        Reference = reference;
        Notes = notes;
        return this;
    }
}
