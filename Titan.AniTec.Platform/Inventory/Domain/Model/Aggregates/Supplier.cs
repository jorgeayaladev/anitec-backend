using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Inventory.Domain.Model.Aggregates;

public class Supplier : IAuditableEntity
{
    public Supplier(int farmId, string name, string? contactPerson, string? phone, string? email, string? address, string? notes)
    {
        FarmId = farmId;
        Name = name;
        ContactPerson = contactPerson;
        Phone = phone;
        Email = email;
        Address = address;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public string Name { get; private set; }
    public string? ContactPerson { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? Address { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Supplier UpdateDetails(string name, string? contactPerson, string? phone, string? email, string? address, string? notes)
    {
        Name = name;
        ContactPerson = contactPerson;
        Phone = phone;
        Email = email;
        Address = address;
        Notes = notes;
        return this;
    }
}
