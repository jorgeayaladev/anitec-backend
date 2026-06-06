namespace Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;

public class Contact
{
    public Contact(int farmId, int contactUserId, string name, string? email, string? phone, string role, string? notes)
    {
        FarmId = farmId;
        ContactUserId = contactUserId;
        Name = name;
        Email = email;
        Phone = phone;
        Role = role;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int ContactUserId { get; private set; }
    public string Name { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string Role { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public Contact UpdateDetails(string name, string? email, string? phone, string role, string? notes)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Role = role;
        Notes = notes;
        return this;
    }
}
