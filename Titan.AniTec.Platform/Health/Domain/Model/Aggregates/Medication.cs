namespace Titan.AniTec.Platform.Health.Domain.Model.Aggregates;

public class Medication
{
    public Medication(string name, string? description, string? category, string? manufacturer,
        string? activeIngredient, string? presentation, string? dosageForm)
    {
        Name = name;
        Description = description;
        Category = category;
        Manufacturer = manufacturer;
        ActiveIngredient = activeIngredient;
        Presentation = presentation;
        DosageForm = dosageForm;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? Category { get; private set; }
    public string? Manufacturer { get; private set; }
    public string? ActiveIngredient { get; private set; }
    public string? Presentation { get; private set; }
    public string? DosageForm { get; private set; }

    public Medication UpdateDetails(string name, string? description, string? category, string? manufacturer,
        string? activeIngredient, string? presentation, string? dosageForm)
    {
        Name = name;
        Description = description;
        Category = category;
        Manufacturer = manufacturer;
        ActiveIngredient = activeIngredient;
        Presentation = presentation;
        DosageForm = dosageForm;
        return this;
    }
}
