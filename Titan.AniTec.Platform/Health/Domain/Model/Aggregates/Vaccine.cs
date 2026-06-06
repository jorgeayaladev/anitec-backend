namespace Titan.AniTec.Platform.Health.Domain.Model.Aggregates;

public class Vaccine
{
    public Vaccine(string name, string? description, string? manufacturer, int? durationDays)
    {
        Name = name;
        Description = description;
        Manufacturer = manufacturer;
        DurationDays = durationDays;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? Manufacturer { get; private set; }
    public int? DurationDays { get; private set; }

    public Vaccine UpdateDetails(string name, string? description, string? manufacturer, int? durationDays)
    {
        Name = name;
        Description = description;
        Manufacturer = manufacturer;
        DurationDays = durationDays;
        return this;
    }
}
